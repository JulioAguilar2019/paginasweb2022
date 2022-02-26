using equiposWebAPi.Models;
using Microsoft.EntityFrameworkCore;


namespace equiposWebAPi
{
    public class prestamosContext : DbContext
    {
        public prestamosContext(DbContextOptions<prestamosContext> options) : base(options)
        {

        }

        public DbSet<equipos> equipos { get; set; }

    }
}
