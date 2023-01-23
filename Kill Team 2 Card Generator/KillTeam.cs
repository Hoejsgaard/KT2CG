using System.Drawing;

namespace KT2CG;

public class KillTeam
{
	public KillTeam(string name, string url, Color color)
	{
		Name = name;
		Url = url;
		Color = color;
	}

	public string Name { get; set; }
	public string Url { get; set; }

	public Color Color { get; set; }

	public List<Equipment> Equipment { get; set; } = new();
	public Ploys Ploys { get; set; } = new();
	public List<TacOps> TacOps { get; set; } = new();
}