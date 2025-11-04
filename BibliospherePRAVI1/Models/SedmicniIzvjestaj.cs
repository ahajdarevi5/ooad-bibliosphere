namespace BibliospherePRAVI1.Models
{
    public class SedmicniIzvjestaj : iIzvjestaj
    {
        public override iIzvjestaj GetClone()
        {
            return (SedmicniIzvjestaj)this.MemberwiseClone();
        }
    }
}