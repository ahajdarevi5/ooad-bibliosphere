using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BibliospherePRAVI1.Models
{
    public class KnjigaKorisnik
    {
        [Key]
        public int knjigaKorisnikID { get; set; }

        [ForeignKey("Korisnik")]
        public int korisnikID { get; set; }
        [ForeignKey("Knjiga")]
        public int? knjigaID { get; set; }

        public Korisnik korisnik { get; set; }
        public Knjiga knjiga { get; set; }

        public VrstaListe vrstaListe { get; set; }
        public KnjigaKorisnik() { }
    }
}
