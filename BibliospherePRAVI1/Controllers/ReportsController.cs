using BibliospherePRAVI1.Models;
using Microsoft.AspNetCore.Mvc;
using BibliospherePRAVI1.Data;

namespace BibliospherePRAVI1.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult GodisnjiIzvjestaj()
        {
            
            
            return View();
        }

        public IActionResult MjesecniIzvjestaj()
        {
            
    
            return View();
        }

        public IActionResult SedmicniIzvjestaj()
        {
          
            
            return View();
        }
    }

}
