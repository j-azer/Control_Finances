using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;

namespace MoneyPlus.Pages.Actives
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly MoneyPlus.Data.ApplicationDbContext _context;

        public EditModel(MoneyPlus.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Active Active { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Actives == null)
            {
                return NotFound();
            }

            var active =  await _context.Actives.FirstOrDefaultAsync(m => m.Id == id);
            if (active == null)
            {
                return NotFound();
            }
            Active = active;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Active.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Active).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveExists(Active.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ActiveExists(int id)
        {
          return _context.Actives.Any(e => e.Id == id);
        }
    }
}
