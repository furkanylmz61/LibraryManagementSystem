using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models;

public class Book
{
    [Key]
    public Guid BookId { get; set;}
    [Required(ErrorMessage = "Name field required! ")]
    [StringLength(100)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Author field required!")]
    [StringLength(100)]
    public string Author { get; set; }
    [StringLength(100)]
    public string? PublishDate { get; set; }
    [Range(1,3000, ErrorMessage = "Number of pages should be between 1 and 3000")]
    public int PageCount { get; set;}
    [StringLength(50)]
    public string? PublisherCompany { get; set; }
    [StringLength(40)]
    public string? ISBN { get; set; }
    [StringLength(40)]
    public string? Genre { get; set; }
    public string? CoverImage { get; set; }
    [StringLength(40)]
    public string? Language { get; set; }
    [StringLength(40)]
    public string? Edition { get; set; } 
    [Range(1,50000, ErrorMessage = "Number of copies should be between 1 and 50000")]
    public int NumberOfCopies { get; set; }
    [Range(1,50000, ErrorMessage = "Available copies should be between 1 and 50000")]
    public int AvailableCopies { get; set; } 
    [StringLength(10)]
    public string? ShelfLocation { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    public Guid? CategoryId { get; set; }
    public  Category? Category { get; set; }
}
