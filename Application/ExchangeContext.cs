using Microsoft.EntityFrameworkCore;

namespace Exchange
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext(DbContextOptions options) : base(options) {}

        public DbSet<Log> Logs { get; set; }
    }
}
