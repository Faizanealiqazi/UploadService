using Microsoft.EntityFrameworkCore;
using UploadService.Models;

namespace UploadService.Data;

public class UploadServiceContext : DbContext
{
    public UploadServiceContext(DbContextOptions<UploadServiceContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .ToTable("ACCOUNT");

        modelBuilder.Entity<Account>()
            .Property(a => a.Id)
            .HasColumnName("ID");

        modelBuilder.Entity<Account>()
            .Property(a => a.UserId)
            .HasColumnName("USER_ID");

        modelBuilder.Entity<Account>()
            .Property(a => a.AccountName)
            .HasColumnName("ACCOUNT_NAME");

        modelBuilder.Entity<Account>()
            .Property(a => a.Balance)
            .HasColumnName("BALANCE");

        modelBuilder.Entity<Account>()
            .Property(a => a.Currency)
            .HasColumnName("CURRENCY");

        modelBuilder.Entity<Account>()
            .Property(a => a.Status)
            .HasColumnName("STATUS");

        modelBuilder.Entity<Account>()
            .Property(a => a.CreateAt)
            .HasColumnName("CREATED_AT");

        modelBuilder.Entity<Account>()
            .Property(a => a.UpdateAt)
            .HasColumnName("UPDATED_AT");

        modelBuilder.Entity<Account>()
            .Property(a => a.Email)
            .HasColumnName("EMAIL");

        modelBuilder.Entity<Account>()
            .Property(a => a.Phone)
            .HasColumnName("PHONE");
    }
}