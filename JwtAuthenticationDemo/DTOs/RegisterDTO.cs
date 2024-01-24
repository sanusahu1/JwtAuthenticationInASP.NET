
using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationDemo.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required,Compare(nameof(Password)) ,DataType(DataType.Password)]
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
