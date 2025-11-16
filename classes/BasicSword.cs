public class BasicSword : IWeapon
{
    public int GetBaseDamage()
    {
        // Retourne un degat de base
        return 5;
    }

    public string GetDescription()
    {
        return "Épée de base";
    }
}