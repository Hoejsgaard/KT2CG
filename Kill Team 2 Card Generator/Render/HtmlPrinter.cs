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
		var stringBuilder = new StringBuilder();

		// do the HTML magic

		stringBuilder.Append(@$"
		<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN""
		  ""http://www.w3.org/TR/html4/strict.dtd"">
		<HTML>
		  <HEAD>
		  <STYLE>
			  {RenderStyles()}
		  </STYLE>
		  </HEAD>
		  <BODY>
			{RenderPages()}
		  </BODY>
		</HTML>");


		Directory.CreateDirectory(_outputFolder);
		File.WriteAllText(_outputFolder + "\\teams.html", stringBuilder.ToString());
	}

	private string RenderPages()
	{
		// foreach faction
		// Render equipment in american mini size in chunks of A4 (my guess is 4x4 on a page)
		// - Only one of equipment ending in \u002B
		// - 5 of other equipments OR as many as you can wholly buy for 10 EP OR based on input options. If multiple EP, just assume 4.
		// Render Tac Ops, Strategic Ploys and Tactical Ploys in Tarot sized cards on A4. There should be 4 on a A4 page.
		var testdata = @"<div class='page'>
					  <div class='grid'>"
			;

		for (var i = 0; i < 16; i++)
			testdata += @" <div class='equipment'>
					   <div class='equipmentInside'>
					     <div class='name'>Horse head</div>
	          		     <div class='EP'>1</div>
					     <div class='description'> Foo bar bas </div>
	          	       </div>
				   </div>
	          ";

		testdata += "</div>" +
		            "</div>";

		return testdata;
	}


	private string RenderStyles()
	{
		return @"
		 .page {
			 width: 21cm;
			 height: 29.7cm;
			 display: block;
			 margin: 0.5cm 0.5cm 0.5cm 0.5cm;
			 box-shadow: 0 0 0.5cm rgba(0, 0, 0, 0.5);
			 padding: 1cm 1cm 1cm 1cm;
		}
		 .grid {
			 display: grid;
			 grid-template-columns: repeat(4, 44mm);
			 grid-template-rows: repeat(4, 63mm);
			 gap: 0 0;
			 grid-auto-flow: column;
			 grid-auto-columns: 44mm;
		}
		 .equipment {
			 padding: 2px;
			 color: #c54c21;
			 height: 63mm;
			 width: 44mm;
			 background: linear-gradient(-45deg, transparent 14px, #c54c21 0) 
		}
		 .equipmentInside {
			 display: block;
			 padding: 0px;
			 height: 63mm;
			 background: linear-gradient(-45deg, transparent 13px, #FFFFFF 0);
		}
		 .name {
			 font-size: 120%;
			 color: white;
			 background: #c54c21 
		}
		 .EP {
			 color: black;
		}
		 .description{
			 color: black;
			 background:linear-gradient(315deg, transparent 13px, #FFFFFF 0) 
		}

			  ";
	}
}