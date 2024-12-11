using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers;

public class StudentController : Controller
{
  private readonly ApplicationDbContext _context;

  // Constructeur and connect to the database
  public StudentController(ApplicationDbContext context)
  {
    _context = context;
  }

  public ActionResult Index()
  {
    return View(_context.Students.ToList());
  }

  public ActionResult New()
  {
    return View();
  }

  public ActionResult Create(StudentModel student)
  {
    if (!ModelState.IsValid)
    {
      return View("New");
    }

    student.Email = student.Firstname.ToLower() + "_" + student.Lastname.ToLower() + "@school.com";
    // Ajouter le student
    _context.Students.Add(student);

    // Sauvegarder les changements
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Show(int id)
  {
    var student = _context.Students.Find(id);
    return View(student);
  }

  public ActionResult Edit(int id)
  {
    var student = _context.Students.Find(id);
    return View(student);
  }

  public ActionResult Update(StudentModel student)
  {
    if (!ModelState.IsValid)
    {
      return View("Edit");
    }

    var studentToUpdate = _context.Students.Find(student.Id);

    studentToUpdate.Firstname = student.Firstname;
    studentToUpdate.Lastname = student.Lastname;
    studentToUpdate.Email = student.Firstname.ToLower() + "_" + student.Lastname.ToLower() + "@school.com";

    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Delete(int id)
  {
    _context.Students.Remove(_context.Students.Find(id));
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
}