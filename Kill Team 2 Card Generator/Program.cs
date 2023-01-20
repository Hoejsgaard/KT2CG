﻿// See https://aka.ms/new-console-template for more information

using System.Drawing;
using System.Text.Json;
using KT2CG;

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
KillTeamRepo repo = new KillTeamRepo();
ScraperOptions options = new ScraperOptions();
options.KillTeams.Add(repo.Get("Talons of the Emperor"));
//options.KillTeams = repo.GetAll();
var scraper = new Scraper(options);
var equipmentList = scraper.GetEquipment();

string json = JsonSerializer.Serialize(equipmentList);
using (StreamWriter writer = new StreamWriter("c:\\tmp\\eqipment.json"))
{
	writer.Write(json);
}

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