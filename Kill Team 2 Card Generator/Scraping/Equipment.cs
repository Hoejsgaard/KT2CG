namespace KT2CG.Scraping;

public class Equipment
{
    public string Name { get; set; }
    public string Cost { get; set; }
    public string Description { get; set; }

    public List<Weapon> Weapons { get; set; } = new();

    public List<Ability> Abilities { get; set; } = new();
}