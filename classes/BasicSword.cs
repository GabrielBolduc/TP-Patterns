// Une arme de base simple
public class BasicSword : IWeapon
{
    public int GetBaseDamage()
    {
        // Retourne un dégât de base
        return 5;
    }

    public string GetDescription()
    {
        return "Épée de base";
    }
}