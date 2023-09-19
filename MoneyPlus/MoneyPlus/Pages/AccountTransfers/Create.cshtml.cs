using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;
using System.Security.Claims;

namespace MoneyPlus.Pages.AccountTransfers;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly Account _account;

    public CreateModel(MoneyPlus.Data.ApplicationDbContext context, Account account)
    {
        _context = context;
        _account = account;

    }


    public IActionResult OnGet()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        ViewData["AccountId"] = new SelectList(_context.Accounts.Where(a => a.UserId == userId), "Id", "Description");
        ViewData["TypingId"] = new SelectList(_context.Typings.Where(t => t.Id == 3), "Id", "Type");
        ViewData["ActiveId"] = new SelectList(_context.Actives.Where(a => a.UserId == userId), "Id", "Description");
        ViewData["PayeeId"] = new SelectList(_context.Payees.Where(a => a.UserId == userId), "Id", "Name");
        ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "Id", "Name");

        return Page();
    }

    [BindProperty]
    public Transfer Transfer { get; set; }


    public async Task<IActionResult> OnPostAsync()
    {

        if (Transfer.TypingId == 3)
        {
            _account.TransferMoney(Transfer.Amount, Transfer.AccountId, Transfer.AccountIdReceived);

            Transfer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Transfer.Date = DateTime.Now;
            _context.Transfers.Add(Transfer);
            await _context.SaveChangesAsync();

        }
        else
            throw new Exception();

        return RedirectToPage("/Accounts/Index");

    }
}


