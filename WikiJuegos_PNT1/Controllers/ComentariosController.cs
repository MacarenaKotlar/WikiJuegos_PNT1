using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiJuegos_PNT1.Context;
using WikiJuegos_PNT1.Models;

namespace WikiJuegos_PNT1.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly WikiJuegosDatabaseContext _context;

        public ComentariosController(WikiJuegosDatabaseContext context)
        {
            _context = context;
        }

        // GET: Comentarios
        public async Task<IActionResult> Index()
        {
            var wikiJuegosDatabaseContext = _context.Comentarios.Include(c => c.Juego);
            return View(await wikiJuegosDatabaseContext.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.Juego)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        public IActionResult Create()
        {
            ViewData["JuegoId"] = new SelectList(_context.Juegos, "Id", "Desarrollador");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Texto,Puntaje,Fecha,UsuarioNombre,JuegoId")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JuegoId"] = new SelectList(_context.Juegos, "Id", "Desarrollador", comentario.JuegoId);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }
            ViewData["JuegoId"] = new SelectList(_context.Juegos, "Id", "Desarrollador", comentario.JuegoId);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Texto,Puntaje,Fecha,UsuarioNombre,JuegoId")] Comentario comentario)
        {
            if (id != comentario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.Id))
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
            ViewData["JuegoId"] = new SelectList(_context.Juegos, "Id", "Desarrollador", comentario.JuegoId);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.Juego)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.Id == id);
        }
    }
}
