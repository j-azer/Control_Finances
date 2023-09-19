using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data.Entities;

namespace MoneyPlus.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Active> Actives { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Typing> Typings { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Payee> Payees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}