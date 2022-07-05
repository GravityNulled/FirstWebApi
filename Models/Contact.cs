global using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Contact
{
    public Guid Id { get; set; }
    [Required]
    public string? FullName { get; set; }
    [Required]
    public string? Email { get; set; }         
    public string? Address { get; set; }
    public int Phone { get; set; }
}