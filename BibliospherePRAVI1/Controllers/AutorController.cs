using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliospherePRAVI1.Data;
using BibliospherePRAVI1.Models;
using BibliospherePRAVI1.Data;
using BibliospherePRAVI1.Models;

namespace BibliospherePRAVI1.Controllers
{
    public class AutorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _enviroment;

        public AutorController(ApplicationDbContext context, IWebHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        // GET: Autor
        public async Task<IActionResult> Index()
        {
            return View(await _context.autor.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string authorSrch)
        {
            ViewData["autor"] = authorSrch;

            var qry = from x in _context.autor select x;
            if (!String.IsNullOrEmpty(authorSrch))
            {
                qry = qry.Where(x => x.autorIme.Contains(authorSrch) || x.autorPrezime.Contains(authorSrch));
            }

            return View(await qry.AsNoTracking().ToListAsync());
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autor
                .Include(a => a.AutorKnjiga)
                .ThenInclude(ak => ak.Knjiga)
                .FirstOrDefaultAsync(m => m.autorID == id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHssfff");
            newFileName += Path.GetExtension(autor.ImageFile!.FileName);

            string imageFullPath = _enviroment.WebRootPath + "/autori/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {
                autor.ImageFile.CopyTo(stream);
            }
            Autor noviAut = new Autor()
            {
                autorIme = autor.autorIme,
                autorPrezime = autor.autorPrezime,
                autorDatumRod = autor.autorDatumRod,
                autorSlika = newFileName,
            };

            _context.autor.Add(noviAut);
            _context.SaveChanges();

            return RedirectToAction("Index", "autor");
        }



        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autor.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            Autor noviAutor = new Autor()
            {
                autorIme = autor.autorIme,
                autorPrezime = autor.autorPrezime,
                autorDatumRod = autor.autorDatumRod,
                ImageFile = autor.ImageFile
            };
            return View(autor);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("autorID,autorIme,autorPrezime,autorSlika,autorDatumRod")] Autor autor)
        {
            if (id != autor.autorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.autorID))
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
            return View(autor);
        }

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autor
                .FirstOrDefaultAsync(m => m.autorID == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.autor.FindAsync(id);
            if (autor != null)
            {
                _context.autor.Remove(autor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
            return _context.autor.Any(e => e.autorID == id);
        }
    }
}
