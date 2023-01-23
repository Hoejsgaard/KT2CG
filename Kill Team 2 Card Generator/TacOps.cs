namespace KT2CG;

public class TacOps
{
	public string Name { get; set; }
	public string Number { get; set; }
	public string Description { get; set; }
	public string[] Conditions { get; set; } = new string[2];
	public List<Ability> Abilities { get; set; } = new List<Ability>();
}