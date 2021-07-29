using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebApi.CaprezzoDigitale.ExtensionMethods;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale
{
    public class SeedMockData
    {
        public static void SeedAllData(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogDebug("******************** Inizio SeedMockData ********************");
            SeedTipiMessaggio(app, logger);
            SeedMessaggi(app, logger);
            SeedGalleria(app, logger);
            SeedTipiStatistiche(app, logger);
            // seed Messaggi con eventuali allegati
            logger.LogDebug("******************** Fine SeedMockData ********************");

        }
        public static void SeedTipiMessaggio(IApplicationBuilder app, ILogger<Startup> logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<Models.Options>();

                string mockTipiMessaggio_Path = GetWindowsLinuxPath(options.WebApiOptions["mockTipiMessaggio_Path"]);
                logger.LogDebug($"Leggo file '{mockTipiMessaggio_Path}'.");
                string jsonString = File.ReadAllText(mockTipiMessaggio_Path, Encoding.UTF7);
                List<TipoMessaggio> tipiMessaggio = JsonConvert.DeserializeObject<List<TipoMessaggio>>(jsonString);
                logger.LogDebug($"TipiMessaggio trovati: {tipiMessaggio.Count()}.");

                // seed TipiMessaggio
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                int count = 0;
                tipiMessaggio.ForEach(f =>
                {
                    if (context.TipiMessaggio.FirstOrDefault(t => t.DisplayName == f.DisplayName && t.Descrizione == f.Descrizione) == null)
                    {
                        context.TipiMessaggio.Add(f);
                        count++;
                    }
                });
                context.SaveChanges();
                logger.LogDebug($"Seed {count} TipiMessaggio.");
            }
        }
        public static void SeedMessaggi(IApplicationBuilder app, ILogger<Startup> logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<Models.Options>();

                string mockMessaggi_Path = GetWindowsLinuxPath(options.WebApiOptions["mockMessaggi_Path"]);
                logger.LogDebug($"Leggo file '{mockMessaggi_Path}'.");
                string jsonString = File.ReadAllText(mockMessaggi_Path, Encoding.UTF7).Replace("\\n", Environment.NewLine);
                List<Messaggio> messaggi = JsonConvert.DeserializeObject<List<Messaggio>>(jsonString);
                logger.LogDebug($"Messaggi trovati: {messaggi.Count()}.");

                // seed Messaggi
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                int count = 0;
                messaggi.ForEach(f =>
                {
                    if (context.Messaggi.FirstOrDefault(m => m.Titolo == f.Titolo && m.DataPubblicazione == f.DataPubblicazione) == null)
                    {
                        // se l'immagine in seed è un link scaricala.
                        if (Regex.IsMatch(f.UrlImmagineCopertina, "(^http://)|(^https://)", RegexOptions.IgnoreCase))
                        {
                            // scarica immagine nella giusta cartella
                            string filename = new Regex(new string(Path.GetInvalidFileNameChars()))
                                .Replace(Path.GetFileName(f.UrlImmagineCopertina), string.Empty);
                            SeedMockData.DownloadRemoteImageFile(f.UrlImmagineCopertina, Path.Combine(GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]), filename));
                            
                            // sistema UrlImmagine del messaggio
                            f.UrlImmagineCopertina = filename;
                        }
                        else if (!string.IsNullOrWhiteSpace(f.UrlImmagineCopertina))
                        {
                            // altrimenti procedi normalmente.
                            File.Copy(
                                Path.Combine(Directory.GetParent(mockMessaggi_Path).FullName, f.UrlImmagineCopertina),
                                Path.Combine(GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]), f.UrlImmagineCopertina), true);
                            f.UrlImmagineCopertina = Path.GetFileName(f.UrlImmagineCopertina);
                        }

                        // seed allegati messaggio
                        f.Allegati?.ForEach(a =>
                        {
                            if (!string.IsNullOrWhiteSpace(a.FilePath))
                            {
                                File.Copy(
                                    Path.Combine(Directory.GetParent(mockMessaggi_Path).FullName, a.FilePath),
                                    Path.Combine(GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]), a.FilePath), true);
                                a.FilePath = Path.GetFileName(a.FilePath);
                            }
                        });

                        context.Messaggi.Add(f);
                        count++;
                    }
                });
                context.SaveChanges();
                logger.LogDebug($"Seed {count} Messaggi.");
            }
        }
        public static void SeedGalleria(IApplicationBuilder app, ILogger<Startup> logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Models.Options options = scope.ServiceProvider.GetRequiredService<Models.Options>();

                List<string> listImageFileName = Directory
                    .GetFiles(GetWindowsLinuxPath(options.WebApiOptions["mockImage_Path"]))
                    .OrderBy(o => o)
                    .ToList();
                logger.LogDebug($"Immagini galleria trovate: {listImageFileName.Count()}.");

                // seed immagini galleria
                int count = 0;
                int countCopyImage = 0;
                string outFileName;
                listImageFileName.ForEach(f =>
                {
                    outFileName = Path.Combine(GetWindowsLinuxPath(options.WebApiOptions["galleria_FolderPath"]), Path.GetFileName(f));
                    count++;
                    if (!File.Exists(outFileName))
                    {
                        File.Copy(f, outFileName);
                        countCopyImage++;
                    }
                });
                logger.LogDebug($"Copiate {countCopyImage} nuove immagini nella cartella {options.WebApiOptions["galleria_FolderPath"]}.");
                logger.LogDebug($"Seed {count} immagini in galleria.");
            }
        }
        public static void SeedTipiStatistiche(IApplicationBuilder app, ILogger<Startup> logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<Models.Options>();

                logger.LogDebug("Check tipi statistiche.");
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                foreach (TipiStatistica tipoStatistica in (TipiStatistica[])Enum.GetValues(typeof(TipiStatistica)))
                {
                    if (context.TipiStatistica.FirstOrDefault(s => s.Tipo == tipoStatistica.Name()) == null)
                    {
                        TipoStatistica tipo = new TipoStatistica()
                        {
                            Id = (short)tipoStatistica,
                            Tipo = tipoStatistica.Name(),
                            Descrizione = tipoStatistica.Description()
                        };
                        logger.LogDebug($"Add statistica {JsonConvert.SerializeObject(tipo)}.");
                        context.TipiStatistica.Add(tipo);
                    }
                }
                context.SaveChanges();
            }
        }
        private static void DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // if the remote file was found, download oit
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
            }
        }
        public static string GetWindowsLinuxPath(string path)
        {
            return Path.Combine(path.Split('\\'));
        }
    }
}
