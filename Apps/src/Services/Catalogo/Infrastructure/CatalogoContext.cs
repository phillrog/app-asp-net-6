using Catalogo.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infrastructure
{
    public class CatalogoContext : DbContext
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

        public DbSet<CatalogoItem> CatalogoItems { get; set; }
    }
}
