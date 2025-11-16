using System;

// 'public sealed class' : 'sealed' (scellé) empêche l'héritage,
// ce qui est une bonne pratique pour un Singleton.
public sealed class GameSettings
{
    // 1. L'implémentation du "mode lazy" (paresseux)
    // On dit à C# "Voici comment créer un GameSettings,
    // mais ne le fais pas tant qu'on ne te le demande pas."
    // 'Lazy<T>' gère le "thread-safety" pour nous.
    private static readonly Lazy<GameSettings> _lazyInstance =
        new Lazy<GameSettings>(() => new GameSettings());

    // 2. L'accesseur global (public)
    // C'est le seul point d'accès public. La première fois qu'on
    // appellera 'GameSettings.Instance', le '_lazyInstance' va 
    // s'exécuter et créer l'objet. Les appels suivants
    // retourneront l'objet déjà créé.
    public static GameSettings Instance => _lazyInstance.Value;

    // 3. Le constructeur PRIVE
    // C'est la clé du Singleton. Personne à l'extérieur
    // ne peut faire 'new GameSettings()'.
    private GameSettings()
    {
        // On initialise notre générateur aléatoire ici
        // pour qu'il soit partagé par toute l'application.
        // C'est crucial pour éviter d'avoir les mêmes
        // nombres aléatoires si on l'appelle trop vite.
        Rng = new Random();
        Console.WriteLine("--- Instance GameSettings créée (Mode Lazy) ---");
    }

    // --- PROPRIÉTÉS GLOBALES ---

    // Le générateur aléatoire partagé (Random Number Generator)
    public Random Rng { get; private set; }

    // Le 'flag' qui contrôle la boucle de jeu principale
    // 'volatile' peut être utile dans un contexte multi-thread,
    // mais pour une app console simple, 'bool' est suffisant.
    public bool IsSimulationRunning { get; set; }

    // --- PARAMÈTRES DE SIMULATION (CRÉATIVITÉ) ---

    // Temps d'attente entre les attaques (en millisecondes)
    public int TempsAttenteEntreTours => 700; // 0.7 secondes

    // Modificateur global de dégâts (Yvan veut de la créativité !)
    public float DamageModifier => 1.1f; // +10% de dégâts sur tout

    // Vie de base des joueurs
    public int BaseHealth => 200;

    // Durée de la simulation (20 à 40 secondes)
    // .Next(min, max) : 'max' est exclusif, donc on met 40001
    public int MinSimulationDuration => 20000;
    public int MaxSimulationDuration => 40001; 
}