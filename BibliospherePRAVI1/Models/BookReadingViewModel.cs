namespace BibliospherePRAVI1.Models
{
    public class BookReadingViewModel
    {
        public int BookId { get; set; }
        public string CurrentPageContent { get; set; }
        public int CurrentPageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
