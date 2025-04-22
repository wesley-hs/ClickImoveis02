using ClickImoveis01.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ClickImoveis01.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dados = await _context.Clientes.ToListAsync();


            return View(dados);
        }

        private string HashPassword(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                // Gerar hash da senha antes de salvar
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cliente.Senha));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    cliente.Senha = builder.ToString();
                }

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Clientes.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cliente usuario)
        {
            if (id != usuario.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Hash na senha nova antes de atualizar
                usuario.Senha = HashPassword(usuario.Senha);

                _context.Clientes.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Clientes.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Clientes.FindAsync(id);

            if (dados == null)
                return NotFound();

            return View(dados);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfig(int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Clientes.FindAsync(id);

            if (dados == null)
                return NotFound();

            _context.Clientes.Remove(dados);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
