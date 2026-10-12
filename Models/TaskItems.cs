using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public class TaskItems
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)] 
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^(Pending|InProgress|Completed)$")]
        public string Status { get; set; } = string.Empty;
    }
}
