using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Demo_webapp.Entites;

namespace My_Demo_webapp.Data
{
    public class WebappDbContext : DbContext
    {
        public WebappDbContext(DbContextOptions<WebappDbContext> options) : base(options) { }

        public DbSet<MasUser> MasUsers { get; set; }
    }
}
