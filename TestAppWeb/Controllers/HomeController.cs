using System.Diagnostics;
using System.Net.NetworkInformation;
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

        var m = new HomeIndexModel() { Env = new Dictionary<string, string>() };
        foreach (var key in envs.Keys.OfType<string>())
            m.Env.Add(key, envs[key]!.ToString()!);

        m.Interfaces = NetworkInterface.GetAllNetworkInterfaces()
                                       .Select(p =>
                                       {
                                           var a = p.GetIPProperties().UnicastAddresses;
                                           return new IPA()
                                           {
                                               Name      = p.Name + ": " + p.NetworkInterfaceType,
                                               Addresses = !a.Any() ? "-" : string.Join("<br/>", a.Select(add => add.Address.ToString()))
                                           };
                                       })
                                       .ToArray();

        return View(m);
    }

    public async Task<IActionResult> Data()
    {
        var c = new HttpClient();
        var r = await c.GetFromJsonAsync<WeatherForecast[]>("http://hostapi:7500/WeatherForecast");
        return View(r);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}