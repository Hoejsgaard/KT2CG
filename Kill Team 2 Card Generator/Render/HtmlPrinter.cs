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
		// Render equipment in american mini size on A4
		// - Only one of eqipment ending in \u002B
		// - 5 of other eqipments OR based on input options.
		// - Calculate how many cards there are. Figure out how many pages are needed. Render
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
			   size: 297mm 210mm;
			   margin: 30mm 45mm 30mm 45mm;
			 }

			 .grid {
				  display: grid;
          grid-template-columns: repeat(4, 1fr);
          grid-template-rows: repeat(4, 1fr);
            
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
          linear-gradient(315deg, transparent 13px, #FFFFFF 0)
			  }
			  ";
	}
}