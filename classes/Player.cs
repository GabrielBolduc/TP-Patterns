using System;
using System.Collections.Generic;

// Le Joueur IMPLÉMENTE ISubject.
// Il sera observé.
// C'est l'équivalent de ta classe Player.cs précédente
public class Player : ISubject
{
    public string Name { get; private set; }
    private int _health;

    // Liste de tous les objets qui observent ce joueur
    private List<IObserver> _observers = new List<IObserver>();

    public Player(string name)
    {
        this.Name = name;
        // On utilise le Singleton (Étape 1) pour définir la vie de base
        this._health = GameSettings.Instance.BaseHealth;
    }

    // Propriété publique pour lire la vie
    public int Health
    {
        get { return _health; }
        private set
        {
            // On s'assure que la vie ne descend pas sous 0
            _health = Math.Max(0, value);
        }
    }

    // C'est l'action qui déclenche la notification
    public void TakeDamage(int damage)
    {
        // On utilise le Singleton (Étape 1) pour le modificateur
        int actualDamage = (int)(damage * GameSettings.Instance.DamageModifier);
        this.Health -= actualDamage;
        
        Console.WriteLine($"\t{Name} subit {actualDamage} points de dégâts ! PV restants : {Health}");

        // L'état du joueur a changé, il notifie tous ses observateurs !
        Notify();
    }

    // --- Implémentation de l'interface ISubject ---

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        // On fait une boucle sur tous les observateurs
        // et on appelle leur méthode Update().
        // On passe 'this' (le joueur lui-même) en paramètre.
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}