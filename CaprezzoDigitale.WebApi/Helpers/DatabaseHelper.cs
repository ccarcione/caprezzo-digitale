using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using CaprezzoDigitale.WebApi.ExtensionMethods;
using CaprezzoDigitale.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CaprezzoDigitale.WebApi.Helpers
{
    public class DatabaseHelper
    {
        public static void UpdateDatabaseMigrate(IApplicationBuilder app, ILogger logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                string databaseName = context.Database.GetDbConnection().Database;
                if (!(context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    logger.LogDebug($"Database {databaseName} not found. Database initialization with migration.");
                    context.Database.Migrate();
                }
                logger.LogDebug($"Database {databaseName} ok!");
                if (context.Database.GetPendingMigrations().Any())
                {
                    logger.LogDebug($"Database {databaseName} not updated. Pending Migrations application.");
                    context.Database.Migrate();
                }
                logger.LogDebug($"Pending Migrations Database {databaseName} ok.");
            }
        }

        public static void SeedAllData(IApplicationBuilder app, ILogger logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Options options = scope.ServiceProvider.GetRequiredService<Options>();

                bool isSeedAllData;
                bool.TryParse(options.WebApiOptions["seedAllData"], out isSeedAllData);
                if (!isSeedAllData)
                {
                    return;
                }
                logger.LogDebug("******************** Start SeedMockData ********************");
                SeedTipiMessaggio(app, logger);

                // seed Messaggi con eventuali allegati
                bool ignoreSeedMessaggi;
                bool.TryParse(options.WebApiOptions["ignoreSeedMessaggi"], out ignoreSeedMessaggi);
                if (!ignoreSeedMessaggi)
                {
                    SeedMessaggi(app, logger);
                }    

                bool ignoreSeedGalleria;
                bool.TryParse(options.WebApiOptions["ignoreSeedGalleria"], out ignoreSeedGalleria);
                if (!ignoreSeedGalleria)
                {
                    SeedGalleria(app, logger);
                }

                SeedTipiStatistiche(app, logger);
                logger.LogDebug("******************** End SeedMockData ********************");
            }

        }
        
        private static void SeedTipiMessaggio(IApplicationBuilder app, ILogger logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Options options = scope.ServiceProvider.GetRequiredService<Options>();

                string mockTipiMessaggio_Path = UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["mockTipiMessaggio_Path"]);
                logger.LogDebug($"Read file '{mockTipiMessaggio_Path}'.");
                if (!File.Exists(mockTipiMessaggio_Path))
                {
                    logger.LogDebug($"File {mockTipiMessaggio_Path} not found.");
                    return;
                }
                string jsonString = File.ReadAllText(mockTipiMessaggio_Path, Encoding.UTF7);
                List<TipoMessaggio> tipiMessaggio = JsonConvert.DeserializeObject<List<TipoMessaggio>>(jsonString);
                logger.LogDebug($"TipiMessaggio found: {tipiMessaggio.Count()}.");

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
        private static void SeedMessaggi(IApplicationBuilder app, ILogger logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Options options = scope.ServiceProvider.GetRequiredService<Options>();

                string mockMessaggi_Path = UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["mockMessaggi_Path"]);
                logger.LogDebug($"Read file '{mockMessaggi_Path}'.");
                if (!File.Exists(mockMessaggi_Path))
                {
                    logger.LogDebug($"File {mockMessaggi_Path} not found.");
                    return;
                }
                string jsonString = File.ReadAllText(mockMessaggi_Path, Encoding.UTF7).Replace("\\n", Environment.NewLine);
                List<Messaggio> messaggi = JsonConvert.DeserializeObject<List<Messaggio>>(jsonString);
                logger.LogDebug($"Messaggi found: {messaggi.Count()}.");

                // seed Messaggi
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                int count = 0;
                messaggi.ForEach(f =>
                {
                    if (context.Messaggi.FirstOrDefault(m => m.Titolo == f.Titolo && m.DataPubblicazione == f.DataPubblicazione) == null)
                    {
                        // if the seeded image is a link download it.
                        if (Regex.IsMatch(f.UrlImmagineCopertina, "(^http://)|(^https://)", RegexOptions.IgnoreCase))
                        {
                            // download image in the right folder
                            string filename = new Regex(new string(Path.GetInvalidFileNameChars()))
                                .Replace(Path.GetFileName(f.UrlImmagineCopertina), string.Empty);
                            UtilityHelper.DownloadRemoteImageFile(f.UrlImmagineCopertina, Path.Combine(UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]), filename));

                            // system UrlImage of the message
                            f.UrlImmagineCopertina = filename;
                        }
                        else if (!string.IsNullOrWhiteSpace(f.UrlImmagineCopertina))
                        {
                            // otherwise proceed normally.
                            File.Copy(
                                Path.Combine(Directory.GetParent(mockMessaggi_Path).FullName, f.UrlImmagineCopertina),
                                Path.Combine(UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]), f.UrlImmagineCopertina), true);
                            f.UrlImmagineCopertina = Path.GetFileName(f.UrlImmagineCopertina);
                        }

                        // seed allegati messaggio
                        f.Allegati?.ForEach(a =>
                        {
                            if (!string.IsNullOrWhiteSpace(a.FilePath))
                            {
                                File.Copy(
                                    Path.Combine(Directory.GetParent(mockMessaggi_Path).FullName, a.FilePath),
                                    Path.Combine(UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]), a.FilePath), true);
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
        private static void SeedGalleria(IApplicationBuilder app, ILogger logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Options options = scope.ServiceProvider.GetRequiredService<Options>();
                List<string> validExtImage = new List<string>()
                {
                    ".bmp", ".jpg", ".jpeg", ".jpe", ".jfif", ".gif", ".png"
                };

                List<string> listImageFileName = Directory
                    .GetFiles(UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["mockImage_Path"]))
                    .Where(f => validExtImage.Contains(Path.GetExtension(f)))
                    .OrderBy(o => o)
                    .ToList();
                logger.LogDebug($"Immagini galleria found: {listImageFileName.Count()}.");

                // seed immagini galleria
                int count = 0;
                int countCopyImage = 0;
                string outFileName;
                listImageFileName.ForEach(f =>
                {
                    outFileName = Path.Combine(UtilityHelper.GetWindowsLinuxPath(options.WebApiOptions["galleria_FolderPath"]), Path.GetFileName(f));
                    count++;
                    if (!File.Exists(outFileName))
                    {
                        File.Copy(f, outFileName);
                        countCopyImage++;
                    }
                });
                logger.LogDebug($"Copy {countCopyImage}/{count} new images to the folder {options.WebApiOptions["galleria_FolderPath"]}.");
            }
        }
        private static void SeedTipiStatistiche(IApplicationBuilder app, ILogger logger)
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
    }
}
