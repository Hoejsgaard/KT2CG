namespace KT2CG;

public class Equipment
{
	public string Name { get; set; }
	public string Cost { get; set; }
	public string Description { get; set; }

	public List<Weapon> Weapons { get; set; } = new List<Weapon>();

	public List<Ability> Abilities { get; set; } = new List<Ability>();
}