
namespace WebApi.Models;

public class AddContactRequest
{
    [Required]
    public string? FullName { get; set; }
    [Required]
    public string? Email { get; set; }         
    public string? Address { get; set; }
    public int Phone { get; set; }
}