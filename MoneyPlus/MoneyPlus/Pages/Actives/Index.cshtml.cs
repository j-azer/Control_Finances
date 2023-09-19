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

namespace MoneyPlus.Pages.Actives
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public IndexModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Active> Active { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_context.Actives != null)
            {
                Active = await _context.Actives.Where(a => a.UserId == userId).ToListAsync();
            }
        }
    }
}
