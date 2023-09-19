using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data.Entities;
using System.Globalization;
using System.Security.Claims;

namespace MoneyPlus.Pages.Reports;

[Authorize]
public class Report2Model : PageModel
{
    private readonly MoneyPlus.Data.ApplicationDbContext _context;
    private readonly Transfer _transfer;

    public Report2Model(MoneyPlus.Data.ApplicationDbContext context, Transfer transfer)
    {
        _context = context;
        _transfer = transfer;
    }

    public IList<Transfer> Transfer { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        ViewData["AccountId"] = new SelectList(_context.Accounts.Where(a => a.UserId == userId), "Id", "Type");

        var dateYear = string.IsNullOrEmpty(Request.Query["year"].ToString()) ? DateTime.Now.Year.ToString() : Request.Query["year"].ToString();

        var dateMonth = string.IsNullOrEmpty(Request.Query["month"].ToString()) ? DateTime.Now.Month.ToString() : Request.Query["month"].ToString();

        var year = int.Parse(dateYear) > DateTime.Now.Year ? DateTime.Now.Year : int.Parse(dateYear);

        var month = int.Parse(dateMonth) > 12 || int.Parse(dateMonth) < 1 ? 12 : int.Parse(dateMonth);        

        var account = Request.Query["account"].ToString();

        ViewData["nextMonth"] = month + 1;
        ViewData["lastMonth"] = month - 1;
        ViewData["actualMonth"] = month;

        ViewData["nextYear"] = year + 1;
        ViewData["lastYear"] = year - 1;
        ViewData["actualYear"] = year;

        if (_context.Transfers != null)
        {

            Transfer = await _context.Transfers
            .Include(t => t.Account)
            .Include(t => t.Active)
            .Include(t => t.Payee)
            .Include(t => t.Subcategory.Category)
            .Include(t => t.Subcategory)
            .Include(t => t.Typing).Where(a => a.UserId == userId && a.TypingId == 1 && a.Date.Month == month && a.Date.Year == year)
            .OrderBy(t => t.Date).ToListAsync();


            if (!string.IsNullOrEmpty(account))
            {
                Transfer = Transfer.Where(a => a.AccountId == int.Parse(account)).ToList();
            }

           


            ViewData["ResultSum"] = Transfer.Sum(t => t.Amount).ToString("F2", CultureInfo.InvariantCulture);
        }
    }

}
