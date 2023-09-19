using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoneyPlus.Pages.admin;

[Authorize(Roles = "admin")]
public class AdministratorAreaModel : PageModel
{
    public void OnGet()
    {
    }
}
