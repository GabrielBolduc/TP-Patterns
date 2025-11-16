// C'est notre "CondimentDecorator"
// Il implémente IWeapon et contient (wrap) une autre IWeapon.
public abstract class WeaponDecorator : IWeapon
{
    protected IWeapon _wrappedWeapon;

    public WeaponDecorator(IWeapon weapon)
    {
        _wrappedWeapon = weapon;
    }

    // Par défaut, on délègue les appels à l'objet enveloppé
    public virtual int GetBaseDamage()
    {
        return _wrappedWeapon.GetBaseDamage();
    }

    public virtual string GetDescription()
    {
        return _wrappedWeapon.GetDescription();
    }
}