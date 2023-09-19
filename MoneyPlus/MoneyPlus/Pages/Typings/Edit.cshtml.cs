using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Data.Entities;

namespace MoneyPlus.Pages.Typings
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
        public Typing Typing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Typings == null)
            {
                return NotFound();
            }

            var typing =  await _context.Typings.FirstOrDefaultAsync(m => m.Id == id);
            if (typing == null)
            {
                return NotFound();
            }
            Typing = typing;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Typing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypingExists(Typing.Id))
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

        private bool TypingExists(int id)
        {
          return _context.Typings.Any(e => e.Id == id);
        }
    }
}
