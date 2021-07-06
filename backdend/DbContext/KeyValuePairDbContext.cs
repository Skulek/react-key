using Microsoft.EntityFrameworkCore;

namespace backend.DbContext
{
    public class KeyValuePairDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public KeyValuePairDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Entities.KeyValuePair> KeyValuePairs { get; set; }
    }
}
