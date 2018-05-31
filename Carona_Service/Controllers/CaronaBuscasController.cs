using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carona_Service.Models;
using Carona_Service.Data;
using Newtonsoft.Json;

namespace Carona_Service.Controllers
{
    public class CaronaBuscasController : Controller
    {
        private readonly Carona_ServiceContext _context;

        public CaronaBuscasController(Carona_ServiceContext context)
        {
            _context = context;
        }

        // GET: CaronaBuscas
        public async Task<IActionResult> Index()
        {
            return View(await _context.CaronaBusca.ToListAsync());
        }

        // GET: CaronaBuscas
        public async Task<string> IndexBusca(CaronaBusca carona)
        {
            var caronasOfertadas = await CaronaUtil.ConsulteCaronasOfertadasAsync(carona.Id.ToString(), _context);
            //return  RedirectToAction("IndexResultadoBusca", "CaronaOfertas", carona); // View(nameof(), model);

            var retorno = JsonConvert.SerializeObject(caronasOfertadas);
            return retorno;
        }

        // GET: CaronaBuscas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caronaBusca = await _context.CaronaBusca
                .SingleOrDefaultAsync(m => m.Id == id);
            if (caronaBusca == null)
            {
                return NotFound();
            }

            return View(caronaBusca);
        }

        // GET: CaronaBuscas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CaronaBuscas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<string> Create(string caronaJson)
        {
            try
            {
                var caronaBusca = JsonConvert.DeserializeObject<CaronaBusca>(caronaJson);
                var resultadoTask = "";
                if (!ModelState.IsValid)
                {
                    caronaBusca.Id = Guid.NewGuid();

                    var resultado = await CaronaUtil.CadastreCaronaBuscaAsync(caronaBusca, _context);
                    resultadoTask = resultado.ToString();

                    ViewBag.id = caronaBusca.Id;
                    return "Carona cadastrada com sucesso\n" + caronaBusca.Descricao;
                }

                return "Não foi possível cadastrar esta carona\nResultado: " + resultadoTask;
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                return "Ocorreu um erro: " + e.Message;
            }
        }

        // GET: CaronaBuscas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caronaBusca = await _context.CaronaBusca.SingleOrDefaultAsync(m => m.Id == id);
            if (caronaBusca == null)
            {
                return NotFound();
            }
            return View(caronaBusca);
        }

        // POST: CaronaBuscas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,IdUsuario,Descricao,HorarioPartida,HorarioChegada")] CaronaBusca caronaBusca)
        {
            if (id != caronaBusca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caronaBusca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaronaBuscaExists(caronaBusca.Id))
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
            return View(caronaBusca);
        }

        // GET: CaronaBuscas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caronaBusca = await _context.CaronaBusca
                .SingleOrDefaultAsync(m => m.Id == id);
            if (caronaBusca == null)
            {
                return NotFound();
            }

            return View(caronaBusca);
        }

        // POST: CaronaBuscas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var caronaBusca = await _context.CaronaBusca.SingleOrDefaultAsync(m => m.Id == id);
            _context.CaronaBusca.Remove(caronaBusca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaronaBuscaExists(Guid id)
        {
            return _context.CaronaBusca.Any(e => e.Id == id);
        }
    }
}
