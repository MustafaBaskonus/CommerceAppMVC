using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record UserDto
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "UserName Is required.")]
        public string UserName { get; init; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number Is required.")]
        public string PhoneNumber { get; init; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "EMail Is required.")]
        public string EMail { get; init; }
        public HashSet<String> Roles { get; set; } = new HashSet<string>();
    }
}