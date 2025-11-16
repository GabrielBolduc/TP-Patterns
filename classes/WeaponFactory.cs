using System;

public static class WeaponFactory
{
    public static IWeapon CreateRandomWeapon()
    {
        Random rng = GameSettings.Instance.Rng;

        // choisir arme de base
        IWeapon weapon;
        if (rng.Next(0, 2) == 0) 
        {
            weapon = new BasicSword();
        }
        else
        {
            weapon = new HeavyAxe();
        }

        // 50% de chance d'ajouter un enchantement de feu
        if (rng.Next(0, 2) == 0)
        {
            weapon = new FireEnchantment(weapon);
        }

        // 33% de chance d'ajouter un enchantement aiguise
        if (rng.Next(0, 3) == 0)
        {
            weapon = new SharpnessEnchantment(weapon);
        }

        return weapon;
    }
}