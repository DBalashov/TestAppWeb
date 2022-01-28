using System.Diagnostics;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using TestAppWeb.Models;

namespace TestAppWeb.Controllers;

public class HomeController : Controller
{
    readonly ILogger<HomeController> _logger;

    static readonly List<byte[]> l = new();

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
        l.Add(new byte[1048576*10]);

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