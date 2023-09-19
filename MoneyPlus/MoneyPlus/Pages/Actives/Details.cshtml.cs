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

namespace MoneyPlus.Pages.Actives
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public DetailsModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Active Active { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Actives == null)
            {
                return NotFound();
            }

            var active = await _context.Actives.FirstOrDefaultAsync(m => m.Id == id);
            if (active == null)
            {
                return NotFound();
            }
            else 
            {
                Active = active;
            }
            return Page();
        }
    }
}
