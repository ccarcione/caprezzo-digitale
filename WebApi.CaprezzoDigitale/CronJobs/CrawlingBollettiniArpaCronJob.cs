using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale.CronJobs
{
    /// <summary>
    /// https://medium.com/@gtaposh/net-core-3-1-cron-jobs-background-service-e3026047b26d
    /// </summary>
    public class CrawlingBollettiniArpaCronJob : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly IServiceScopeFactory _scopeFactory;

        //private string Schedule => "*/10 * * * * *"; //Runs every 10 seconds
        private string Schedule => "* 30 13 * * *"; //Runs every 10 seconds

        public CrawlingBollettiniArpaCronJob(IServiceScopeFactory scopeFactory)
        {
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    Download_meteoidrologica();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(new TimeSpan(1, 0, 0), stoppingToken); //1 hours delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Download_meteoidrologica()
        {
            string URL_XML_File = @"http://www.arpa.piemonte.it/export/xmlcap/allerta.xml";
            string URL_PDF_File = @"http://www.arpa.piemonte.it/bollettini/bollettino_allerta.pdf/at_download/file";
            string tipoBollettino = "meteoidrologica";

            using (var scope = _scopeFactory.CreateScope())
            {
                ILogger<CrawlingBollettiniArpaCronJob> logger = scope.ServiceProvider.GetRequiredService<ILogger<CrawlingBollettiniArpaCronJob>>();
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                try
                {
                    string xmlFilePath = DownloadBollettino(logger, URL_XML_File, tipoBollettino);
                    string pdfFilePath = DownloadBollettino(logger, URL_PDF_File, tipoBollettino, @"/at_download/file");

                    context.BollettiniArpa.Add(new BollettinoArpa()
                    {
                        Date = DateTime.Now,
                        Tipo = tipoBollettino,
                        XML_FileName = Path.Combine(xmlFilePath),
                        PDF_FileName = Path.Combine(pdfFilePath)
                    });
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    logger.LogError("Errore download bollettino.", URL_XML_File, URL_PDF_File, e);
                }
            }
        }

        private string DownloadBollettino(ILogger<CrawlingBollettiniArpaCronJob> logger, string URLString, string tipoBollettino, string replaceTextPerEstrazioneNomeFile = null)
        {
            string url_per_estrazione_filename = replaceTextPerEstrazioneNomeFile == null ? URLString : URLString.Replace(replaceTextPerEstrazioneNomeFile, "");
            string path = Path.Combine("BollettiniArpa", tipoBollettino);
            string fileName = string.Concat(
                    Path.GetFileNameWithoutExtension(url_per_estrazione_filename),
                    "_",
                    DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"),
                    Path.GetExtension(url_per_estrazione_filename));

            logger.LogDebug($"Crawling bollettino {tipoBollettino} {fileName}, {DateTime.Now.ToString("F")}");

            using (HttpClient httpClient = new HttpClient())
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                    
                using (FileStream outputFileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    httpClient.GetAsync(URLString).Result
                        .Content
                        .ReadAsStreamAsync().Result
                        .CopyToAsync(outputFileStream);
                }
                logger.LogDebug($"Bollettino {fileName} salvato su disco, {DateTime.Now.ToString("F")}");
            }

            return Path.Combine(path, fileName);
        }
    }
}
