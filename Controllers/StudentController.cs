using Microsoft.AspNetCore.Mvc;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.ViewModels;

namespace StudentRegistrationSystem.Controllers;

public class StudentController : Controller
{
    private static readonly List<Student> RegisteredStudents = new();
    private static readonly object SyncRoot = new();

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.StudentCount = GetStudents().Count;
        return View(new StudentRegistrationViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(StudentRegistrationViewModel model)
    {
        // Server-side validation runs even when JavaScript is disabled.
        if (!ModelState.IsValid)
        {
            ViewBag.StudentCount = GetStudents().Count;
            return View(model);
        }

        // Manual validation detects business rules not represented by attributes.
        if (GetStudents().Any(student =>
                string.Equals(student.Email, model.Email.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            ModelState.AddModelError(nameof(model.Email), "Бұл электрондық пошта мекенжайы бұрын тіркелген.");
            ViewBag.StudentCount = GetStudents().Count;
            return View(model);
        }

        var student = new Student
        {
            Id = GetNextId(),
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            Email = model.Email.Trim(),
            Age = model.Age!.Value,
            Faculty = model.Faculty.Trim(),
            Course = model.Course!.Value,
            GPA = model.GPA!.Value,
            PhoneNumber = model.PhoneNumber.Trim(),
            AboutMe = string.IsNullOrWhiteSpace(model.AboutMe) ? null : model.AboutMe.Trim()
        };

        lock (SyncRoot)
        {
            RegisteredStudents.Add(student);
        }

        TempData["SuccessMessage"] = $"Қош келдіңіз, {student.FirstName}! Студенттік профиліңіз сәтті тіркелді.";
        return RedirectToAction(nameof(Success));
    }

    [HttpGet]
    public IActionResult Students()
    {
        return View(GetStudents());
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var student = GetStudents().FirstOrDefault(item => item.Id == id);
        return student is null ? NotFound() : View(student);
    }

    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }

    private static IReadOnlyList<Student> GetStudents()
    {
        lock (SyncRoot)
        {
            return RegisteredStudents.ToList();
        }
    }

    private static int GetNextId()
    {
        lock (SyncRoot)
        {
            return RegisteredStudents.Count == 0
                ? 1
                : RegisteredStudents.Max(student => student.Id) + 1;
        }
    }
}
