using System.ComponentModel.DataAnnotations.Schema;

namespace ToastyBudgets.Entities;

public class Transaction
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public int AccountId { get; set; }

    [ForeignKey("AccountId")]
    public Account? Account { get; set; }
}
