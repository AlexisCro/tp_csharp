using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tpnote.Models;
using mvc.Models;
using mvc.Data;

namespace tpnote.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<TeacherModel> _teacherManager;
    private readonly UserManager<StudentModel> _studentManager;  
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, UserManager<TeacherModel> teacherManager, UserManager<StudentModel> studentManager,  ApplicationDbContext context)
    {
        _logger         = logger;
        _teacherManager = teacherManager;
        _studentManager = studentManager;
        _context        = context;
    }

    public IActionResult Index()
    {
      var events = _context.Events.ToList();
      return View(events);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
