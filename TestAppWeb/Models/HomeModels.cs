namespace TestAppWeb.Models;

public class HomeIndexModel
{
    public Dictionary<string, string> Env        { get; set; }
    public IPA[]                      Interfaces { get; set; }
}

public class IPA
{
    public string Name      { get; set; }
    public string Addresses { get; set; }
}