using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BibliospherePRAVI1.Models
{
    public class Izdavac
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Name:")]
        public string NazivIzdavaca { get; set; }

        public Izdavac() { }
    }
}