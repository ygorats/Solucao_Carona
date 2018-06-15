using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Carona_Service.Models;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Carona_Service.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private readonly Carona_ServiceContext _context;

        public UsuariosController(Carona_ServiceContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        // GET: Usuarios/Details/5
        [HttpGet("{id}")]
        [ActionName("Details")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [HttpGet("Create")]
        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Cadastra um usuário.
        /// </summary>
        /// <param name="usuarioJson">O json com os dados daquele usuário.</param>
        /// <returns>O resultado do serviço.</returns>
		[HttpPost("Create")]
        public async Task<ServiceResult> Create(string usuarioJson)
        {
            ServiceResult resultado;

            try
            {
                var usuario = JsonConvert.DeserializeObject<Usuario>(usuarioJson);
                var verificacaoDeUsuario = _context.Usuario.FirstOrDefault(user => user.Email == usuario.Email);
                if (verificacaoDeUsuario != null)
                {
                    CopieNovosDadosDeUsuario(verificacaoDeUsuario, usuario);
                    _context.Usuario.Update(verificacaoDeUsuario);
                    await _context.SaveChangesAsync();
                    resultado = new ServiceResult(true, JsonConvert.SerializeObject(verificacaoDeUsuario));
                    return resultado;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        //usuario.Id = Guid.NewGuid();
                        _context.Add(usuario);
                        await _context.SaveChangesAsync();
                        resultado = new ServiceResult(true, JsonConvert.SerializeObject(usuario));
                        return resultado;
                    }

                    resultado = new ServiceResult(false, JsonConvert.SerializeObject(ModelState));
                    return resultado;
                }

            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }

                System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create) + "/ErrorLog", JsonConvert.SerializeObject(e));
                resultado = new ServiceResult(false, JsonConvert.SerializeObject(e));
                return resultado;
            }
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Email,Cpf,Telefone")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(Guid id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        private void CopieNovosDadosDeUsuario(Usuario antigo, Usuario novo)
        {
            antigo.Nome = novo.Nome;
            antigo.Gender = novo.Gender;
            antigo.Telefone = novo.Telefone;
            antigo.UrlFoto = novo.UrlFoto;
        }
    }
}
