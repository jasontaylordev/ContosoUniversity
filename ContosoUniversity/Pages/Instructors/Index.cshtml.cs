using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;  // Add VM
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors;

public class IndexModel : PageModel
{
    private readonly SchoolContext _context;

    public IndexModel(SchoolContext context)
    {
        _context = context;
    }

    public InstructorIndexData InstructorData { get; set; }
    public int InstructorID { get; set; }
    public int CourseID { get; set; }

    public async Task OnGetAsync(int? id, int? courseID)
    {
        InstructorData = new InstructorIndexData();
        InstructorData.Instructors = await _context.Instructors
            .Include(i => i.OfficeAssignment)
            .Include(i => i.Courses)
                .ThenInclude(c => c.Department)
            .OrderBy(i => i.LastName)
            .ToListAsync();

        if (id != null)
        {
            InstructorID = id.Value;
            Instructor instructor = InstructorData.Instructors
                .Where(i => i.ID == id.Value).Single();
            InstructorData.Courses = instructor.Courses;
        }

        if (courseID != null)
        {
            CourseID = courseID.Value;
            var selectedCourse = InstructorData.Courses
                .Where(x => x.CourseID == courseID).Single();
            await _context.Entry(selectedCourse)
                          .Collection(x => x.Enrollments).LoadAsync();
            foreach (Enrollment enrollment in selectedCourse.Enrollments)
            {
                await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
            }
            InstructorData.Enrollments = selectedCourse.Enrollments;
        }
    }
}
