using BibliospherePRAVI1.Models;

public class KnjigaDetailsViewModel
{
    public Knjiga Knjiga { get; set; }
    public Recenzija NovaRecenzija { get; set; } = new Recenzija();
    public List<Recenzija> Recenzije { get; set; } = new List<Recenzija>();
    public bool IsInReadList { get; set; }
}
