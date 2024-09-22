using Microsoft.EntityFrameworkCore;
using UploadService.Models;

namespace UploadService.Data;

public class UploadServiceContext : DbContext
{
    public UploadServiceContext(DbContextOptions<UploadServiceContext> options) : base(options) { }
    public DbSet<Account> Account { get; set; }
}