using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebHostApplication.Data;
using WebHostApplication.Models.Usuarios;
using WebHostApplication.ViewModels.Usuarios;

namespace WebHostApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly WebHostDbcontext _context;

        public AuthController(WebHostDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.FechaHora = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy, HH:mm:ss", new System.Globalization.CultureInfo("es-ES"));
            if (HttpContext.Session.GetString("Usuario") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // ------------------------------------------------------
        // POST: /Auth/Login
        // ------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario no encontrado.";
                return View(dto);
            }

            var passwordHashIngresado = HashPassword(dto.Password);

            if (!string.Equals(usuario.Password, passwordHashIngresado, StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "Contraseña incorrecta.";
                return View(dto);
            }

            // Guardar sesión
            HttpContext.Session.SetString("Usuario", usuario.Username);
            HttpContext.Session.SetString("Rol", usuario.Rol ?? "Usuario");

            return RedirectToAction("Index", "Home");
        }

        // ------------------------------------------------------
        // GET: /Auth/Register
        // ------------------------------------------------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ------------------------------------------------------
        // POST: /Auth/Register
        // ------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            bool existe = await _context.Usuarios.AnyAsync(u => u.Username == usuario.Username);
            if (existe)
            {
                ViewBag.Error = "El nombre de usuario ya existe.";
                return View(usuario);
            }

            usuario.Password = HashPassword(usuario.Password);
            usuario.Rol ??= "Usuario";

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            ViewBag.Mensaje = "Usuario registrado correctamente. Ahora puede iniciar sesión.";
            return RedirectToAction("Login");
        }

        // ------------------------------------------------------
        // GET: /Auth/Logout
        // ------------------------------------------------------
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ------------------------------------------------------
        // Método para encriptar contraseña
        // ------------------------------------------------------
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}

