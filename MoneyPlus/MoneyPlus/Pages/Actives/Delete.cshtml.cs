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
    public class DeleteModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public DeleteModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Actives == null)
            {
                return NotFound();
            }
            var active = await _context.Actives.FindAsync(id);

            if (active != null)
            {
                Active = active;
                _context.Actives.Remove(Active);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
