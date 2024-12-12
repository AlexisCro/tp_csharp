using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;

namespace mvc.Controllers;

public class EventController : Controller
{
  private readonly ApplicationDbContext _context;

  // Constructeur and connect to the database
  public EventController(ApplicationDbContext context)
  {
    _context = context;
  }
 
  public ActionResult Index()
  {
    return View(_context.Events.ToList());
  }

  public ActionResult New()
  {
    return View();
  }

  public ActionResult Create(EventModel eventModel)
  {
    if (!ModelState.IsValid)
    {
      return View("New");
    }

    _context.Events.Add(eventModel);

    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Show(int id)
  {
    var eventObject = _context.Events.Find(id);
    return View(eventObject);
  }

  public ActionResult Edit(int id)
  {
    var eventObject = _context.Events.Find(id);
    return View(eventObject);
  }

// TODO: Do a view model to fix the problem.
  public ActionResult Update(EventModel eventModel)
  {
    if (!ModelState.IsValid)
    {
      return View("Edit");
    }

    var eventToUpdate = _context.Events.Find(eventModel.Id);

    eventToUpdate.Title = eventModel.Title;
    eventToUpdate.Description = eventModel.Description;
    eventToUpdate.EventDate = eventModel.EventDate;
    eventToUpdate.MaxParticipants = eventModel.MaxParticipants;
    eventToUpdate.Location = eventModel.Location;

    // Sauvegarder les changements
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public ActionResult Delete(int id)
  {
    var eventObject = _context.Events.Find(id);
    _context.Events.Remove(eventObject);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
}