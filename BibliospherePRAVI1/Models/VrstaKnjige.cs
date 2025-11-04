using System.ComponentModel.DataAnnotations;

namespace BibliospherePRAVI1.Models
{
    public enum VrstaKnjige
    {
        [Display(Name = "Memoir")]
        Memoir,
        [Display(Name = "Self-help")]
        SelfHelp,
        [Display(Name = "Poetry")]
        Poetry,
        [Display(Name = "Novel")]
        Novel,
        [Display(Name = "Short story")]
        ShortStory,
        [Display(Name = "Novella")]
        Novella

    }
}
