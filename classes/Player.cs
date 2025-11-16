using System;
using System.Collections.Generic;

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

    public int Health
    {
        get { return _health; }
        private set
        {
            _health = Math.Max(0, value);
        }
    }

    // action qui déclenche la notification
    public void TakeDamage(int damage)
    {
        // On utilise le Singleton (Étape 1) pour le modificateur
        int actualDamage = (int)(damage * GameSettings.Instance.DamageModifier);
        this.Health -= actualDamage;
        
        Console.WriteLine($"\t{Name} subit {actualDamage} points de dégâts ! PV restants : {Health}");
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