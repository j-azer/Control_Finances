using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;

namespace MoneyPlus.Pages.Subcategories
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public DeleteModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Subcategory Subcategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Subcategories == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategories.FirstOrDefaultAsync(m => m.Id == id);

            if (subcategory == null)
            {
                return NotFound();
            }
            else 
            {
                Subcategory = subcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Subcategories == null)
            {
                return NotFound();
            }
            var subcategory = await _context.Subcategories.FindAsync(id);

            if (subcategory != null)
            {
                Subcategory = subcategory;
                _context.Subcategories.Remove(Subcategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
