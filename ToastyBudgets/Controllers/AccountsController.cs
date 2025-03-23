using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToastyBudgets.Data;
using ToastyBudgets.Entities;

namespace ToastyBudgets.Controllers;

public class AccountsController(ApplicationDbContext context) : Controller
{
    // GET: Accounts
    public async Task<IActionResult> Index()
    {
        Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Account, Microsoft.AspNetCore.Identity.IdentityUser?> applicationDbContext = context.Accounts.Include(a => a.User);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Accounts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Account? account = await context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (account == null)
        {
            return NotFound();
        }

        return View(account);
    }

    // GET: Accounts/Create
    public IActionResult Create()
    {
        ViewData["IdentityUserId"] = new SelectList(context.Users, "Id", "Id");
        return View();
    }

    // POST: Accounts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Balance,IdentityUserId")] Account account)
    {
        if (ModelState.IsValid)
        {
            context.Add(account);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["IdentityUserId"] = new SelectList(context.Users, "Id", "Id", account.IdentityUserId);
        return View(account);
    }

    // GET: Accounts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Account? account = await context.Accounts.FindAsync(id);
        if (account == null)
        {
            return NotFound();
        }

        ViewData["IdentityUserId"] = new SelectList(context.Users, "Id", "Id", account.IdentityUserId);
        return View(account);
    }

    // POST: Accounts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Balance,IdentityUserId")] Account account)
    {
        if (id != account.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(account);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(account.Id))
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

        ViewData["IdentityUserId"] = new SelectList(context.Users, "Id", "Id", account.IdentityUserId);
        return View(account);
    }

    // GET: Accounts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Account? account = await context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (account == null)
        {
            return NotFound();
        }

        return View(account);
    }

    // POST: Accounts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        Account? account = await context.Accounts.FindAsync(id);
        if (account != null)
        {
            context.Accounts.Remove(account);
        }

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AccountExists(int id)
    {
        return context.Accounts.Any(e => e.Id == id);
    }
}
