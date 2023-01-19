// See https://aka.ms/new-console-template for more information

using KT2CG;

Console.WriteLine("Hello, World!");
KillTeamRepo repo = new KillTeamRepo();
ScraperOptions options = new ScraperOptions();
options.KillTeams.Add(repo.Get("Blooded"));
var scraper = new Scraper(options);
var equipmentList = scraper.GetEquipment();

Console.WriteLine();
Console.WriteLine("Hey buddy! You crawled whapedia for equipment! Good job you!");
Console.WriteLine("Here are the " + equipmentList.Count + " pieces of equipment I found for Blooded");
Console.WriteLine();
Console.WriteLine();

foreach (var killTeam in options.KillTeams)
{
	Console.Write("Equipment for " + killTeam.Name);
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

Console.ReadKey();