using BankingApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SenderUser)
                .WithMany(u => u.SentTransactions)
                .HasForeignKey(t => t.SenderUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ReceiverUser)
                .WithMany(u => u.ReceivedTransactions)
                .HasForeignKey(t => t.ReceiverUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
