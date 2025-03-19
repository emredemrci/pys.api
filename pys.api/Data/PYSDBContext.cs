using Microsoft.EntityFrameworkCore;
using pys.api.Entities;
using pys.api.Models; 

namespace pys.api.Data
{
    public class PYSDBContext : DbContext
    {
        public PYSDBContext(DbContextOptions<PYSDBContext> options) : base(options) { }

        public virtual DbSet<Personnel> Personnel { get; set; }
    }
}
