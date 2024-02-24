using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2021GB650_2021CZ601.Models
{
    public class pedidosContext : DbContext
    {
        public pedidosContext(DbContextOptions<pedidosContext> options) : base(options)
        {

        }

        public DbSet<pedidos> pedidos { get; set; }
    }
}
