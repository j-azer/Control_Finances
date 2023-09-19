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
    public class DetailsModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public DetailsModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
