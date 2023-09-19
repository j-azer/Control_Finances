using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;

namespace MoneyPlus.Pages.Typings
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public IndexModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Typing> Typing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Typings != null)
            {
                Typing = await _context.Typings.ToListAsync();
            }
        }
    }
}
