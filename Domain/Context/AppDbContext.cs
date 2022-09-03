using AFEXChile.Components;
using AFEXChile.Domain.Entity;
using System.Data.Entity;

namespace AFEXChile.Domain.Context
{
    public class AppDbContext : DbContext
    {
        public virtual System.Data.Entity.DbSet<Videos> Videos { get; set; }
        public AppDbContext()
           : base(Credentials.ConnectionString)
        {
            Database.SetInitializer<DbContext>(null);
        }
    }
}