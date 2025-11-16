// Une arme de base, simple.
// C'est l'équivalent de ton "Regular.cs"
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