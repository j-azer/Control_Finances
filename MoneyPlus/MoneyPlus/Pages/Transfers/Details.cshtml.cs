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

namespace MoneyPlus.Pages.Transfers
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public DetailsModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Transfer Transfer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transfers == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(t => t.Typing)
                .Include(t => t.Account)
                .Include(t => t.Active)
                .Include(t => t.Payee)
                .Include(t => t.Subcategory.Category)
                .Include(t => t.Subcategory).FirstOrDefaultAsync(m => m.Id == id);
            

            if (transfer == null)
            {
                return NotFound();
            }
            else 
            {
                Transfer = transfer;
            }
            return Page();
        }
    }
}
