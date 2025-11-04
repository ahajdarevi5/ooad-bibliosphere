using System.ComponentModel.DataAnnotations;

namespace BibliospherePRAVI1.Models
{
    public class Korisnik : iKnjiga
    {
        [Key]
        public int korisnikID { get; set; }

        public string korisnikIme { get; set; }

        public string korisnikPrezime { get; set; }
        public string korisnikUsername { get; set; }
        public string korisnikEmail { get; set; }
        public string korisnikLozinka { get; set; }
        public DateTime korisnikDatumRodjenja { get; set; }
        public string brojTelefona { get; set; }

        //navigacija za knjigaKorisnik
        public List<KnjigaKorisnik> knjigaKorisnik { get; set; }

        // lista knjiga za iSearch
        public List<Knjiga> listaKnjiga { get; set; }

        // atributi za citatelja 
        public DateTime citateljDatPrid { get; set; }

        //atributi za korisnicku podrsku
        public DateTime datumZaposlenjaKP { get; set; }

        //atributi za admina
        public DateTime datumZaposlenjaAD { get; set; }

        public Korisnik()
        {
            knjigaKorisnik = new List<KnjigaKorisnik>();
        }

        public Knjiga nadjiKnjigu(string naziv)
        {
            Knjiga k = new Knjiga();
            return k;
        }

    }
}