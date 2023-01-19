﻿using System.Text;
using HtmlAgilityPack;

namespace KT2CG;

public class Scraper
{
	private readonly ScraperOptions _options;

	public Scraper(ScraperOptions options)
	{
		_options = options;
	}


	public Dictionary<string, List<Equipment>> GetEquipment()
	{
		
		Dictionary<string, List<Equipment>> killTeamsEquipment = new Dictionary<string, List<Equipment>>();
		foreach (var killTeam in _options.KillTeams)
		{
			var equipmentList = new List<Equipment>();
			HtmlDocument _doc;
			var page = killTeam.Url;
			var web = new HtmlWeb();
			_doc = web.Load(page);
			var equipmentDivs = _doc.DocumentNode.SelectNodes(
				"//h2[text()='Equipment']/following-sibling::div[@class='Columns2']/div[@class='BreakInsideAvoid ']");


			foreach (var equipmentDiv in equipmentDivs)
			{
				var equipment = new Equipment();

				var nameNode = equipmentDiv.SelectSingleNode(".//p[@class='h_relic']");
				equipment.Name = nameNode.InnerText.Trim();

				var costStart = equipment.Name.IndexOf("[") + 1;
				var costEnd = equipment.Name.IndexOf("]");
				equipment.Cost = equipment.Name.Substring(costStart, costEnd - costStart);
				equipment.Name = equipment.Name.Substring(0, costStart - 1).Trim();

				equipmentDiv.RemoveChild(nameNode);


				var brs = equipmentDiv.SelectNodes(".//br");
				if (brs != null)
					foreach (var child in brs)
						child.ParentNode.ReplaceChild(HtmlNode.CreateNode(Environment.NewLine), child);

				var tables = equipmentDiv.SelectNodes(".//div//table[@class='wTable']");
				if (tables != null)
					foreach (var table in tables)
						ReplaceTableWithText(table);

				equipment.Description = equipmentDiv.InnerText.Trim();

				equipmentList.Add(equipment);
			}
			killTeamsEquipment.Add(killTeam.Name, equipmentList);
		}

		return killTeamsEquipment;
	}


	public void ReplaceTableWithText(HtmlNode table)
	{
		var sb = new StringBuilder();
		sb.Append(Environment.NewLine + Environment.NewLine);
		var tbody = table.SelectNodes(".//tbody");
		if (tbody.Count == 2)
		{
			sb.AppendLine(tbody[1].SelectSingleNode(".//tr[1]//td[2]").InnerText);
			sb.AppendLine("Type  : " + (tbody[1].SelectSingleNode(".//tr[1]//td[1]").HasClass("wsDataRanged")
				? "Ranged"
				: "Melee"));
			sb.AppendLine("A     : " + tbody[1].SelectSingleNode(".//tr[1]//td[3]").InnerText);
			sb.AppendLine("BS/WS : " + tbody[1].SelectSingleNode(".//tr[1]//td[4]").InnerText);
			sb.AppendLine("D     : " + tbody[1].SelectSingleNode(".//tr[1]//td[5]").InnerText);
			sb.AppendLine("!     : " + tbody[1].SelectSingleNode(".//tr[3]//td[2]").InnerText);
			var specialRules = tbody[1].SelectSingleNode(".//tr[3]//td[1]").SelectNodes("./div/span");
			if (specialRules != null)
			{
				sb.Append("Special: ");
				foreach (var specialRule in specialRules)
				{
					var details = specialRule.SelectNodes("./span");
					sb.Append(details[0].InnerText);
					if (details.Count > 1)
					{
						var span = details[1].SelectSingleNode("./span");
						if (span.HasClass("f1"))
							sb.Append("△");
						if (span.HasClass("f2"))
							sb.Append("◯");
						if (span.HasClass("f3"))
							sb.Append("□");
						if (span.HasClass("f6"))
							sb.Append("⬠");
					}

					sb.Append(", ");
				}

				sb.Remove(sb.Length - 2, 2);
				sb.AppendLine();
			}
		}

		table.ParentNode.ReplaceChild(HtmlNode.CreateNode(sb.ToString()), table);
	}
}