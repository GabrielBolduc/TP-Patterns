// un autre decorateur
public class SharpnessEnchantment : WeaponDecorator
{
    public SharpnessEnchantment(IWeapon weapon) : base(weapon) { }

    public override int GetBaseDamage()
    {
        // degats de l'arme + degats de perforation
        return base.GetBaseDamage() + 2;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + " (aiguiser)";
    }
}