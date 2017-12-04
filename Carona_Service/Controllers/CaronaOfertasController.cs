﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carona_Service.Models;
using Carona_Service.Data;

namespace Carona_Service.Controllers
{
    public class CaronaOfertasController : Controller
    {
        private readonly Carona_ServiceContext _context;

        public CaronaOfertasController(Carona_ServiceContext context)
        {
            _context = context;
        }

        // GET: CaronaOfertas
        public async Task<IActionResult> Index()
        {
            return View(await _context.CaronaOferta.ToListAsync());
        }

        public async Task<IActionResult> IndexResultadoBusca(string referencia)
        {
            //var id = (Guid)ViewBag.id;
            var model = await CaronaUtil.ConsulteCaronasOfertadasAsync(referencia, _context);
            return View(nameof(Index), model);
        }

        // GET: CaronaOfertas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caronaOferta = await _context.CaronaOferta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (caronaOferta == null)
            {
                return NotFound();
            }

            return View(caronaOferta);
        }

        // GET: CaronaOfertas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CaronaOfertas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,Descricao,HorarioPartida,HorarioChegada")] CaronaOferta caronaOferta)
        {
            if (ModelState.IsValid)
            {
                caronaOferta.Id = Guid.NewGuid();
                _context.Add(caronaOferta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caronaOferta);
        }

        // GET: CaronaOfertas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caronaOferta = await _context.CaronaOferta.SingleOrDefaultAsync(m => m.Id == id);
            if (caronaOferta == null)
            {
                return NotFound();
            }
            return View(caronaOferta);
        }

        // POST: CaronaOfertas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdUsuario,Descricao,HorarioPartida,HorarioChegada")] CaronaOferta caronaOferta)
        {
            if (id != caronaOferta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caronaOferta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaronaOfertaExists(caronaOferta.Id))
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
            return View(caronaOferta);
        }

        // GET: CaronaOfertas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caronaOferta = await _context.CaronaOferta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (caronaOferta == null)
            {
                return NotFound();
            }

            return View(caronaOferta);
        }

        // POST: CaronaOfertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var caronaOferta = await _context.CaronaOferta.SingleOrDefaultAsync(m => m.Id == id);
            _context.CaronaOferta.Remove(caronaOferta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaronaOfertaExists(Guid id)
        {
            return _context.CaronaOferta.Any(e => e.Id == id);
        }
    }
}