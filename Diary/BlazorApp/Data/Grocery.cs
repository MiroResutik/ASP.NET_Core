using System.ComponentModel.DataAnnotations;
namespace BlazorApp.Data

{
    public class Grocery
    {
        [Required] // Form validation
        [StringLength(15, ErrorMessage ="Name should be less than 15 characters!!!")]
        public string Name { get; set; }
        [Required] // Form validation
        [Range(1, 1000, ErrorMessage = "Valid price range is between 1 - 1000!!!")]
        public float Price { get; set; }

    }
}
