using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Data;

public class Category
{
    [Key]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Category name mandatory!")]
    [StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Description mandatory!")]
    public string Description { get; set; }
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}