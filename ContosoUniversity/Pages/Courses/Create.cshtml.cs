using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Pages.Courses;

public class CreateModel : DepartmentNamePageModel
{
    private readonly SchoolContext _context;

    public CreateModel(SchoolContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        PopulateDepartmentsDropDownList(_context);
        return Page();
    }

    [BindProperty]
    public Course Course { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var emptyCourse = new Course();

        if (await TryUpdateModelAsync<Course>(
             emptyCourse,
             "course",   // Prefix for form value.
             s => s.CourseID, s => s.DepartmentID, s => s.Title, s => s.Credits))
        {
            _context.Courses.Add(emptyCourse);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        // Select DepartmentID if TryUpdateModelAsync fails.
        PopulateDepartmentsDropDownList(_context, emptyCourse.DepartmentID);
        return Page();
    }
}
