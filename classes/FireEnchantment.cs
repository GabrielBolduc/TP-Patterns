public class FireEnchantment : WeaponDecorator
{
    public FireEnchantment(IWeapon weapon) : base(weapon) { }

    public override int GetBaseDamage()
    {
        return base.GetBaseDamage() + 4;
    }

    public override string GetDescription()
    {
        return base.GetDescription() + " (enflammée)";
    }
}