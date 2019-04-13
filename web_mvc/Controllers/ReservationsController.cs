using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_mvc.Data;
using web_mvc.Models;

namespace web_mvc.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Reservation.Include(r => r.figurine);

            List<Reservation> reservations = await applicationDbContext.ToListAsync();
            foreach (Reservation reservation in reservations)
            {
                if (DateTime.Compare(reservation.date_expiration, DateTime.Now) <= 0)
                {
                    List<Figurine> figurines = _context.Figurine.ToList();

                    foreach (Figurine figurine in figurines)
                    {
                        if (figurine.Id.Equals(reservation.FigurineId))
                        {
                            figurine.quantite_magasin += reservation.quantite;
                            _context.Reservation.Remove(reservation);
                            await _context.SaveChangesAsync();
                            break;
                        }
                    }
                }
            }

            var reserver =  applicationDbContext.Where(r => r.achete == false);

            if (User.IsInRole("Customer"))
            {
                string userId = _userManager.GetUserId(HttpContext.User);
                return View(reserver.Where(r => r.UserId == userId));
            }
           
            return View(await reserver.ToListAsync());

        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.figurine)
                .FirstOrDefaultAsync(m => m.Id == id);

            List<Figurine> figurines = _context.Figurine.ToList();

            foreach (Figurine figurine in figurines)
            {
                if (figurine.Id.Equals(reservation.FigurineId))
                {
                    ViewBag.Figurine = figurine.Nom;
                }
            }

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["FigurineId"] = new SelectList(_context.Figurine, "Id", "Nom");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,quantite,FigurineId")] Reservation reservation)
        {
            List<Figurine> figurines = _context.Figurine.ToList();

            foreach(Figurine figurine in figurines)
            {
                if (figurine.Id.Equals(reservation.FigurineId))
                {
                    if (figurine.quantite_magasin + figurine.quantite_stock < reservation.quantite || reservation.quantite == 0)
                    {
                        ViewData["FigurineId"] = new SelectList(_context.Figurine, "Id", "Nom");
                        return View();
                    }
                    if (figurine.quantite_stock < reservation.quantite)
                    {
                        figurine.quantite_magasin = figurine.quantite_magasin - (reservation.quantite - figurine.quantite_stock);
                        figurine.quantite_stock = 0;
                    }
                    else
                    {
                        figurine.quantite_stock = figurine.quantite_stock - reservation.quantite;
                    }
                    reservation.montant = figurine.prix_ttc * reservation.quantite;
                }
            }

            

            reservation.UserId = _userManager.GetUserId(HttpContext.User);

            reservation.date_expiration = DateTime.Now.AddDays(7);
           

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.FigurineId = new SelectList(_context.Figurine, "Id", "Nom", reservation.FigurineId);

            return View(reservation);
        }


        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            List<Figurine> figurines = _context.Figurine.ToList();
            foreach(Figurine figurine in figurines)
            {
                if (figurine.Id.Equals(reservation.FigurineId))
                {
                    ViewBag.Figurine = figurine.Nom;
                }
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,achete,quantite,date_expiration,FigurineId")] Reservation reservation)
        {

            if (id != reservation.Id)
            {
                return NotFound();
            }

            List<Figurine> figurines = _context.Figurine.ToList();
            foreach (Figurine figurine in figurines)
            {
                if (figurine.Id.Equals(reservation.FigurineId))
                {
                    reservation.montant = reservation.quantite * figurine.prix_ttc;
                }
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["FigurineId"] = new SelectList(_context.Figurine, "Id", "Nom", reservation.FigurineId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.figurine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            List<Figurine> figurines = _context.Figurine.ToList();

            foreach (Figurine figurine in figurines)
            {
                if (figurine.Id.Equals(reservation.FigurineId))
                {
                    figurine.quantite_magasin += reservation.quantite;
                    _context.Reservation.Remove(reservation);
                    await _context.SaveChangesAsync();
                    break;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
