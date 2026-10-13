using System.ComponentModel.DataAnnotations;

namespace TaskList.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;

    }
}
