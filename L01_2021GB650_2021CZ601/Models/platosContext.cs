using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2021GB650_2021CZ601.Models
{
    public class platosContext : DbContext
    {
        public platosContext(DbContextOptions<platosContext> options) : base(options)
        {

        }

        public DbSet<platos>  platos { get; set; }
    }
}
