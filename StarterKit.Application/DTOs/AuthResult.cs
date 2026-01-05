using StarterKit.Domain.Entities;

namespace StarterKit.Application.DTOs
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public User? User { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        private AuthResult(bool success, User? user, IEnumerable<string> errors)
        {
            Success = success;
            User = user;
            Errors = errors;
        }

        public static AuthResult SuccessResult(User user)
        {
            return new AuthResult(true, user, new List<string>());
        }

        public static AuthResult FailureResult(IEnumerable<string> errors)
        {
            return new AuthResult(false, null, errors);
        }
    }
}