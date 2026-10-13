using System.ComponentModel.DataAnnotations;

namespace TaskList.DTOs
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; } = String.Empty;

    }
}
