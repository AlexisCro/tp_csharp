using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers;

public class StudentController : Controller
{
  private readonly ApplicationDbContext _context;
  private readonly UserModel _user;
  private readonly UserManager<UserModel> _userManager;

  // Constructeur and connect to the database
  public StudentController(ApplicationDbContext context, UserManager<UserModel> userManager)
  {
    _context     = context;
    _userManager = userManager;
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
    if (_context.Roles.FirstOrDefault(r => r.Name == "Student") == null)
    {
      _context.Roles.Add(new RoleModel { Name = "Student" });
      _context.SaveChanges();
    }

    student.Role = _context.Roles.FirstOrDefault(role => role.Name == "Student");
    student.UserName = student.Firstname.ToLower() + "_" + student.Lastname.ToLower();
    student.Email = student.Firstname.ToLower() + "_" + student.Lastname.ToLower() + "@school.com";
    // Ajouter le student
    _context.Students.Add(student);

    // Sauvegarder les changements
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Show(string id)
  {
    var student = _context.Students.Find(id);
    return View(student);
  }

  public ActionResult Edit(string id)
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

  public ActionResult Delete(string id)
  {
    _context.Students.Remove(_context.Students.Find(id));
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
}