using Microsoft.AspNetCore.Mvc;
using WikiJuegos_PNT1.Context;
using WikiJuegos_PNT1.Models;
using Microsoft.EntityFrameworkCore;
using WikiJuegos_PNT1.ViewModels;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WikiJuegos_PNT1.Controllers
{
    public class AccesoController : Controller
    {
        private readonly WikiJuegosDatabaseContext _context;
        public AccesoController(WikiJuegosDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroVM modelo)
        {
            if(modelo.Contra != modelo.ConfirmarContra)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                Nombre = modelo.Nombre,
                Contra = modelo.Contra
            };

            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            if(usuario.Id != 0) return RedirectToAction("Login", "Acceso");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuario? usuario = await _context.Usuario
                .Where(u =>
                u.Nombre == modelo.Nombre &&
                u.Contra == modelo.Contra
                ).FirstOrDefaultAsync();

            if(usuario == null)
            {
                ViewData["Mensaje"] = "Nombre de usuario o contraseña incorrecta";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.NameIdentifier, (usuario.Id).ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
