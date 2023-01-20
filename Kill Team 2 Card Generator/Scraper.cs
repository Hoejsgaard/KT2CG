using System.Text;
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
		var killTeamsEquipment = new Dictionary<string, List<Equipment>>();
		foreach (var killTeam in _options.KillTeams)
		{
			Console.WriteLine("Working on scraping " + killTeam.Name);
			Console.WriteLine();
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


				var htmlBreaks = equipmentDiv.SelectNodes(".//br");
				if (htmlBreaks != null)
					foreach (var child in htmlBreaks)
						child.ParentNode.ReplaceChild(HtmlNode.CreateNode(Environment.NewLine), child);

				var assumedEquipmentTables = equipmentDiv.SelectNodes(".//div//table[@class='wTable']");
				if (assumedEquipmentTables != null)
					foreach (var table in assumedEquipmentTables)
						ExtractWeapons(table, equipment);

				var assumedAbilities = equipmentDiv.SelectNodes(".//div/div[@class='Corner24_in']");
				if (assumedAbilities != null)
					foreach (var ability in assumedAbilities)
					{
						ExtractAbility(ability, equipment);
						// Remove the extracted text
						ability.ParentNode.ReplaceChild(HtmlNode.CreateNode(""), ability);
					}

				ReplaceDistanceShapes(equipmentDiv);
				equipment.Description = equipmentDiv.InnerText.Trim();

				equipmentList.Add(equipment);
			}

			killTeamsEquipment.Add(killTeam.Name, equipmentList);
		}

		return killTeamsEquipment;
	}

	private void ExtractAbility(HtmlNode ability, Equipment equipment)
	{
		var headerNode = ability.SelectSingleNode(".//h3");
		var descriptionNode = ability.SelectSingleNode(".//div");
		var apNode = headerNode.SelectSingleNode(".//span");
		apNode.ParentNode.ReplaceChild(HtmlNode.CreateNode(""), apNode);

		ReplaceDistanceShapes(descriptionNode);
		var description = descriptionNode.InnerText.Trim();


		equipment.Abilities.Add(new Ability
		{
			AP = apNode.InnerText,
			Description = description,
			Name = headerNode.InnerText
		});
	}

	public void ReplaceDistanceShapes(HtmlNode node)
	{
		var circles = node.SelectNodes(".//span[@class='f1']");
		var triangles = node.SelectNodes(".//span[@class='f2']");
		var squares = node.SelectNodes(".//span[@class='f3']");
		var pentagons = node.SelectNodes(".//span[@class='f6']");
		ReplaceDistanceShape(triangles, "△");
		ReplaceDistanceShape(circles, "◯");
		ReplaceDistanceShape(squares, "□");
		ReplaceDistanceShape(pentagons, "⬠");
	}

	private void ReplaceDistanceShape(HtmlNodeCollection? nodes, string replacement)
	{
		if (nodes == null)
			return;
		foreach (var node in nodes) node.ParentNode.ReplaceChild(HtmlNode.CreateNode(replacement), node);
	}


	public void ExtractWeapons(HtmlNode table, Equipment equipment)
	{
		var tbody = table.SelectNodes(".//tbody");
		if (tbody.Count == 2)
		{
			var weapon = new Weapon();

			var name = tbody[1].SelectSingleNode(".//tr[1]//td[2]").InnerText;
			weapon.Name = name;
			weapon.IsRanged = tbody[1].SelectSingleNode(".//tr[1]//td[1]").HasClass("wsDataRanged");
			weapon.Attacks = tbody[1].SelectSingleNode(".//tr[1]//td[3]").InnerText;
			weapon.Skill = tbody[1].SelectSingleNode(".//tr[1]//td[4]").InnerText;
			weapon.Damage = tbody[1].SelectSingleNode(".//tr[1]//td[5]").InnerText;

			var critRules = tbody[1].SelectSingleNode(".//tr[3]//td[2]")?.SelectNodes("./div/span");
			if (critRules != null)

				foreach (var critRule in critRules)
				{
					var details = critRule.SelectNodes("./span");
					weapon.OnCrit.Add(details[0].InnerText);
				}

			var specialRules = tbody[1].SelectSingleNode(".//tr[3]//td[1]")?.SelectNodes("./div/span");
			if (specialRules != null)
				foreach (var specialRule in specialRules)
				{
					var details = specialRule.SelectNodes("./span");
					if (details == null) //e.g., farstalker kinband, Kroot pistol
					{
						var rule = specialRule.ParentNode.InnerText;

						if (specialRule.HasClass("f1"))
							rule += "△";
						if (specialRule.HasClass("f2"))
							rule += "◯";
						if (specialRule.HasClass("f3"))
							rule += "□";
						if (specialRule.HasClass("f6"))
							rule += "⬠";
						else
							rule += specialRule.InnerText;

						weapon.SpecailRules.Add(rule);
					}
					else
					{
						var rule = details[0].InnerText;

						if (details.Count > 1)
						{
							var span = details[1].SelectSingleNode("./span");
							if (span != null)
							{
								if (span.HasClass("f1"))
									rule += "△";
								if (span.HasClass("f2"))
									rule += "◯";
								if (span.HasClass("f3"))
									rule += "□";
								if (span.HasClass("f6"))
									rule += "⬠";
								else
									rule += span.InnerText;
							}
						}

						weapon.SpecailRules.Add(rule);
					}
				}

			equipment.Weapons.Add(weapon);
		}

		table.ParentNode.ReplaceChild(HtmlNode.CreateNode(""), table);
	}

	public void ReplaceTableWithText(HtmlNode table)
	{
		var sb = new StringBuilder();
		sb.Append(Environment.NewLine + Environment.NewLine);
		var tbody = table.SelectNodes(".//tbody");
		if (tbody.Count == 2)
		{
			var name = tbody[1].SelectSingleNode(".//tr[1]//td[2]").InnerText;
			sb.AppendLine(name);
			sb.AppendLine("Type  : " + (tbody[1].SelectSingleNode(".//tr[1]//td[1]").HasClass("wsDataRanged")
				? "Ranged"
				: "Melee"));
			sb.AppendLine("A     : " + tbody[1].SelectSingleNode(".//tr[1]//td[3]").InnerText);
			sb.AppendLine("BS/WS : " + tbody[1].SelectSingleNode(".//tr[1]//td[4]").InnerText);
			sb.AppendLine("D     : " + tbody[1].SelectSingleNode(".//tr[1]//td[5]").InnerText);
			sb.Append("!     : ");
			var critRules = tbody[1].SelectSingleNode(".//tr[3]//td[2]")?.SelectNodes("./div/span");
			if (critRules == null)
			{
				sb.Append("-");
			}
			else
			{
				foreach (var critRule in critRules)
				{
					var details = critRule.SelectNodes("./span");
					sb.Append(details[0].InnerText);
					sb.Append(", ");
				}

				sb.Remove(sb.Length - 2, 2);
			}

			sb.AppendLine();

			var specialRules = tbody[1].SelectSingleNode(".//tr[3]//td[1]")?.SelectNodes("./div/span");
			if (specialRules != null)
			{
				sb.Append("Special: ");
				foreach (var specialRule in specialRules)
				{
					var details = specialRule.SelectNodes("./span");
					if (details == null) //e.g., farstalker kinband, Kroot pistol
					{
						sb.Append(specialRule.ParentNode.InnerText);
						if (specialRule.HasClass("f1"))
							sb.Append("△");
						if (specialRule.HasClass("f2"))
							sb.Append("◯");
						if (specialRule.HasClass("f3"))
							sb.Append("□");
						if (specialRule.HasClass("f6"))
							sb.Append("⬠");
						else
							sb.Append(specialRule.InnerText);
					}
					else
					{
						sb.Append(details[0].InnerText);

						if (details.Count > 1)
						{
							var span = details[1].SelectSingleNode("./span");
							if (span != null)
							{
								if (span.HasClass("f1"))
									sb.Append("△");
								if (span.HasClass("f2"))
									sb.Append("◯");
								if (span.HasClass("f3"))
									sb.Append("□");
								if (span.HasClass("f6"))
									sb.Append("⬠");
								else
									sb.Append(span.InnerText);
							}
						}
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