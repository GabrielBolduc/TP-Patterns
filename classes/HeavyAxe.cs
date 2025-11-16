// Une autre arme de base, pour la variété.
public class HeavyAxe : IWeapon
{
    public int GetBaseDamage()
    {
        // Une hache frappe plus fort
        return 8;
    }

    public string GetDescription()
    {
        return "Hache lourde";
    }
}