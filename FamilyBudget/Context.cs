using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace FamilyBudget
{
    public class Context : DbContext
    {
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public Context()
            : base("localsql2012")
        {
         // Database.SetInitializer<Context>(new DropCreateDatabaseAlways<Context>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Operation>().HasOptional(p => p.AccountToPut)
                .WithMany()
                .HasForeignKey(p => p.AccountToPutId);

            modelBuilder.Entity<Operation>().HasOptional(p => p.AccountToWithdraw)
                .WithMany()
                .HasForeignKey(p => p.AccountToWithdrawId);
        }
    }
}
