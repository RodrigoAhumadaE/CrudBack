#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CrudBack.Models;

public class MyContext : DbContext{
    public DbSet<Usuario> Usuarios {get; set;}

    public MyContext(DbContextOptions options) : base(options){}
    
}