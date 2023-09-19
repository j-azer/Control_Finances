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
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public DetailsModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
