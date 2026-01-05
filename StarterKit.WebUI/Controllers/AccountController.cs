using Microsoft.AspNetCore.Mvc;
using StarterKit.Application.DTOs;
using StarterKit.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using StarterKit.Domain.Entities;

namespace StarterKit.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAuthService authService, SignInManager<User> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            // Guardar la URL de retorno en ViewBag para usarla en la vista
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            // Validar el modelo
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            // Intentar registrar el usuario
            var result = await _authService.RegisterAsync(dto);

            // Si falla, mostrar errores
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }

            // Si es exitoso, redirigir al Login
            TempData["SuccessMessage"] = "Registro exitoso. Por favor inicia sesión.";
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDto dto, string? returnUrl = null)
        {
            // Validar el modelo
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            // Intentar hacer login
            var result = await _authService.LoginAsync(dto);

            // Si falla, mostrar errores
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                ViewBag.ReturnUrl = returnUrl;
                return View(dto);
            }

            // Si es exitoso, redirigir
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}