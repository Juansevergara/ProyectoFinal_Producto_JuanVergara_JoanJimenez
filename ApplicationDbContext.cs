using Microsoft.EntityFrameworkCore;
using PRODUCTO.entidades;

namespace PRODUCTO;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Producto> Productos { get; set; }

}
