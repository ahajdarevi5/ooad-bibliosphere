using BibliospherePRAVI1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibliospherePRAVI1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Knjiga> Knjiga { get; set; }
        public DbSet<Izdavac> izdavac { get; set; }
        public DbSet<Recenzija> recenzija { get; set; }
        public DbSet<Autor> autor { get; set; }
        public DbSet<Autor_Knjiga> autorKnjiga { get; set; }
        public DbSet<KnjigaKorisnik> knjigaKorisnik { get; set; }
        public DbSet<Korisnik> korisnik { get; set; }
        public DbSet<GodisnjiIzvjestaj> GodisnjiIzvjestaji { get; set; }
        public DbSet<MjesecniIzvjestaj> MjesecniIzvjestaji { get; set; }
        public DbSet<SedmicniIzvjestaj> SedmicniIzvjestaji { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Knjiga>().ToTable("Knjiga");
            modelBuilder.Entity<Izdavac>().ToTable("Izdavac");
            modelBuilder.Entity<Recenzija>().ToTable("Recenzija");
            modelBuilder.Entity<Autor>().ToTable("Autor");
            modelBuilder.Entity<Autor_Knjiga>().ToTable("AutorKnjiga");
            modelBuilder.Entity<KnjigaKorisnik>().ToTable("KnjigaKorisnik");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");



            modelBuilder.Entity<KnjigaKorisnik>()
                .HasOne(kk => kk.korisnik)
                .WithMany(k => k.knjigaKorisnik)
                .HasForeignKey(kk => kk.korisnikID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Autor_Knjiga>()
            .HasKey(ak => new { ak.AutorID, ak.KnjigaID });

            modelBuilder.Entity<Autor_Knjiga>()
                .HasOne(ak => ak.Autor)
                .WithMany(a => a.AutorKnjiga)
                .HasForeignKey(ak => ak.AutorID);

            modelBuilder.Entity<Autor_Knjiga>()
                .HasOne(ak => ak.Knjiga)
                .WithMany(k => k.AutorKnjiga)
                .HasForeignKey(ak => ak.KnjigaID);
        }
        public async Task<GodisnjiIzvjestaj> CloneGodisnjiIzvjestajAsync(int izvjestajID)
        {
            var izvjestaj = await GodisnjiIzvjestaji.FindAsync(izvjestajID);
            if (izvjestaj == null)
            {
                return null;
            }

            return (GodisnjiIzvjestaj)izvjestaj.GetClone();
        }

        public async Task<MjesecniIzvjestaj> CloneMjesecniIzvjestajAsync(int izvjestajID)
        {
            var izvjestaj = await MjesecniIzvjestaji.FindAsync(izvjestajID);
            if (izvjestaj == null)
            {
                return null;
            }

            return (MjesecniIzvjestaj)izvjestaj.GetClone();
        }

        public async Task<SedmicniIzvjestaj> CloneSedmicniIzvjestajAsync(int izvjestajID)
        {
            var izvjestaj = await SedmicniIzvjestaji.FindAsync(izvjestajID);
            if (izvjestaj == null)
            {
                return null;
            }

            return (SedmicniIzvjestaj)izvjestaj.GetClone();
        }



    }
}