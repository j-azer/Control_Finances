using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyPlus.Data;
using System.ComponentModel.DataAnnotations;

namespace MoneyPlus.Pages.admin;

public class secretModel : PageModel
{

    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _user;
    private readonly RoleManager<IdentityRole> _roleAdmin;

    public secretModel(ApplicationDbContext context,
        UserManager<IdentityUser> user, RoleManager<IdentityRole> roleAdmin)
    {
        _context = context;
        _user = user;
        _roleAdmin = roleAdmin;
    }

    [BindProperty]
    [EmailAddress]
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }

    public IActionResult OnGet(string pwd)
    {
        if (pwd == "pouco-segura")
        {
            return Page();
        }
        return NotFound();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await DesignateRoleAsync();
        return RedirectToPage(".././Index");

    }

    public async Task DesignateRoleAsync()
    {
        _context.Database.EnsureCreated();

        string role = "admin";

        if (await _roleAdmin.FindByNameAsync(role) == null)
        {
            await _roleAdmin.CreateAsync(new IdentityRole(role));
        }

        var users = _context.Users.ToList();

        var existUsers = users.FirstOrDefault(r => r.Email == Email);

        if (existUsers != null)
        {
            IdentityResult roleResult = await _user.AddToRoleAsync(existUsers, role);
        }
        var newUser = new IdentityUser
        {
            UserName = Email,
            Email = Email
        };

        var result = await _user.CreateAsync(newUser);

        if (result.Succeeded)
        {
            await _user.AddPasswordAsync(newUser, Password);
            await _user.AddToRoleAsync(newUser, role);
        }

        await _context.SaveChangesAsync();
    }
}
