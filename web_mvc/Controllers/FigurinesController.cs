using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_mvc.Data;
using web_mvc.Models;

namespace web_mvc.Controllers
{
    public class FigurinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FigurinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Figurines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Figurine.ToListAsync());
        }

        // GET: Figurines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var figurine = await _context.Figurine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (figurine == null)
            {
                return NotFound();
            }

            List<Marque> MarquesList = _context.Marque.ToList();
            List<Categorie> CategoriesList = _context.Categorie.ToList();

            foreach(Marque marque in MarquesList)
            {
                if (marque.Id.Equals(figurine.MarqueId))
                {
                    ViewBag.Marque = marque.Nom;
                    break;
                }
            }

            foreach (Categorie categorie in CategoriesList)
            {
                if (categorie.Id.Equals(figurine.CategorieId))
                {
                    ViewBag.Categorie = categorie.nom;
                    break;
                }
            }

            return View(figurine);
        }

        // GET: Figurines/Create
        public IActionResult Create()
        {
            ViewBag.MarquesList = _context.Marque.ToList();
            ViewBag.CategoriesList = _context.Categorie.ToList();
            return View();
        }

        // POST: Figurines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,prix_ttc,quantite_magasin,quantite_stock,date_parution,nb_exemplaires,poids,largeur,hauteur,longueur,reference,description,MarqueId,CategorieId")] Figurine figurine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(figurine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(figurine);
        }

        // GET: Figurines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var figurine = await _context.Figurine.FindAsync(id);
            if (figurine == null)
            {
                return NotFound();
            }
            ViewBag.MarquesList = _context.Marque.ToList();
            ViewBag.CategoriesList = _context.Categorie.ToList();

           

            return View(figurine);
        }

        // POST: Figurines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,prix_ttc,quantite_magasin,quantite_stock,date_parution,nb_exemplaires,poids,largeur,hauteur,longueur,reference,description,MarqueId,CategorieId")] Figurine figurine)
        {
            if (id != figurine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(figurine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FigurineExists(figurine.Id))
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
            return View(figurine);
        }

        // GET: Figurines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var figurine = await _context.Figurine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (figurine == null)
            {
                return NotFound();
            }

            List<Marque> MarquesList = _context.Marque.ToList();
            List<Categorie> CategoriesList = _context.Categorie.ToList();

            foreach (Marque marque in MarquesList)
            {
                if (marque.Id.Equals(figurine.MarqueId))
                {
                    ViewBag.Marque = marque.Nom;
                    break;
                }
            }

            foreach (Categorie categorie in CategoriesList)
            {
                if (categorie.Id.Equals(figurine.CategorieId))
                {
                    ViewBag.Categorie = categorie.nom;
                    break;
                }
            }

            return View(figurine);
        }

        // POST: Figurines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var figurine = await _context.Figurine.FindAsync(id);
            _context.Figurine.Remove(figurine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FigurineExists(int id)
        {
            return _context.Figurine.Any(e => e.Id == id);
        }
    }
}
