using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Departments;

public class DetailsModel : PageModel
{
    private readonly SchoolContext _context;

    public DetailsModel(SchoolContext context)
    {
        _context = context;
    }

    public Department Department { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Department = await _context.Departments
            .Include(d => d.Administrator).FirstOrDefaultAsync(m => m.DepartmentID == id);

        if (Department == null)
        {
            return NotFound();
        }
        return Page();
    }
}
