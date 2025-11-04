using System.ComponentModel.DataAnnotations;

namespace BibliospherePRAVI1.Models
{
    public enum VrstaIzvjestaja
    {
        [Display(Name = "Genre")]
        Genre,
        [Display(Name = "Author")]
        Author,
        [Display(Name = "Number of pages")]
        NumberOfPages,
        [Display(Name = "Reading time")]
        ReadingTime,
        [Display(Name = "Recommendations")]
        Recommendations
    }
}
