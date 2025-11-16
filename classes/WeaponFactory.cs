using System;

// On crée une classe 'static' car elle n'a pas besoin
// d'être instanciée. C'est juste une boîte à outils.
public static class WeaponFactory
{
    // C'est la MÊME méthode qu'on avait dans Program.cs
    public static IWeapon CreateRandomWeapon()
    {
        // On utilise le Singleton (Étape 1) pour le Rng
        Random rng = GameSettings.Instance.Rng;

        // 1. Choisir une arme de base
        IWeapon weapon;
        if (rng.Next(0, 2) == 0) // 50% chance
        {
            weapon = new BasicSword();
        }
        else
        {
            weapon = new HeavyAxe();
        }

        // 2. 50% de chance d'ajouter un enchantement de feu
        if (rng.Next(0, 2) == 0)
        {
            weapon = new FireEnchantment(weapon);
        }

        // 3. 33% de chance d'ajouter un enchantement aiguisé
        if (rng.Next(0, 3) == 0)
        {
            weapon = new SharpnessEnchantment(weapon);
        }

        return weapon;
    }
}