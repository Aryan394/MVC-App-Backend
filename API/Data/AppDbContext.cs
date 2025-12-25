using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser>  Users { get; set; } // represents the table name inside the db
    public DbSet<Member> Members { get; set; }
    public DbSet<Photos> Photos { get; set; }
}