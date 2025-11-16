public class FireEnchantment : WeaponDecorator
{
    public FireEnchantment(IWeapon weapon) : base(weapon) { }

    // On decore les methodes de base
    public override int GetBaseDamage()
    {
        // Degats de l'arme + degats de feu
        return base.GetBaseDamage() + 4;
    }

    public override string GetDescription()
    {
        // Description de l'arme + description du feu
        return base.GetDescription() + " (enflammée)";
    }
}