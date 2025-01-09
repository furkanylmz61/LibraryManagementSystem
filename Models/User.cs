using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    [Required(ErrorMessage = "User name mandatory!!")]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email adress mandatory!")]
    [EmailAddress]
    public string Email { get; set; }

    // Şifre güvenlik için hashing ve salting işlemleri yapıldıktan sonra saklanır.
    [Required]
    public string PasswordHash { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime MembershipDate { get; set; }

    // Bir kullanıcının ödünç aldığı kitaplar
    public virtual ICollection<Borrow> BorrowedBooks { get; set; } = new List<Borrow>();}