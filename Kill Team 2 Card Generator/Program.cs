// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using KT2CG;
using KT2CG.Render;
using KT2CG.Scraping;

Console.WriteLine("HOLA! Let's get some eqipment data, shall we?");
string dataPath = "c:\\tmp\\killTeamsData.json";
string htmlPath = "c:\\tmp\\killTeamsCards";
var repo = new KillTeamRepo();
//var teams = repo.GetAll();
var teams = repo.Get("Novitiate");
var scraper = new Scraper(teams);
scraper.Scrape();

var json = JsonSerializer.Serialize(teams);
using (var writer = new StreamWriter(dataPath))
{
	writer.Write(json);
}

foreach (var killTeam in teams)
{
	Console.WriteLine($"------ {killTeam.Name} --------");
	Console.WriteLine($"Fetched {killTeam.Equipment.Count} pieces of equipment");
	Console.WriteLine($"Fetched {killTeam.TacOps.Count} Tac Ops");
	Console.WriteLine($"Fetched {killTeam.Ploys.StrategicPloys.Count} strategic ploys");
	Console.WriteLine($"Fetched {killTeam.Ploys.TacticalPloys.Count} tactical ploys");
	Console.WriteLine();
}

var htmlPrinter = new HtmlPrinter(dataPath, htmlPath);
htmlPrinter.Print();

Console.WriteLine("That was fun. Press any key to exit");
Console.ReadKey();