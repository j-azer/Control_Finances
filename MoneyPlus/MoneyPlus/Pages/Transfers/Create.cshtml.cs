using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;

namespace MoneyPlus.Pages.Transfers;

[Authorize]
public class CreateModel : PageModel
{
    private readonly MoneyPlus.Data.ApplicationDbContext _context;
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
        ViewData["ActiveId"] = new SelectList(_context.Actives.Where(a => a.UserId == userId), "Id", "Description");
        ViewData["PayeeId"] = new SelectList(_context.Payees.Where(a => a.UserId == userId), "Id", "Name");
        ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "Id", "Name");
        ViewData["TypingId"] = new SelectList(_context.Typings.Where(t => t.Id != 3), "Id", "Type");


        return Page();
    }

    [BindProperty]
    public Transfer Transfer { get; set; }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (Transfer.TypingId == 1)
        {
            if (_account.DepositMoney(Transfer.Amount, Transfer.AccountId) == true)
            {
                Transfer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Transfers.Add(Transfer);
                await _context.SaveChangesAsync();
            }
        }
        else if (Transfer.TypingId == 2)
        {
            if (_account.TakeMoney(Transfer.Amount, Transfer.AccountId) == true)
            {
                Transfer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.Transfers.Add(Transfer);
                await _context.SaveChangesAsync();
            }
        }
        else
            throw new Exception();

        return RedirectToPage("./Index");

    }
}
