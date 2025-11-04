using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BibliospherePRAVI1.Models;

namespace BibliospherePRAVI1.Models
{
    public class Autor_Knjiga
    {
        [Key]
        public int AutorKnjigaID { get; set; }
        [ForeignKey("Autor")]
        public int AutorID { get; set; }
        [ForeignKey("Knjiga")]
        public int KnjigaID { get; set; }

        public Autor Autor { get; set; }
        public Knjiga Knjiga { get; set; }

        public Autor_Knjiga() { }

    }
}