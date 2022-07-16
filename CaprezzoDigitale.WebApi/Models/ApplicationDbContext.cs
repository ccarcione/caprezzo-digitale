using Microsoft.EntityFrameworkCore;

namespace CaprezzoDigitale.WebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // use only for Update-Database command
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Server=localhost;Port=5433;Database=caprezzo_digitale.dev;User Id=postgres;Password=example");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Messaggio>()
                .HasOne(p => p.TipoMessaggio)
                .WithMany(b => b.Messaggi)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Messaggio>()
                .HasMany(p => p.Allegati)
                .WithOne(b => b.Messaggio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Statistica>()
                .HasOne(p => p.TipoStatistica)
                .WithMany(b => b.Statistiche)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Statistica>()
                .Property(p => p.Data)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<Feedback>()
                .Property(p => p.Data)
                .HasDefaultValueSql("now()");
        }

        public DbSet<Messaggio> Messaggi { get; set; }
        public DbSet<Allegato> Allegati { get; set; }
        public DbSet<TipoMessaggio> TipiMessaggio { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Statistica> Statistiche { get; set; }
        public DbSet<TipoStatistica> TipiStatistica { get; set; }
        public DbSet<BollettinoArpa> BollettiniArpa { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Keys> SubscriptionKeys { get; set; }
    }
}
