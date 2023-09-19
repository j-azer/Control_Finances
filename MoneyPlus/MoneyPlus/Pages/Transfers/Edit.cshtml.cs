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

namespace MoneyPlus.Pages.Transfers
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
        public Transfer Transfer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Transfers == null)
            {
                return NotFound();
            }

            var transfer =  await _context.Transfers.FirstOrDefaultAsync(m => m.Id == id);
            if (transfer == null)
            {
                return NotFound();
            }
            Transfer = transfer;
           ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Description");
           ViewData["ActiveId"] = new SelectList(_context.Actives, "Id", "Description");
           ViewData["PayeeId"] = new SelectList(_context.Payees, "Id", "Name");
           ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "Id", "Name");
           ViewData["TypingId"] = new SelectList(_context.Typings, "Id", "Type");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Transfer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Transfer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransferExists(Transfer.Id))
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

        private bool TransferExists(int id)
        {
          return _context.Transfers.Any(e => e.Id == id);
        }
    }
}
