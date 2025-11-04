using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BibliospherePRAVI1.Models
{
    public abstract class iIzvjestaj
    {
        [Key]
        public int IzvjestajID { get; set; }
        [ForeignKey("Citatelj")]
        [DisplayName("User ID:")]
        public int? korisnikID { get; set; }

        public Korisnik korisnik { get; set; }


        public string Sadrzaj { get; set; }
        [DisplayName("Most read genre:")]
        public VrstaKnjige najcitanijiZanr { get; set; }


        public abstract iIzvjestaj GetClone();
    }
}
