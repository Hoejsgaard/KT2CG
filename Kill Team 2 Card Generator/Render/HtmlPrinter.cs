using System.Text;
using System.Text.Json;
using KT2CG.Scraping;

namespace KT2CG.Render;

/// <summary>
///  Hit it with a hammer - who needs frameworks
/// </summary>
public class HtmlPrinter
{
	private readonly List<KillTeam> _killTeams;
	private readonly string _outputFolder;

	public HtmlPrinter(string path, string outputFolder)
	{
		_outputFolder = outputFolder;
		using (var file = File.OpenText(path))
		{
			var json = file.ReadToEnd();
			_killTeams = JsonSerializer.Deserialize<List<KillTeam>>(json);
			Console.WriteLine($"Printer initialized with {_killTeams.Count} kill teams");
		}
	}

	public HtmlPrinter(List<KillTeam> killTeams, string outputFolder)
	{
		_killTeams = killTeams;
		_outputFolder = outputFolder;
		Console.WriteLine($"Printer initialized with {_killTeams.Count} kill teams");
	}

	public void Print()
	{
		StringBuilder stringBuilder = new StringBuilder();

		// do the HTML magic

		stringBuilder.Append(@$"
		<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN""
		  ""http://www.w3.org/TR/html4/strict.dtd"">
		<HTML>
		  <HEAD />
		  <BODY>
			{RenderPages()}
		  </BODY>
		</HTML>");

		
		Directory.CreateDirectory( _outputFolder );
		File.WriteAllText(_outputFolder + "\\teams.html", stringBuilder.ToString());
	}

	private string RenderPages()
	{
  // foreach faction
  // Render equipment in american mini size on A4
  // - Only one of eqipment ending in \u002B
  // - 5 of other eqipments OR based on input options.
  // - Calculate how many cards there are. Figure out how many pages are needed. Render
  // Render Tac Ops, Strategic Ploys and Tactical Ploys in Tarot sized cards on A4. There should be 4 on a A4 page.
  return "";
	}

}