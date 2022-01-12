using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestAppWeb.Models;

namespace TestAppWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var envs = Environment.GetEnvironmentVariables();
        var r    = new Dictionary<string, string>();
        foreach (var key in envs.Keys.OfType<string>())
            r.Add(key, envs[key]!.ToString()!);
        return View(r);
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