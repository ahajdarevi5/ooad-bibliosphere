namespace BibliospherePRAVI1.Models
{
    public class GodisnjiIzvjestaj : iIzvjestaj
    {
        public override iIzvjestaj GetClone()
        {
            return (GodisnjiIzvjestaj)this.MemberwiseClone();
        }
    }
}
