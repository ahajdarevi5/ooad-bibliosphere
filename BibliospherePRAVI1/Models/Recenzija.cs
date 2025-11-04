using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BibliospherePRAVI1.Models
{
    public class Recenzija
    {
        [Key]
        public int ID { get; set; }

        public double Ocjena { get; set; }

        public string Komentar { get; set; }

        [ForeignKey("Knjiga")]
        public int? KnjigaID { get; set; }
        public Knjiga knjiga { get; set; }

        [ForeignKey("Korisnik")]
        public string? KorisnikID { get; set; }
        public IdentityUser korisnik { get; set; }

        public Recenzija() { }
    }
}
