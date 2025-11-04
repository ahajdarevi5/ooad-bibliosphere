using System.ComponentModel.DataAnnotations;

namespace BibliospherePRAVI1.Models
{
    public enum VrstaListe
    {
        [Display(Name = "Favorites")]
        Favorites,
        [Display(Name = "Read")]
        Read
    }
}
