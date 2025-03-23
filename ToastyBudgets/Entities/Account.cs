using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToastyBudgets.Entities;

public class Account
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public string IdentityUserId { get; set; } = string.Empty;

    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }
}
