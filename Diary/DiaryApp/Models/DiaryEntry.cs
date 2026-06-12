using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Models
{
    public class DiaryEntry
    {
        // Properties
        //[Key] 
        public int DiaryEntryId { get; set; }
        [Required(ErrorMessage = "Please enter a title!!! (Client side validation)")] // cannot be null
        [StringLength(50,MinimumLength = 2, ErrorMessage = "Title must be between 2 and 50 Characters!!! (Client side validation)")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public DateTime Created { get; set; } = DateTime.Today; 

    }
}
