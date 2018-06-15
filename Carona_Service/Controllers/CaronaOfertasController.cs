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
    public class CaronaOfertasController : Controller
    {
        private readonly Carona_ServiceContext _context;

        private ServiceResult _resultado;

        public CaronaOfertasController(Carona_ServiceContext context)
        {
            _context = context;
        }

        // GET: CaronaOfertas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.CaronaOferta.ToListAsync());
        }

        // GET: CaronaBuscas
        [HttpGet]
        public async Task<string> IndexBusca(CaronaOferta carona)
        {
            var caronasBuscadas = await CaronaUtil.ConsulteCaronasBuscadasAsync(carona.Id.ToString(), _context);
            //return  RedirectToAction("IndexResultadoBusca", "CaronaOfertas", carona); // View(nameof(), model);

            var retorno = JsonConvert.SerializeObject(caronasBuscadas);
            return retorno;
        }

        [HttpGet]
        public async Task<IActionResult> IndexResultadoBusca(string referencia)
        {
            //var id = (Guid)ViewBag.id;
            var model = await CaronaUtil.ConsulteCaronasOfertadasAsync(referencia, _context);
            return View(nameof(Index), model);
        }

        // GET: CaronaOfertas/Details/5
        [HttpGet]
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CaronaOfertas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ServiceResult> Create(string caronaJson) //[Bind("Id,IdUsuario,Descricao,HorarioPartida,HorarioChegada,PontoPartida,PontoChegada")]
        {
            try
            {
                var caronaOferta = JsonConvert.DeserializeObject<CaronaOferta>(caronaJson);
                var resultadoTask = "";
                if (ModelState.IsValid)
                {
                    caronaOferta.Id = Guid.NewGuid();
                    var resultado = await CaronaUtil.CadastreCaronaOfertaAsync(caronaOferta, _context);
                    resultadoTask = resultado.ToString();

                    //_context.Add(caronaOferta);
                    //await _context.SaveChangesAsync();

                    _resultado = new ServiceResult(true, JsonConvert.SerializeObject(caronaOferta));
                    return _resultado;
                }
                _resultado = new ServiceResult(false, JsonConvert.SerializeObject(ModelState));
                return _resultado;
            }
            catch(Exception e)
            {
                while(e.InnerException != null)
                {
                    e = e.InnerException;
                }

                _resultado = new ServiceResult(false, JsonConvert.SerializeObject(e));
                return _resultado;
            }
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
        [HttpPut]
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
