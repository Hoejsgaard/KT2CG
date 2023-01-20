namespace KT2CG;

public class Weapon
{
	public string Name { get; set; }
	public bool IsRanged { get; set; }
	public string Attacks { get; set; }
	public string Skill { get; set; }
	public string Damage { get; set; }

	public List<string> OnCrit { get; set; } = new();

	public List<string> SpecailRules { get; set; } = new();
}