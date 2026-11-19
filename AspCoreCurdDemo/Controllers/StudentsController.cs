

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspCoreCurdDemo.Models;
using AspCoreCurdDemo.Data;

public class StudentsController : Controller
{
    private readonly ApplicationDBContext _context;

    public StudentsController(ApplicationDBContext context)
    {
        _context = context;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        return View("~/Views/Student/Index.cshtml",
            await _context.Students.ToListAsync());
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? studentid)
    {
        if (studentid == null)
            return NotFound();

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.StudentId == studentid);

        if (student == null)
            return NotFound();

        return View("~/Views/Student/Details.cshtml", student);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        return View("~/Views/Student/Create.cshtml");
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("StudentId,Name,Email,Course,EnrollmentDate")] Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View("~/Views/Student/Create.cshtml", student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? studentid)
    {
        if (studentid == null)
            return NotFound();

        var student = await _context.Students.FindAsync(studentid);

        if (student == null)
            return NotFound();

        return View("~/Views/Student/Edit.cshtml", student);
    }

    // POST: Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int? studentid,
        [Bind("StudentId,Name,Email,Course,EnrollmentDate")] Student student)
    {
        if (studentid != student.StudentId)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View("~/Views/Student/Edit.cshtml", student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? studentid)
    {
        if (studentid == null)
            return NotFound();

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.StudentId == studentid);

        if (student == null)
            return NotFound();

        return View("~/Views/Student/Delete.cshtml", student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? studentid)
    {
        var student = await _context.Students.FindAsync(studentid);

        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int? studentid)
    {
        return _context.Students.Any(e => e.StudentId == studentid);
    }
}
