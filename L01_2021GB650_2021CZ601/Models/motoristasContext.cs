using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2021GB650_2021CZ601.Models
{
    public class motoristasContext : DbContext
    {
        public motoristasContext(DbContextOptions<motoristasContext> options) : base(options)
        {

        }

        public DbSet<motoristas> motoristas { get; set; }
    }
}
