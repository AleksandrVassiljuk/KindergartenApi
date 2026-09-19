using KindergartenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KindergartenApi.Data
{
    public class KindergartenDbContext : DbContext
    {
        public KindergartenDbContext(DbContextOptions<KindergartenDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kindergarten> Kindergartens { get; set; }
    }
}