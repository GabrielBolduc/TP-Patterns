// C'est notre "Chocolate" ou "WhippedCream"
public class FireEnchantment : WeaponDecorator
{
    public FireEnchantment(IWeapon weapon) : base(weapon) { }

    // On "décore" les méthodes de base
    public override int GetBaseDamage()
    {
        // Dégâts de l'arme + dégâts de feu
        return base.GetBaseDamage() + 4;
    }

    public override string GetDescription()
    {
        // Description de l'arme + description du feu
        return base.GetDescription() + " (enflammée)";
    }
}