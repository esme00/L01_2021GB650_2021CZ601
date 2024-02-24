using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2021GB650_2021CZ601.Models
{
    public class clientesContext : DbContext
    {
        public clientesContext(DbContextOptions<clientesContext> options) : base(options)
        {

        }

        public DbSet<clientes> clientes { get; set; }
    }
}
