// Un autre décorateur, pour la créativité.
public class SharpnessEnchantment : WeaponDecorator
{
    public SharpnessEnchantment(IWeapon weapon) : base(weapon) { }

    public override int GetBaseDamage()
    {
        // Dégâts de l'arme + dégâts de perforation
        return base.GetBaseDamage() + 2;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + " (aiguisée)";
    }
}