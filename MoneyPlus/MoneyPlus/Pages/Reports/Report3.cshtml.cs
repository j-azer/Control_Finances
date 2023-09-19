using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data.Entities;
using MoneyPlus.Models;
using System.Globalization;
using System.Security.Claims;

namespace MoneyPlus.Pages.Reports;

[Authorize]
public class Report3Model : PageModel
{
    private readonly MoneyPlus.Data.ApplicationDbContext _context;
    private readonly Transfer _transfer;

    public Report3Model(MoneyPlus.Data.ApplicationDbContext context, Transfer transfer)
    {
        _context = context;
        _transfer = transfer;
    }

    public IList<Transfer> transfers { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var dateYear = string.IsNullOrEmpty(Request.Query["year"]
                             .ToString()) ? DateTime.Now.Year.ToString() : Request.Query["year"].ToString();

        var year = int.Parse(dateYear) > DateTime.Now.Year ? DateTime.Now.Year : int.Parse(dateYear);

        ViewData["nextYear"] = year + 1;
        ViewData["lastYear"] = year - 1;
        ViewData["actualYear"] = year;

        if (_context.Transfers != null)
        {

            transfers = await _context.Transfers
            .Include(t => t.Subcategory.Category)
            .Include(t => t.Subcategory)
            .Include(t => t.Typing).Where(a => a.UserId == userId && a.TypingId == 2 && a.Date.Year == year)
            .OrderBy(t => t.Date).ToListAsync();

            var aggSubCategorysExpenses = transfers.GroupBy(x => new { x.Subcategory, x.Date.Month })
                .Select(x => new AggregateExepenseSubcategoryModel
                {
                    Subcategory = x.Key.Subcategory,
                    Month = x.Key.Month,
                    Total = x.Sum(a => a.Amount)
                }).ToList();


            var aggCategorysExpenses = transfers.GroupBy(x => new { x.Subcategory.Category, x.Date.Month })
                .Select(x => new AggregateExepenseCategoryModel
                {
                    Category = x.Key.Category,
                    Month = x.Key.Month,
                    Total = x.Sum(a => a.Amount)
                }).ToList();

            ViewData["Months"] = MonthModel.GetMonths();
            ViewData["AggCategorysExpenses"] = aggCategorysExpenses;
            ViewData["AggSubCategorysExpenses"] = aggSubCategorysExpenses;
            ViewData["ResultSum"] = transfers.Sum(t => t.Amount).ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
