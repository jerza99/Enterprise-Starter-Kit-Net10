using Microsoft.AspNetCore.Identity;
using StarterKit.Application.DTOs;
using StarterKit.Application.Interfaces;
using StarterKit.Domain.Entities;

namespace StarterKit.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthResult> RegisterAsync(RegisterUserDto dto)
        {
            // Validar que las contraseñas coincidan
            if (dto.Password != dto.ConfirmPassword)
                return AuthResult.FailureResult(new[] { "Las contraseñas no coinciden" });

            // Crear nueva instancia de User
            var user = new User
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber
            };

            // Intentar crear el usuario
            var result = await _userManager.CreateAsync(user, dto.Password);

            // Si falla, retornar errores
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return AuthResult.FailureResult(errors);
            }

            // Retornar éxito
            return AuthResult.SuccessResult(user);
        }

        public async Task<AuthResult> LoginAsync(LoginUserDto dto)
        {
            // Buscar usuario por email o username
            var user = await _userManager.FindByEmailAsync(dto.EmailOrUserName) 
                ?? await _userManager.FindByNameAsync(dto.EmailOrUserName);

            // Si no existe, retornar error
            if (user == null)
                return AuthResult.FailureResult(new[] { "Usuario o contraseña incorrectos" });

            // Intentar hacer login
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                dto.Password,
                dto.RememberMe,
                lockoutOnFailure: false);

            // Si falla, retornar error
            if (!result.Succeeded)
            {
                // Determinar el tipo de error
                if (result.IsLockedOut)
                    return AuthResult.FailureResult(new[] { "Tu cuenta ha sido bloqueada temporalmente" });
                else if (result.IsNotAllowed)
                    return AuthResult.FailureResult(new[] { "No tienes permiso para iniciar sesión" });
                else
                    return AuthResult.FailureResult(new[] { "Usuario o contraseña incorrectos" });
            }

            // Retornar éxito
            return AuthResult.SuccessResult(user);
        }
    }
}