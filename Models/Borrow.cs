using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models;

public class Borrow
{
    [Key]
    public int BorrowId { get; set; }

    public Guid BookId { get; set; }
    [ForeignKey("BookId")]
    public virtual Book Book { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}