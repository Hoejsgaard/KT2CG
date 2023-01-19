// See https://aka.ms/new-console-template for more information

using KT2CG;

Console.WriteLine("HOLA! Let's get some eqipment data, shall we?");
KillTeamRepo repo = new KillTeamRepo();
ScraperOptions options = new ScraperOptions();
//options.KillTeams.Add(repo.Get("Farstalker Kinband"));
options.KillTeams = repo.GetAll();
var scraper = new Scraper(options);
var equipmentList = scraper.GetEquipment();

foreach (var killTeam in options.KillTeams)
{
	Console.WriteLine("--------------------------------------------------------------");
	Console.WriteLine("------------EQUIPMENT FOR " + killTeam.Name + "---------------");
	Console.WriteLine("--------------------------------------------------------------");

 Console.WriteLine();
	foreach (var equipment in equipmentList[killTeam.Name])
	{
		Console.WriteLine("Equipment:");
		Console.WriteLine("Name: " + equipment.Name);
		Console.WriteLine("Cost: " + equipment.Cost);
		Console.WriteLine("Description: " + Environment.NewLine + equipment.Description);
		Console.WriteLine();
	}

}

Console.WriteLine("That was fun. Press any key to exit");
Console.ReadKey();