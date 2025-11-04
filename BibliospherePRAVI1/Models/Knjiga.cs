using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BibliospherePRAVI1.Models
{
    public class Knjiga
    {
        [Key]
        public int knjigaID { get; set; }

        [DisplayName("Book ISBN:")]
        public long ISBN { get; set; }
        [DisplayName("Book name:")]
        public string knjigaNaziv { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date published:")]
        public DateTime datumObjavljivanjaKnj { get; set; }

        [DisplayName("Type:")]
        [EnumDataType(typeof(VrstaKnjige))] public VrstaKnjige vrstaKnjige { get; set; }
        [DisplayName("Barcode:")]
        public string knjigaBarKod { get; set; }
        [DisplayName("Picture:")]
        public string knjigaSlika { get; set; }
        [DisplayName("Publisher:")]
        public int? izdavacID { get; set; }

        [DisplayName("Publisher:")]
        public Izdavac izdavac { get; set; }

        [DisplayName("Picture:")]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [DisplayName("Barcode:")]
        [NotMapped]
        public IFormFile? ImageFileB { get; set; }


        //navigacija za Autor_Knjiga
        public List<Autor_Knjiga> AutorKnjiga { get; set; }


        public List<Recenzija> Recenzije { get; set; }

        public Knjiga()
        {
            Recenzije = new List<Recenzija>();
        }
    }
}
