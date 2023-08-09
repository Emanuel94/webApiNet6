using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Owner>? Owners { set; get; }

        public DbSet<Account>? Account { set; get; }    

        public DbSet<User>? Users { set; get; }
    }
}