using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyPlus.Models;
using MoneyPlus.Services.NewsService;

namespace MoneyPlus.Pages;


public class IndexModel : PageModel
{       
    private readonly ILogger<IndexModel> _logger;        

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;            
    }

    public void OnGet()
    {
      
    }
}