using System.ComponentModel.DataAnnotations;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryConstants.CategoryNameMaxLength, MinimumLength =CategoryConstants.CategoryNameMinLength)]
        public string Name { get; set; }
    }
}
