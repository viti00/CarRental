using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CarRental.Data.ModelsConstnants;

namespace CarRental.Data.Models
{
    [Table("Categories", Schema = "19118076")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryConstants.CategoryNameMaxLength, MinimumLength =CategoryConstants.CategoryNameMinLength)]
        public string Name { get; set; }

        public DateTime LastModified_19118076 { get; set; }
    }
}
