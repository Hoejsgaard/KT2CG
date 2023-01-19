using System.Drawing;

namespace KT2CG;

public class KillTeam
{
	public string Name { get; set; }
	public string Url { get; set; }

	public Color Color { get; set; }

	public KillTeam(string name, string url, Color color)
	{
		Name = name;
		Url = url;
		Color = color;
	}
}