using System;
using System.Collections.Generic;

// le Joueur implemente ISubject.
// il sera observer
public class Player : ISubject
{
    public string Name { get; private set; }
    private int _health;

    // liste de objets qui observent ce joueur
    private List<IObserver> _observers = new List<IObserver>();

    public Player(string name)
    {
        this.Name = name;
        // singleton pour définir la vie de base
        this._health = GameSettings.Instance.BaseHealth;
    }

    public int Health
    {
        get { return _health; }
        private set
        {
            _health = Math.Max(0, value);
        }
    }

    // declenche la notification
    public void TakeDamage(int damage)
    {
        // singleton pour le modificateur
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