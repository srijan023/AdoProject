using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using adoProject.Models;

namespace adoProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Student std)
    {
        if (ModelState.IsValid)
        {
            StudentDBAccess dbAccess = new StudentDBAccess();
            string response = dbAccess.AddStudentData(std);
            Console.WriteLine("Response " + response);
            return RedirectToAction("Index");
        }
        return View(std);
    }

    [HttpGet]
    public IActionResult Data()
    {
        List<Student> students = new List<Student>();
        StudentDBAccess dbAccess = new StudentDBAccess();
        students = dbAccess.GetStudentData();
        return View(students);
    }

// Because html form does not have delete method
    [HttpPost]
    public IActionResult Delete(int id){
      StudentDBAccess dbAccess = new StudentDBAccess();
      dbAccess.DeleteStudentData(id);
      return RedirectToAction("Data");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
