using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliospherePRAVI1.Data;
using BibliospherePRAVI1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace BibliospherePRAVI1.Controllers
{
    public class KnjigaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _enviroment;
        private readonly UserManager<IdentityUser> _userManager;

        public KnjigaController(ApplicationDbContext context, IWebHostEnvironment enviroment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _enviroment = enviroment;
            _userManager = userManager;
        }

        // GET: Knjiga
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Knjiga.Include(k => k.izdavac);

            var knjige = _context.Knjiga.ToList();
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string bookSrch)
        {
            ViewData["knjiga"] = bookSrch;

            var qry = _context.Knjiga.Include(k => k.izdavac).AsQueryable();
            if (!System.String.IsNullOrEmpty(bookSrch))
            {
                qry = qry.Where(x => x.knjigaNaziv.Contains(bookSrch));
            }

            return View(await qry.AsNoTracking().ToListAsync());
        }



        // GET: Knjiga/Create
        public IActionResult Create()
        {
            ViewData["izdavacID"] = new SelectList(_context.izdavac, "ID", "NazivIzdavaca");

            return View();
        }

        // POST: Knjiga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["izdavacID"] = new SelectList(_context.izdavac, "ID", "NazivIzdavaca", knjiga.izdavacID);


            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(knjiga.ImageFile!.FileName);

            string imageFullPath = _enviroment.WebRootPath + "/slike/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {
                knjiga.ImageFile.CopyTo(stream);
            }

            //////////////////////////////////////
            string newFileName1 = DateTime.Now.ToString("yyyyMMddHHff");
            newFileName1 += Path.GetExtension(knjiga.ImageFileB!.FileName);

            string imageFullPath1 = _enviroment.WebRootPath + "/barkodovi/" + newFileName1;

            using (var stream = System.IO.File.Create(imageFullPath1))
            {
                knjiga.ImageFileB.CopyTo(stream);
            }

            Knjiga novaKnj = new Knjiga()
            {
                ISBN = knjiga.ISBN,
                knjigaNaziv = knjiga.knjigaNaziv,
                datumObjavljivanjaKnj = knjiga.datumObjavljivanjaKnj,
                vrstaKnjige = knjiga.vrstaKnjige,
                knjigaSlika = newFileName,
                knjigaBarKod = newFileName1,
                izdavacID = knjiga.izdavacID,
            };

            _context.Knjiga.Add(novaKnj);
            _context.SaveChanges();

            return RedirectToAction("Index", "Knjiga");
        }
        // GET: Knjiga/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjiga.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }

            ViewData["izdavacID"] = new SelectList(_context.izdavac, "ID", "NazivIzdavaca", knjiga.izdavacID);
            return View(knjiga);
        }


        /*[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("knjigaID,ISBN,knjigaNaziv,datumObjavljivanjaKnj,vrstaKnjige,knjigaBarKod,knjigaSlika,izdavacID")] Knjiga knjiga)
{
    if (id != knjiga.knjigaID)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Fetch the existing book to retain the current image if no new image is provided
            var existingKnjiga = await _context.Knjiga.AsNoTracking().FirstOrDefaultAsync(k => k.knjigaID == id);
            if (existingKnjiga == null)
            {
                return NotFound();
            }

            // Retain the existing image file name
            knjiga.knjigaSlika = existingKnjiga.knjigaSlika;
                    knjiga.knjigaBarKod = existingKnjiga.knjigaBarKod;

                    _context.Update(knjiga);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!KnjigaExists(knjiga.knjigaID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    ViewData["izdavacID"] = new SelectList(_context.izdavac, "ID", "NazivIzdavaca", knjiga.izdavacID);
    return View(knjiga);
}*/
        // POST: Knjiga/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("knjigaID,ISBN,knjigaNaziv,datumObjavljivanjaKnj,vrstaKnjige,izdavacID")] Knjiga knjiga)
        {
            if (id != knjiga.knjigaID)
            {
                Console.WriteLine("TTRAZIM ID PRVI");
                return NotFound();
            }
            Console.WriteLine("JEL VALIDAN MODEL");
            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing book
                    var existingKnjiga = await _context.Knjiga.FindAsync(id);
                    Console.WriteLine("NASAO KNJIGU");
                    if (existingKnjiga == null)
                    {
                        Console.WriteLine("NEMA KNJIGE");
                        return NotFound();
                    }
                    Console.WriteLine("APDEJT");
                    // Update properties (excluding picture and barcode)
                    existingKnjiga.ISBN = knjiga.ISBN;
                    existingKnjiga.knjigaNaziv = knjiga.knjigaNaziv;
                    existingKnjiga.datumObjavljivanjaKnj = knjiga.datumObjavljivanjaKnj;
                    existingKnjiga.vrstaKnjige = knjiga.vrstaKnjige;
                    existingKnjiga.izdavacID = knjiga.izdavacID;
                    Console.WriteLine("EVO DO TU");

                    // Update the entity
                    _context.Update(existingKnjiga);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("BAZAU");


                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnjigaExists(knjiga.knjigaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                // Log the model state errors
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            ViewData["izdavacID"] = new SelectList(_context.izdavac, "ID", "NazivIzdavaca", knjiga.izdavacID);
            return View(knjiga);
        }

        // GET: Knjiga/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjiga
                .Include(k => k.izdavac)
                .Include(k => k.Recenzije) // Include related reviews
                .Include(k => k.AutorKnjiga) // Include related author_books
                .FirstOrDefaultAsync(m => m.knjigaID == id);

            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        // POST: Knjiga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knjiga = await _context.Knjiga
                .Include(k => k.Recenzije) // Include related reviews
                .Include(k => k.AutorKnjiga) // Include related author_books
                .FirstOrDefaultAsync(m => m.knjigaID == id);

            if (knjiga != null)
            {
                // Remove related reviews
                _context.recenzija.RemoveRange(knjiga.Recenzije);

                // Remove related author_books
                _context.autorKnjiga.RemoveRange(knjiga.AutorKnjiga);

                // Remove the book itself
                _context.Knjiga.Remove(knjiga);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var userName = user.UserName;


            var knjiga = await _context.Knjiga
                .Include(k => k.izdavac)
                .Include(k => k.AutorKnjiga)
                    .ThenInclude(ak => ak.Autor)
                .Include(k => k.Recenzije)
                    .ThenInclude(r => r.korisnik)
                .FirstOrDefaultAsync(m => m.knjigaID == id);

            if (knjiga == null)
            {
                return NotFound();
            }

            var korisnikId = await _context.korisnik
                .Where(k => k.korisnikEmail == userName)
                .Select(k => (int?)k.korisnikID)
                .FirstOrDefaultAsync();

            var existingEntry = _context.knjigaKorisnik
           .FirstOrDefault(ub => ub.korisnikID == korisnikId && ub.knjigaID == id);

            var flag = false;

            if (existingEntry != null)
            {
                flag = true;
            }

            var viewModel = new KnjigaDetailsViewModel
            {
                Knjiga = knjiga,
                Recenzije = knjiga.Recenzije.ToList(),
                IsInReadList = flag
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveReview(KnjigaDetailsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                viewModel.NovaRecenzija.KorisnikID = user.Id;
                viewModel.NovaRecenzija.KnjigaID = viewModel.Knjiga.knjigaID;
                _context.recenzija.Add(viewModel.NovaRecenzija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = viewModel.Knjiga.knjigaID });
            }

            var knjiga = await _context.Knjiga
                .Include(k => k.izdavac)
                .Include(k => k.AutorKnjiga)
                    .ThenInclude(ak => ak.Autor)
                .Include(k => k.Recenzije)
                    .ThenInclude(r => r.korisnik)
                .FirstOrDefaultAsync(m => m.knjigaID == viewModel.Knjiga.knjigaID);

            viewModel.Knjiga = knjiga;
            viewModel.Recenzije = knjiga.Recenzije.ToList();

            return View("Details", viewModel);
        }

        private bool KnjigaExists(int id)
        {
            return _context.Knjiga.Any(e => e.knjigaID == id);
        }

        //delete review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _context.recenzija.FindAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            _context.recenzija.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = review.KnjigaID });
        }
        public async Task<IActionResult> NewReleases()
        {
            DateTime twoMonthsAgo = DateTime.Now.AddMonths(-2);

            var applicationDbContext = _context.Knjiga.Include(k => k.izdavac).Where(k => k.datumObjavljivanjaKnj >= twoMonthsAgo);

            var knjige = _context.Knjiga.ToList();

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> MyLibrary()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var userName = user.UserName;

            var korisnikId = await _context.korisnik
                .Where(k => k.korisnikEmail == userName)
                .Select(k => (int?)k.korisnikID)
                .FirstOrDefaultAsync();
        

            var userBookIds = await _context.knjigaKorisnik
            .Where(kk => kk.korisnikID == korisnikId)
            .Select(kk => kk.knjigaID)
            .ToListAsync();

            var myBooks = await _context.Knjiga
                .Include(k => k.izdavac)
                .Where(k => userBookIds.Contains(k.knjigaID))
                .ToListAsync();

         

            return View(myBooks);

        }

        [HttpPost]
        public async Task<JsonResult> AddToRead(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            
            var userName = user.UserName;


            var korisnikId = await _context.korisnik
                .Where(k => k.korisnikEmail == userName)
                .Select(k => (int?)k.korisnikID)
                .FirstOrDefaultAsync();

            var existingEntry = _context.knjigaKorisnik
           .FirstOrDefault(ub => ub.korisnikID == korisnikId && ub.knjigaID == bookId);

            if (existingEntry != null)
            {
                return Json(new { success = false, message = "Book already in read list." });
            }

            var knjigaKorisnik = new KnjigaKorisnik
            {
                korisnikID = (int)korisnikId, 
                knjigaID = bookId, 
                vrstaListe = (VrstaListe)1
            };

            _context.knjigaKorisnik.Add(knjigaKorisnik);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Book added to read list." });
        }
    }

}