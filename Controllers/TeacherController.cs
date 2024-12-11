using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers;

public class TeacherController : Controller
{
  private readonly ApplicationDbContext _context;

  // Constructeur and connect to the database
  public TeacherController(ApplicationDbContext context)
  {
    _context = context;
  }

  public ActionResult Index()
  {
    return View(_context.Teachers.ToList());
  }

  public ActionResult New()
  {
    return View();
  }

  public ActionResult Create(TeacherModel teacher)
  {
    if (!ModelState.IsValid)
    {
      return View("New");
    }

    teacher.Email = teacher.Firstname.ToLower() + "_" + teacher.Lastname.ToLower() + "@school.com";
    // Ajouter le teacher
    _context.Teachers.Add(teacher);

    // Sauvegarder les changements
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Show(int id)
  {
    var teacher = _context.Teachers.Find(id);
    return View(teacher);
  }

  public ActionResult Edit(int id)
  {
    var teacher = _context.Teachers.Find(id);
    return View(teacher);
  }

  public ActionResult Update(TeacherModel teacher)
  {
    if (!ModelState.IsValid)
    {
      return View("Edit");
    }

    var teacherToUpdate = _context.Teachers.Find(teacher.Id);

    teacherToUpdate.Firstname = teacher.Firstname;
    teacherToUpdate.Lastname = teacher.Lastname;
    teacherToUpdate.Email = teacher.Firstname.ToLower() + "_" + teacher.Lastname.ToLower() + "@school.com";

    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Delete(int id)
  {
    _context.Teachers.Remove(_context.Teachers.Find(id));
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
}