// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using KT2CG;
using KT2CG.Render;
using KT2CG.Scraping;

//new KillTeam("Novitiate", "https://wahapedia.ru/kill-team2/kill-teams/novitiate/", Color.DarkRed),
//	new KillTeam("Talons of the Emperor", "https://wahapedia.ru/kill-team2/kill-teams/talons-of-the-emperor/", Color.DarkOrange),
//	new KillTeam("Hunter Clade", "https://wahapedia.ru/kill-team2/kill-teams/hunter-clade/", Color.Red),
//	new KillTeam("Elucidian Starstrider", "https://wahapedia.ru/kill-team2/kill-teams/elucidian-starstrider/", Color.Orange),
//	new KillTeam("Veteran Guardsmen", "https://wahapedia.ru/kill-team2/kill-teams/veteran-guardsman/", Color.RoyalBlue),
//	new KillTeam("Kasrkin", "https://wahapedia.ru/kill-team2/kill-teams/kasrkin/", Color.CornflowerBlue),
//	new KillTeam("Grey Knight", "https://wahapedia.ru/kill-team2/kill-teams/grey-knight/", Color.Gray),
//	new KillTeam("Imperial Navy Breacher", "https://wahapedia.ru/kill-team2/kill-teams/imperial-navy-breacher/", Color.DarkBlue),
//	new KillTeam("Intercession Squad", "https://wahapedia.ru/kill-team2/kill-teams/intercession-squad/", Color.Blue),
//	new KillTeam("Phobos Strike Team", "https://wahapedia.ru/kill-team2/kill-teams/phobos-strike-team/", Color.CadetBlue),
//	new KillTeam("Gellerpox Infected", "https://wahapedia.ru/kill-team2/kill-teams/gellerpox-infected/", Color.GreenYellow),
//	new KillTeam("Blooded", "https://wahapedia.ru/kill-team2/kill-teams/blooded/", Color.MediumVioletRed),
//	new KillTeam("Legionary", "https://wahapedia.ru/kill-team2/kill-teams/legionary/", Color.SaddleBrown),
//	new KillTeam("Death Guard", "https://wahapedia.ru/kill-team2/kill-teams/death-guard/", Color.DarkGreen),
//	new KillTeam("Warpcoven", "https://wahapedia.ru/kill-team2/kill-teams/warpcoven/", Color.Yellow),
//	new KillTeam("Corsair Voidscarred", "https://wahapedia.ru/kill-team2/kill-teams/corsair-voidscarred/", Color.YellowGreen),
//	new KillTeam("Wyrmblade", "https://wahapedia.ru/kill-team2/kill-teams/wyrmblade/", Color.ForestGreen),
//	new KillTeam("Farstalker Kinband", "https://wahapedia.ru/kill-team2/kill-teams/farstalker-kinband/", Color.RosyBrown),
//	new KillTeam("Kommando", "https://wahapedia.ru/kill-team2/kill-teams/kommando/", Color.DarkOliveGreen),
//	new KillTeam("Pathfinder", "https://wahapedia.ru/kill-team2/kill-teams/pathfinder/", Color.LightGray),

Console.WriteLine("HOLA! Let's get some eqipment data, shall we?");
string dataPath = "c:\\tmp\\killTeamsData.json";
string htmlPath = "c:\\tmp\\killTeamsCards";
var repo = new KillTeamRepo();
//var teams = repo.GetAll();
List<KillTeam> teams = repo.Get("Novitiate");
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