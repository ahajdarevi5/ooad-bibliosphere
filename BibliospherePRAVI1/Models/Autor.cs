using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliospherePRAVI1.Models
{
    public class Autor
    {
        [Key]
        public int autorID { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only lowercase and uppercase letters!")]
        [DisplayName("Name:")]
        public string autorIme { get; set; }

        [DisplayName("Surname:")]
        public string autorPrezime { get; set; }

        [DisplayName("Picture:")]
        public string autorSlika { get; set; }

        [DisplayName("Picture:")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of birth:")]
        public DateTime autorDatumRod { get; set; }

        // navigacija za Autor_Knjiga
        public List<Autor_Knjiga> AutorKnjiga { get; set; }

        public Autor()
        {
            AutorKnjiga = new List<Autor_Knjiga>();
        }
    }
}

