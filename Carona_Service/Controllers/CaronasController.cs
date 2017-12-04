using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carona_Service.Models;

namespace Carona_Service.Controllers
{
    public class CaronasController : Controller
    {
        private readonly Carona_ServiceContext _context;

        public CaronasController(Carona_ServiceContext context)
        {
            _context = context;
        }

        // GET: Caronas
        public async Task<IActionResult> Index()
        {
            return View(await _context.CaronaOferta.ToListAsync());
        }

        // GET: Caronas
        public async Task<IActionResult> Consultar(string pontos)
        {
            var consulta = "SELECT * FROM CARONA";
            var resultado = _context.CaronaOferta.FromSql(consulta);
            return View("Index", await resultado.ToListAsync());
        }

        // GET: Caronas/Details/5
        public async Task<IActionResult> Detalhes(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carona = await _context.CaronaOferta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (carona == null)
            {
                return NotFound();
            }

            return View(carona);
        }

        // GET: Caronas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Caronas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarOferta([Bind("Id,IdUsuario,Descricao,HorarioChegada")] CaronaOferta carona)
        {
            if (ModelState.IsValid)
            {
                carona.Id = Guid.NewGuid();
                _context.Add(carona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carona);
        }

        // POST: Caronas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarBusca(CaronaBusca carona)
        {
            if (ModelState.IsValid)
            {
                carona.Id = Guid.NewGuid();
                _context.Add(carona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carona);
        }

        // GET: Caronas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carona = await _context.CaronaOferta.SingleOrDefaultAsync(m => m.Id == id);
            if (carona == null)
            {
                return NotFound();
            }
            return View(carona);
        }

        // POST: Caronas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Descricao,PontoPartida,PontoChegada,PontosIntermediarios,HorarioChegada")] CaronaOferta carona)
        {
            if (id != carona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaronaExists(carona.Id))
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
            return View(carona);
        }

        // GET: Caronas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carona = await _context.CaronaOferta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (carona == null)
            {
                return NotFound();
            }

            return View(carona);
        }

        // POST: Caronas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carona = await _context.CaronaOferta.SingleOrDefaultAsync(m => m.Id == id);
            _context.CaronaOferta.Remove(carona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaronaExists(Guid id)
        {
            return _context.CaronaOferta.Any(e => e.Id == id);
        }
    }
}
