using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyPlus.Models;
using MoneyPlus.Services.NewsService;

namespace MoneyPlus.Pages.NewsFinance;

public class IndexModel : PageModel
{
    public FinanceNews news;

    private readonly ILogger<IndexModel> _logger;
    private readonly INewsService _newService;

    public IndexModel(ILogger<IndexModel> logger, INewsService newService)
    {
        _logger = logger;
        _newService = newService;
    }

    public void OnGet()
    {
        news = _newService.GetFinanceNews();
    }
}
