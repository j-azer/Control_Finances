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
    public class DeleteModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public DeleteModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Typing Typing { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Typings == null)
            {
                return NotFound();
            }

            var typing = await _context.Typings.FirstOrDefaultAsync(m => m.Id == id);

            if (typing == null)
            {
                return NotFound();
            }
            else 
            {
                Typing = typing;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Typings == null)
            {
                return NotFound();
            }
            var typing = await _context.Typings.FindAsync(id);

            if (typing != null)
            {
                Typing = typing;
                _context.Typings.Remove(Typing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
