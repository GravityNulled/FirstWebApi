global using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;
public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<Contact> Contacts { get; set; }
}