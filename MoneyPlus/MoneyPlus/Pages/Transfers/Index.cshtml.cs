using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;

namespace MoneyPlus.Pages.Transfers;

[Authorize]
public class IndexModel : PageModel
{
    private readonly MoneyPlus.Data.ApplicationDbContext _context;

    public IndexModel(MoneyPlus.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Transfer> Transfer { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (_context.Transfers != null)
        {
            Transfer = await _context.Transfers.Where(a => a.UserId == userId)
            .Include(t => t.Account)
            .Include(t => t.Active)
            .Include(t => t.Payee)
            .Include(t => t.Subcategory.Category)
            .Include(t => t.Subcategory)
            .Include(t => t.Typing).ToListAsync();
        }
    }
}
