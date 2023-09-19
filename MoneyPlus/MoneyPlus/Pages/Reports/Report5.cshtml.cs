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
public class Report5Model : PageModel
{
    private readonly MoneyPlus.Data.ApplicationDbContext _context;
    private readonly Transfer _transfer;
    private readonly Account _account;

    public Report5Model(MoneyPlus.Data.ApplicationDbContext context, Transfer transfer, Account account)
    {
        _context = context;
        _transfer = transfer;
        _account = account;
    }

    public IList<Transfer> Transfer { get; set; } = default!;

    public IList<Account> Account { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        var realInflation = string.IsNullOrEmpty(Request.Query["inflation"].ToString()) ? "1" : Request.Query["inflation"].ToString();

        var realReturns = string.IsNullOrEmpty(Request.Query["returns"].ToString()) ? "1" : Request.Query["returns"].ToString();

        var inflation = double.Parse(realInflation) <= 0.0 ? 1.0 : double.Parse(realInflation);

        var returns = double.Parse(realReturns) <= 0.0 ? 1.0 : double.Parse(realReturns);

        var year = DateTime.Now.Year;

        var account = Request.Query["account"].ToString();


        if (_context.Accounts != null)
        {
            Account = await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
        }        

        if (_context.Transfers != null)
        {

            Transfer = await _context.Transfers
            .Include(t => t.Account)
            .Include(t => t.Typing).Where(a => a.UserId == userId && a.TypingId == 1 && a.Date.Year == year)
            .OrderBy(t => t.Date).ToListAsync();

            if (!string.IsNullOrEmpty(account))
            {
                Transfer = Transfer.Where(a => a.AccountId == int.Parse(account)).ToList();
            }

            var resultSumRevenue = Transfer.Sum(t => t.Amount);

            ViewData["ResultSumRevenue"] = resultSumRevenue.ToString();

            var averageAnnualRevenue = (resultSumRevenue / 12);

            ViewData["AverageAnnualRevenue"] = averageAnnualRevenue.ToString("F2", CultureInfo.InvariantCulture);




            Transfer = await _context.Transfers
            .Include(t => t.Account)
            .Include(t => t.Typing).Where(a => a.UserId == userId && a.TypingId == 2 && a.Date.Year == year)
            .OrderBy(t => t.Date).ToListAsync();

            if (!string.IsNullOrEmpty(account))
            {
                Transfer = Transfer.Where(a => a.AccountId == int.Parse(account)).ToList();
            }

            var resultSumExpense = Transfer.Sum(t => t.Amount);

            ViewData["ResultSumExpense"] = resultSumExpense.ToString();

            var averageAnnualExpense = (resultSumExpense / 12);

            ViewData["AverageAnnualExpense"] = averageAnnualExpense.ToString("F2", CultureInfo.InvariantCulture);




            Account = await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
            
           
            if (!string.IsNullOrEmpty(account))
            {
                Account = Account.Where(a => a.Id == int.Parse(account)).ToList();
            }

            var resultSumBalance = Account.Sum(t => t.Balance);

            ViewData["ResultSumBalance"] = resultSumBalance.ToString("F2", CultureInfo.InvariantCulture);




            var resultMonthsWithoutWork = (resultSumBalance + (resultSumBalance * returns/100)) / (averageAnnualExpense + (averageAnnualExpense * inflation/100));

            ViewData["ResultMonthsWithoutWork"] = resultMonthsWithoutWork.ToString("F2", CultureInfo.InvariantCulture);

        }
    }
}
