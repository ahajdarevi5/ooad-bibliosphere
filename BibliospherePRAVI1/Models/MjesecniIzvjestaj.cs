namespace BibliospherePRAVI1.Models
{
    public class MjesecniIzvjestaj : iIzvjestaj
    {
        public override iIzvjestaj GetClone()
        {
            return (MjesecniIzvjestaj)this.MemberwiseClone();
        }
    }
}