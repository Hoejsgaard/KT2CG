using System.Drawing;

namespace KT2CG.Scraping;

public class KillTeamRepo
{
    private readonly List<KillTeam?> _teams = new()
    {
        new("Novitiate", "https://wahapedia.ru/kill-team2/kill-teams/novitiate/", Color.DarkRed),
        new("Talons of the Emperor", "https://wahapedia.ru/kill-team2/kill-teams/talons-of-the-emperor/",
            Color.DarkOrange),
        new("Hunter Clade", "https://wahapedia.ru/kill-team2/kill-teams/hunter-clade/", Color.Red),
        new("Elucidian Starstrider", "https://wahapedia.ru/kill-team2/kill-teams/elucidian-starstrider/", Color.Orange),
        new("Veteran Guardsmen", "https://wahapedia.ru/kill-team2/kill-teams/veteran-guardsman/", Color.RoyalBlue),
        new("Kasrkin", "https://wahapedia.ru/kill-team2/kill-teams/kasrkin/", Color.CornflowerBlue),
        new("Grey Knight", "https://wahapedia.ru/kill-team2/kill-teams/grey-knight/", Color.Gray),
        new("Imperial Navy Breacher", "https://wahapedia.ru/kill-team2/kill-teams/imperial-navy-breacher/",
            Color.DarkBlue),
        new("Intercession Squad", "https://wahapedia.ru/kill-team2/kill-teams/intercession-squad/", Color.Blue),
        new("Phobos Strike Team", "https://wahapedia.ru/kill-team2/kill-teams/phobos-strike-team/", Color.CadetBlue),
        new("Gellerpox Infected", "https://wahapedia.ru/kill-team2/kill-teams/gellerpox-infected/", Color.GreenYellow),
        new("Blooded", "https://wahapedia.ru/kill-team2/kill-teams/blooded/", Color.MediumVioletRed),
        new("Legionary", "https://wahapedia.ru/kill-team2/kill-teams/legionary/", Color.SaddleBrown),
        new("Death Guard", "https://wahapedia.ru/kill-team2/kill-teams/death-guard/", Color.DarkGreen),
        new("Warpcoven", "https://wahapedia.ru/kill-team2/kill-teams/warpcoven/", Color.Yellow),
        new("Corsair Voidscarred", "https://wahapedia.ru/kill-team2/kill-teams/corsair-voidscarred/",
            Color.YellowGreen),
        new("Wyrmblade", "https://wahapedia.ru/kill-team2/kill-teams/wyrmblade/", Color.ForestGreen),
        new("Farstalker Kinband", "https://wahapedia.ru/kill-team2/kill-teams/farstalker-kinband/", Color.RosyBrown),
        new("Kommando", "https://wahapedia.ru/kill-team2/kill-teams/kommando/", Color.DarkOliveGreen),
        new("Pathfinder", "https://wahapedia.ru/kill-team2/kill-teams/pathfinder/", Color.LightGray)
    };

    public List<KillTeam> GetAll()
    {
        return _teams;
    }

    public List<KillTeam> Get(string name)
    {
        return new List<KillTeam>
        {
            _teams.Find(t => t.Name == name)
        };
    }
}