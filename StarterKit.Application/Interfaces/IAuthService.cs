using StarterKit.Application.DTOs;
using StarterKit.Domain.Entities;

namespace StarterKit.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterUserDto dto);
        Task<AuthResult> LoginAsync(LoginUserDto dto);
    }
}