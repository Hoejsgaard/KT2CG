using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;
using KT2CG.Properties;
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
		File.WriteAllBytes(_outputFolder + "\\r1.png", Resources.f1);
		File.WriteAllBytes(_outputFolder + "\\r2.png", Resources.f2);
		File.WriteAllBytes(_outputFolder + "\\r3.png", Resources.f3);
		File.WriteAllBytes(_outputFolder + "\\r6.png", Resources.f6);
	}

	private List<Equipment> ExpandEqipment(List<Equipment> equipments)
	{
		List<Equipment> expandedEquipments = new List<Equipment>();
		const int equipmentPoints = 10;

		foreach (var equipment in equipments)
		{
			if (equipment.Name.EndsWith("\u002B"))
			{
				expandedEquipments.Add(equipment);
			}
			else if (int.TryParse(equipment.Cost, out var equipmentCost))
			{
				int copies = equipmentPoints / equipmentCost;
				for (int i = 0; i < copies; i++)
				{
					expandedEquipments.Add(equipment);
				}
			}
			else
			{
				int copies = 4;
				for (int i = 0; i < copies; i++)
				{
					expandedEquipments.Add(equipment);
				}
			}
		}

		return expandedEquipments;
	}


	private void RenderEqipmentPages(KillTeam killTeam, ref StringBuilder sb)
	{
		int itemsPerPage = 9;
		List<Equipment> items = ExpandEqipment(killTeam.Equipment);
		int pages = (items.Count + itemsPerPage - 1) / itemsPerPage;

		for (int i = 0; i < pages; i++)
		{
			int firstItem = i * itemsPerPage;
			var lastItem = firstItem + itemsPerPage;
			if (lastItem > items.Count)
				lastItem = items.Count;

			sb.Append(@"
		<div class='page'>
		   <div class='grid'>");

			for (var j = firstItem; j < lastItem; j++)
			{
				sb.Append(@$" 
			<div class='equipment'>
			  <div class='equipmentInside'>
				<div class='equipment-header'>
				 <div class='name'>{items[j].Name}</div>
				  <div class='EP'>[{items[j].Cost}]</div>
				</div>
				<div class='description'>{FormatDescription(items[j].Description) + FormatWeapons(items[j].Weapons)} </div>
			  </div>
			</div>");
			}

			sb.Append(@"
		  </div>
		</div>");
		}
	}

	private string FormatDescription(string description)
	{
		return description.ReplaceRangesWithSpans().ReplaceLineBreaksWithBreaks().BoldFromLineBreakToColon();
	}

	private string FormatWeapons(List<Weapon> weapons)
	{
		StringBuilder sb = new StringBuilder();
		foreach (var weapon in weapons)
		{
			sb.AppendLine("<br/><br/>");
			sb.AppendLine("<b>" + weapon.Name + "</b><br/>");
			sb.AppendLine(weapon.IsRanged ? "Ranged weapon<br/>" : "Melee weapons<br/>");
			sb.AppendLine("A:<span>&#160;&#160;&#160;&#160;</span>" + weapon.Attacks + "<br/>");
			sb.AppendLine("S:<span>&#160;&#160;&#160;&#160;</span>" + weapon.Skill + "<br/>");
			sb.AppendLine("D:<span>&#160;&#160;&#160;&#160;</span>" + weapon.Damage + "<br/>");

			string critRules = "";
			foreach (var rule in weapon.OnCrit)
			{
				critRules += rule + ", ";
			}

			critRules = critRules.Length > 2 ? critRules.Substring(0, critRules.Length - 2) : "-";

			sb.AppendLine("C:<span>&#160;&#160;&#160;&#160;</span>" + critRules + "<br/>");


			string specialRules = "";
			foreach (var rule in weapon.SpecailRules)
			{
				specialRules += rule + ", ";
			}

			specialRules = specialRules.Length > 0 ? specialRules.Substring(0, specialRules.Length - 2) : "-";

			sb.AppendLine("S:<span>&#160;&#160;&#160;&#160;</span>" + specialRules + "<br/>");
		}

		return sb.ToString().ReplaceRangesWithSpans();
	}

	private string RenderPages()
	{
		StringBuilder sb = new StringBuilder();
		foreach (var killTeam in _killTeams)
		{
			RenderEqipmentPages(killTeam, ref sb);
		}

		return sb.ToString();
	}


	private string RenderStyles()
	{
		return @"
		 .page {
			 min-width: 21cm;
			 min-height: 29.7cm;
             max-width: 21cm;
			 max-height: 29.7cm;
			 display: block;
			 margin: 0.5cm 0.5cm 0.5cm 0.5cm;
			 /* box-shadow: 0 0 0.5cm rgba(0, 0, 0, 0.5); */
			 padding: 1cm 1cm 1cm 1cm;
		}
		 .grid {
			 display: grid;
			 grid-template-columns: repeat(auto-fit, 57mm);
			 grid-template-rows: repeat(auto-fit, 89mm);
			 gap: 0 0;
		}
		 .equipment {
			 padding: 2px;
			 color: #c54c21;
			 height: 89mm;
			 width: 57mm;
			 background: linear-gradient(-45deg, transparent 14px, #c54c21 0) 
		}
		 .equipmentInside {
			 display: block;
			 padding: 0px;
			 height: 89mm;
			 background: linear-gradient(-45deg, transparent 13px, #FFFFFF 0);
		}
         .equipment-header {
		     display: flex;
		     justify-content: space-between;
			 background: #c54c21;
			 align-items: center;
			 padding: 5px 5px;
			 height: 40px;
			 font-size: 100%;
			 font-weight: bold;
		 }	 
		 .name, .EP {
			 font-size: 100%;
			 color: white;
			 font-weight: normal;
		}
		 .description{
			 color: black;
			 padding: 5px 5px;
			 background:linear-gradient(315deg, transparent 13px, #FFFFFF 0) 
		}

		.range1 {
			 display: inline-block;
			 height: 19px;
			 width: 22px;
			 vertical-align: middle;
			 background-image: url(r1.png);
		}

		.range2 {
			 display: inline-block;
			 height: 19px;
			 width: 19px;
			 vertical-align: middle;
			 background-image: url(r2.png);
		}

		.range3 {
			 display: inline-block;
			 height: 19px;
			 width: 19px;
			 vertical-align: middle;
			 background-image: url(r3.png);
		}

		.range6 {
			 display: inline-block;
			 height: 19px;
			 width: 22px;
			 vertical-align: middle;
			 background-image: url(r6.png);
		}
		 ";
	}
}