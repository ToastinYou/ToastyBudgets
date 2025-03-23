using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToastyBudgets.Data;
using ToastyBudgets.Entities;

namespace ToastyBudgets.Controllers;

public class TransactionsController(ApplicationDbContext context) : Controller
{
    // GET: Transactions
    public async Task<IActionResult> Index()
    {
        Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Transaction, Account?> applicationDbContext = context.Transactions.Include(t => t.Account);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Transactions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Transaction? transaction = await context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: Transactions/Create
    public IActionResult Create()
    {
        ViewData["AccountId"] = new SelectList(context.Accounts, "Id", "Id");
        return View();
    }

    // POST: Transactions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Description,Amount,Date,AccountId")] Transaction transaction)
    {
        if (ModelState.IsValid)
        {
            context.Add(transaction);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["AccountId"] = new SelectList(context.Accounts, "Id", "Id", transaction.AccountId);
        return View(transaction);
    }

    // GET: Transactions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Transaction? transaction = await context.Transactions.FindAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        ViewData["AccountId"] = new SelectList(context.Accounts, "Id", "Id", transaction.AccountId);
        return View(transaction);
    }

    // POST: Transactions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Amount,Date,AccountId")] Transaction transaction)
    {
        if (id != transaction.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(transaction);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["AccountId"] = new SelectList(context.Accounts, "Id", "Id", transaction.AccountId);
        return View(transaction);
    }

    // GET: Transactions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Transaction? transaction = await context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // POST: Transactions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        Transaction? transaction = await context.Transactions.FindAsync(id);
        if (transaction != null)
        {
            context.Transactions.Remove(transaction);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransactionExists(int id)
    {
        return context.Transactions.Any(e => e.Id == id);
    }
}
