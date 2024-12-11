using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers;

public class TeacherController : Controller
{
  private readonly ApplicationDbContext _context;
  private readonly UserManager<TeacherModel> _userManager;

  // Constructeur and connect to the database
  public TeacherController(UserManager<TeacherModel> userManager)
  {
      _userManager = userManager;
  }

  public async Task<ActionResult> Index()
  {
    var teachers = _userManager.Users;
    return View(teachers.ToList());
  }

  public ActionResult New()
  {
    return View();
  }

  // Managed by the register action from Entity
  // public ActionResult Create(TeacherModel teacher)
  // {
  //   if (!ModelState.IsValid)
  //   {
  //     return View("New");
  //   }

  //   teacher.Email = teacher.Firstname.ToLower() + "_" + teacher.Lastname.ToLower() + "@school.com";
  //   // Ajouter le teacher
  //   _context.Teachers.Add(teacher);

  //   // Sauvegarder les changements
  //   _context.SaveChanges();
  //   return RedirectToAction("Index");
  // }

  public async Task<ActionResult> Show(string id)
  {
    var teacher = await _userManager.FindByIdAsync(id);
    return View(teacher);
  }

  public async Task<ActionResult> Edit(string id)
  {
    var teacher = await _userManager.FindByIdAsync(id);
    return View(teacher);
  }

  public async Task<ActionResult> Update(TeacherModel teacher)
  {
    if (!ModelState.IsValid)
    {
      return View("Edit");
    }

    // TODO : See why the teacherToUpdate is null
    var teacherToUpdate = await _userManager.FindByIdAsync(teacher.Id);

    teacherToUpdate.Firstname = teacher.Firstname;
    teacherToUpdate.Lastname  = teacher.Lastname;
    teacherToUpdate.UserName  = teacher.Firstname.ToLower() + "_" + teacher.Lastname.ToLower();
    teacherToUpdate.Email     = teacher.Firstname.ToLower() + "_" + teacher.Lastname.ToLower() + "@school.com";

    var result = await _userManager.UpdateAsync(teacherToUpdate);

    if (!result.Succeeded)
      {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(teacherToUpdate);
      }
    return RedirectToAction("Index");
  }

  public async Task<ActionResult> Delete(string id)
  {
    var teacher = await _userManager.FindByIdAsync(id);
    var result  = await _userManager.DeleteAsync(teacher);

    if (!result.Succeeded)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
      return View("Index");
    }

    return RedirectToAction("Index");
  }
}