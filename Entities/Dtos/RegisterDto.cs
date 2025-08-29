using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "Username is reqired.")]
        public string UserName { get; init; }
        [Required(ErrorMessage = "EMail is reqired.")]
        public string EMail { get; init; }
        [Required(ErrorMessage = "Password is reqired.")]
        public string Password { get; init; }
    }
}