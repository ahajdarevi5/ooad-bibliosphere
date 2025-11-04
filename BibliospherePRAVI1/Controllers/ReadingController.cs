using BibliospherePRAVI1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliospherePRAVI1.Controllers
{
    public class ReadingController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private const int LinesPerPage = 32; // Number of lines per page

        public ReadingController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Read(int bookId, int pageNumber = 1)
        {
            var bookContent = GetBookContent();

            // Calculate total pages based on lines per page
            var totalPages = (bookContent.Length + LinesPerPage - 1) / LinesPerPage;

            // Handle case where book is not found or page number is out of range
            if (bookContent == null || pageNumber < 1 || pageNumber > totalPages)
            {
                return NotFound();
            }

            // Get the lines for the current page
            var currentPageLines = bookContent.Skip((pageNumber - 1) * LinesPerPage).Take(LinesPerPage).ToArray();

            // Join lines into a single string
            var currentPageContent = string.Join("\n", currentPageLines);

            // Create the ViewModel with the current page content and navigation info
            var viewModel = new BookReadingViewModel
            {
                BookId = bookId,
                CurrentPageContent = currentPageContent,
                CurrentPageNumber = pageNumber,
                TotalPages = totalPages
            };

            // Pass the ViewModel to the view
            return View(viewModel);
        }

        private string[] GetBookContent()
        {
            // Construct the file path for the book's text file
            var bookFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "books", "1.txt");

            // Check if the file exists
            if (!System.IO.File.Exists(bookFilePath))
            {
                return null;
            }

            // Read all lines from the text file
            return System.IO.File.ReadAllLines(bookFilePath);
        }

    }
}
