using Microsoft.EntityFrameworkCore;
using API.Entities;
namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) // Końcówka służy do automatycznej inicjalizacji połączenia z bazą na podstawie podanego parametru
    {
    }
    public DbSet<AppUser> Users { get; set; } // Tutaj tworzymy instacje która będzie obsługiwać zapytania naszych użytkowników 
}
