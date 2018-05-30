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
        public async Task<string> Create(string caronaOfertaJson) //[Bind("Id,IdUsuario,Descricao,HorarioPartida,HorarioChegada,PontoPartida,PontoChegada")]
        {
            try
            {
                var caronaOferta = JsonConvert.DeserializeObject<CaronaOferta>(caronaOfertaJson);
                if (ModelState.IsValid)
                {
                    caronaOferta.Id = Guid.NewGuid();
                    _context.Add(caronaOferta);
                    await _context.SaveChangesAsync();
                    
                    return "Carona cadastrada com sucesso\n" + caronaOferta.Descricao + "\n" + caronaOferta.PontoPartida.ToString();
                    
                }
                return "Não foi possível cadastrar esta carona";
            }
            catch(Exception e)
            {
                while(e.InnerException != null)
                {
                    e = e.InnerException;
                }
                return "Ocorreu um erro: " + e.Message;
            }
        }

        [HttpPost]
        public string Teste()
        {
            //var teste = JsonConvert.DeserializeObject<CaronaOferta>(param);
            //var formatada = string.Format("\nDescr: {0} \nLat:{1} \nLong:{2}", param.Descricao, param.PontoPartida.Latitude.ToString(), param.PontoPartida.Longitude.ToString());
            //var form2 = string.Format("Null:{0}\nToString:{1}", (param == null).ToString(), param.ToString());

            return "Mensagem enviada e recebida:\n " + "sem parametros" + ".\n fim da msg";
        }

        [HttpPost]
        public string TesteStr(string caronaOfertaJson)
        {
            try
            {
                //var teste = JsonConvert.DeserializeObject<CaronaOferta>(param);
                //var formatada = string.Format("\nDescr: {0} \nLat:{1} \nLong:{2}", teste.Descricao, teste.PontoPartida.Latitude.ToString(), teste.PontoPartida.Longitude.ToString());

                return "Mensagem enviada e recebida: \n" + caronaOfertaJson + "\n. fim da msg";
            }
            catch(Exception e)
            {
                return "Mensagem enviada e erro: \n" + e.Message + "\n. fim da msg";
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
