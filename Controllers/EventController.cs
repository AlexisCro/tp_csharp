using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;
using mvc.Services;

namespace mvc.Controllers;

public class EventController : Controller
{
  private readonly ApplicationDbContext _context;
  private readonly UserService _userService;

  // Constructeur and connect to the database
  public EventController(ApplicationDbContext context, UserService userService)
  {
    _context = context;
    _userService = userService;
  }
 
  public ActionResult Index()
  {
    return View(_context.Events.ToList());
  }

  public async Task<ActionResult> New()
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à créer un événement.";
      return RedirectToAction("Index");
    }

    return View();
  }

  public async Task<ActionResult> Create(EventModel eventModel)
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à créer un événement.";
      return RedirectToAction("Index", "Home");
    }

    if (!ModelState.IsValid)
    {
      TempData["Error"] = "Un problème a eu lieu.";
      return View("Index", "Home");
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

  public async Task<ActionResult> Edit(int id)
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à modifier un événement.";
      return RedirectToAction("Index");
    }

    var eventObject = _context.Events.Find(id);
    return View(eventObject);
  }

  public async Task<ActionResult> Update(EventModel eventModel)
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à modifier un événement.";
      return RedirectToAction("Index");
    }

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

  public async Task<ActionResult> Delete(int id)
  {
    if (!await _userService.GetCurrentUserIsTeacher())
    {
      TempData["Error"] = "Vous n'êtes pas autorisé à supprimer un événement.";
      return RedirectToAction("Index");
    }

    var eventObject = _context.Events.Find(id);
    _context.Events.Remove(eventObject);
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public async Task<ActionResult> SubscribeToEvent(int id)
  {
    var eventObject = _context.Events.Find(id);
    var user = await _userService.GetCurrentStudent();
    
    var participant = new ParticipantModel(_context)
    {
      EventId = eventObject.Id,
      StudentId = user.Id
    };

    if (eventObject.ParticipantsCount >= eventObject.MaxParticipants)
    {
      TempData["Info"] = "L'événement est complet.";
      return RedirectToAction("Index");
    }

    eventObject.ParticipantsCount++;
    _context.Participants.Add(participant);
    TempData["Success"] = "Vous êtes inscrit à l'événement.";
    _context.SaveChanges();
    return RedirectToAction("Index");
  }

  public async Task<ActionResult> UnsubscribeToEvent(int id)
  {
    var eventObject = _context.Events.Find(id);
    var user        = await _userService.GetCurrentStudent();
    var participant = _context.Participants.FirstOrDefault(p => p.EventId == eventObject.Id && p.StudentId == user.Id);

    if (participant == null)
    {
      TempData["Error"] = "Vous n'êtes pas inscrit à cet événement.";
      return RedirectToAction("Index");
    }

    eventObject.ParticipantsCount--;
    _context.Participants.Remove(participant);
    TempData["Success"] = "Vous êtes désinscrit de l'événement.";
    _context.SaveChanges();
    return RedirectToAction("Index");
  }
}