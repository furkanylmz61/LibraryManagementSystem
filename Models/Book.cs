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
    public string Author { get; set; }
    public string? PublishDate { get; set; }
    public int PageCount { get; set;}
    public string? PublisherCompany { get; set; }
    public string? ISBN { get; set; }
    public string? Genre { get; set; }
    public string? CoverImage { get; set; }
    public string? Language { get; set; }
    public string? Edition { get; set; } 
    public int NumberOfCopies { get; set; }
    public int AvailableCopies { get; set; } 
    public string? ShelfLocation { get; set; }
    
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}
