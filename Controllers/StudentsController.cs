using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entities;
namespace StudentPortal.Controllers;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public StudentsController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddStudentViewModel viewModel)
    {

        var student = new Student
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            Phone = viewModel.Phone,
            isSubscribed = viewModel.isSubscribed
        };
        await dbContext.Students.AddAsync(student);
        await dbContext.SaveChangesAsync();
        return RedirectToAction("List", "Students");

    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var students = await dbContext.Students.ToListAsync();
        return View(students);

    }
    [HttpGet]

    public async Task<IActionResult> Edit(Guid id)
    {
        var student = await dbContext.Students.FindAsync(id);
        return View(student);

    }

    [HttpPost]
    public async Task<IActionResult> Edit(Student studentData)
    {
        var student = await dbContext.Students.FindAsync(studentData.Id);

        if (student is not null)
        {

            student.Name = studentData.Name;
            student.Email = studentData.Email;
            student.Phone = studentData.Phone;
            student.isSubscribed = studentData.isSubscribed;
            await dbContext.SaveChangesAsync();

        }
        return RedirectToAction("List", "Students");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Student studentData)
    {
        var student = await dbContext.Students.FindAsync(studentData.Id);
        if (student is not null)
        {

            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();
        }
        return RedirectToAction("List", "Students");

    }
}
