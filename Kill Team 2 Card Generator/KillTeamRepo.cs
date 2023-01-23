using System.Drawing;

namespace KT2CG;

public class KillTeamRepo
{
	private List<KillTeam?> _teams = new List<KillTeam?>()
	{
		new KillTeam("Novitiate", "https://wahapedia.ru/kill-team2/kill-teams/novitiate/", Color.DarkRed),
		new KillTeam("Talons of the Emperor", "https://wahapedia.ru/kill-team2/kill-teams/talons-of-the-emperor/", Color.DarkOrange),
		new KillTeam("Hunter Clade", "https://wahapedia.ru/kill-team2/kill-teams/hunter-clade/", Color.Red),
		new KillTeam("Elucidian Starstrider", "https://wahapedia.ru/kill-team2/kill-teams/elucidian-starstrider/", Color.Orange),
		new KillTeam("Veteran Guardsmen", "https://wahapedia.ru/kill-team2/kill-teams/veteran-guardsman/", Color.RoyalBlue),
		new KillTeam("Kasrkin", "https://wahapedia.ru/kill-team2/kill-teams/kasrkin/", Color.CornflowerBlue),
		new KillTeam("Grey Knight", "https://wahapedia.ru/kill-team2/kill-teams/grey-knight/", Color.Gray),
		new KillTeam("Imperial Navy Breacher", "https://wahapedia.ru/kill-team2/kill-teams/imperial-navy-breacher/", Color.DarkBlue),
		new KillTeam("Intercession Squad", "https://wahapedia.ru/kill-team2/kill-teams/intercession-squad/", Color.Blue),
		new KillTeam("Phobos Strike Team", "https://wahapedia.ru/kill-team2/kill-teams/phobos-strike-team/", Color.CadetBlue),
		new KillTeam("Gellerpox Infected", "https://wahapedia.ru/kill-team2/kill-teams/gellerpox-infected/", Color.GreenYellow),
		new KillTeam("Blooded", "https://wahapedia.ru/kill-team2/kill-teams/blooded/", Color.MediumVioletRed),
		new KillTeam("Legionary", "https://wahapedia.ru/kill-team2/kill-teams/legionary/", Color.SaddleBrown),
		new KillTeam("Death Guard", "https://wahapedia.ru/kill-team2/kill-teams/death-guard/", Color.DarkGreen),
		new KillTeam("Warpcoven", "https://wahapedia.ru/kill-team2/kill-teams/warpcoven/", Color.Yellow),
		new KillTeam("Corsair Voidscarred", "https://wahapedia.ru/kill-team2/kill-teams/corsair-voidscarred/", Color.YellowGreen),
		new KillTeam("Wyrmblade", "https://wahapedia.ru/kill-team2/kill-teams/wyrmblade/", Color.ForestGreen),
		new KillTeam("Farstalker Kinband", "https://wahapedia.ru/kill-team2/kill-teams/farstalker-kinband/", Color.RosyBrown),
		new KillTeam("Kommando", "https://wahapedia.ru/kill-team2/kill-teams/kommando/", Color.DarkOliveGreen),
		new KillTeam("Pathfinder", "https://wahapedia.ru/kill-team2/kill-teams/pathfinder/", Color.LightGray),
	};

	public List<KillTeam> GetAll()
	{
		return _teams;
	}

	public List<KillTeam> Get(string name)
	{
		return new List<KillTeam>()
		{
			_teams.Find(t => t.Name == name)
		};
	}
}