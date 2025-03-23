using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToastyBudgets.Entities;

namespace ToastyBudgets.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transaction> Transactions { get; set; }
}
