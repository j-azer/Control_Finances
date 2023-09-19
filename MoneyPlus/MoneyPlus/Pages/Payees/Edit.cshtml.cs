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

namespace MoneyPlus.Pages.Payees
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
        public Payee Payee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Payees == null)
            {
                return NotFound();
            }

            var payee =  await _context.Payees.FirstOrDefaultAsync(m => m.Id == id);
            if (payee == null)
            {
                return NotFound();
            }
            Payee = payee;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Payee.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Payee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayeeExists(Payee.Id))
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

        private bool PayeeExists(int id)
        {
          return _context.Payees.Any(e => e.Id == id);
        }
    }
}
