using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Pages.Departments;

public class CreateModel : PageModel
{
    private readonly 
        SchoolContext _context;

    public CreateModel(
        SchoolContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
    ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "FirstMidName");
        return Page();
    }

    [BindProperty]
    public Department Department { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Departments.Add(Department);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
