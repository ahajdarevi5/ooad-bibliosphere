namespace BibliospherePRAVI1.Models
{
    public class Bridge
    {
        private readonly iKnjiga _knjiga;

        public Bridge(iKnjiga knjiga)
        {
            _knjiga = knjiga;
        }

        public Knjiga nadjiKnjigu(string naziv)
        {
            return _knjiga.nadjiKnjigu(naziv);
        }
    }

}
