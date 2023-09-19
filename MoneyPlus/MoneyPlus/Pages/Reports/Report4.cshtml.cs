using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data.Entities;
using MoneyPlus.Models;
using System.Globalization;
using System.Security.Claims;

namespace MoneyPlus.Pages.Reports;

[Authorize]
public class Report4Model : PageModel
{
    private readonly MoneyPlus.Data.ApplicationDbContext _context;
    private readonly Transfer _transfer;

    public Report4Model(MoneyPlus.Data.ApplicationDbContext context, Transfer transfer)
    {
        _context = context;
        _transfer = transfer;
    }

    public IList<Transfer> transfers { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        if (_context.Transfers != null)
        {

            transfers = await _context.Transfers
            .Include(t => t.Subcategory.Category)
            .Include(t => t.Typing).Where(a => a.UserId == userId && a.TypingId == 2)
            .OrderBy(t => t.Date).ToListAsync();

            var aggCategorysExpenses = transfers.GroupBy(x => new { x.Subcategory.Category, x.Date.Year })
                .Select(x => new AggregateExepenseCategoryModel
                {
                    Category = x.Key.Category,
                    Year = x.Key.Year,
                    Total = x.Sum(a => a.Amount)
                }).ToList();

            ViewData["Years"] = aggCategorysExpenses.Select(y => y.Year).Distinct().OrderBy(y => y).ToList();
            ViewData["AggCategorysExpenses"] = aggCategorysExpenses;
        }
    }
}
