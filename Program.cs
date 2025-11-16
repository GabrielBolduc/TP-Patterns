using System;
using System.Threading; // Requis pour Thread.Sleep

public class Program
{
    public static void Main(string[] args)
    {
        // Boucle "Rejouer" (Critère d'Yvan)
        while (true)
        {
            RunSimulation();

            Console.WriteLine("\nSimulation terminée. Appuyez sur [Espace] pour rejouer, ou [Échap] pour quitter.");
            
            ConsoleKey key;
            do {
                // 'true' empêche la touche pressée de s'afficher dans la console
                key = Console.ReadKey(true).Key; 
            } while (key != ConsoleKey.Spacebar && key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                break; // Sort de la boucle while(true) et termine le programme
            }
            // Si 'Espace' est pressé, la boucle recommence
        }
    }

    /// <summary>
    /// Gère une seule simulation de combat, du début à la fin.
    /// </summary>
    public static void RunSimulation()
    {
        // --- 1. INITIALISATION (Singleton - Étape 1) ---
        
        // On accède au Singleton pour définir l'état de la simulation
        GameSettings.Instance.IsSimulationRunning = true;
        
        // On récupère le générateur aléatoire partagé (Rng)
        Random rng = GameSettings.Instance.Rng;

        // Définir la durée aléatoire (20-40s) via le Singleton
        int durationMs = rng.Next(
            GameSettings.Instance.MinSimulationDuration,
            GameSettings.Instance.MaxSimulationDuration
        );
        
        // Création d'un timer pour gérer le "Match Nul"
        var simTimer = new System.Timers.Timer(durationMs);
        
        // On s'abonne à l'événement 'Elapsed' du timer
        simTimer.Elapsed += (sender, e) => {
            Console.WriteLine("\n--- TEMPS ÉCOULÉ (MATCH NUL) ---");
            // On utilise le Singleton pour arrêter le combat
            GameSettings.Instance.IsSimulationRunning = false; 
        };
        simTimer.AutoReset = false; // Ne se déclenche qu'une fois
        simTimer.Start();

        // Nettoie la console pour une nouvelle simulation
        Console.Clear();
        Console.WriteLine($"--- NOUVELLE SIMULATION (Durée max: {durationMs / 1000}s) ---");

        // --- 2. SETUP (Observer - Étape 2) ---
        
        // Créer les Sujets (les objets qui seront observés)
        Player player1 = new Player("Yvan, le Barbare");
        Player player2 = new Player("Cedrick, le chevalier");

        // Créer l'Observateur (l'objet qui surveille)
        GameOverManager gameOverManager = new GameOverManager();

        // Connecter l'Observateur aux Sujets
        // Le GameOverManager écoute maintenant les notifications de player1
        player1.Attach(gameOverManager); 
        // ET les notifications de player2
        player2.Attach(gameOverManager); 
        

        // --- 3. BOUCLE DE COMBAT (Decorator - Étape 3) ---
        
        bool isPlayer1Turn = true;
        
        // La boucle tourne TANT QUE le Singleton dit "true"
        // (Elle sera arrêtée soit par le Timer, soit par le GameOverManager)
        while (GameSettings.Instance.IsSimulationRunning)
        {
            if (isPlayer1Turn)
            {
                Console.WriteLine($"\nTour de {player1.Name} :");
                // On fait appel à notre "Usine" pour créer une arme (ta suggestion !)
                IWeapon weapon = WeaponFactory.CreateRandomWeapon();
                
                Console.WriteLine($"\t{player1.Name} attaque avec: {weapon.GetDescription()}");
                
                // Le joueur 2 subit des dégâts
                // (Si sa vie tombe à 0, il appellera Notify() et le GameOverManager réagira)
                player2.TakeDamage(weapon.GetBaseDamage());
            }
            else
            {
                Console.WriteLine($"\nTour de {player2.Name} :");
                // On crée une autre arme aléatoire
                IWeapon weapon = WeaponFactory.CreateRandomWeapon();
                
                Console.WriteLine($"\t{player2.Name} attaque avec: {weapon.GetDescription()}");
                
                player1.TakeDamage(weapon.GetBaseDamage());
            }

            // Changer de tour
            isPlayer1Turn = !isPlayer1Turn;
            
            // Faire une pause (valeur lue depuis le Singleton)
            Thread.Sleep(GameSettings.Instance.TempsAttenteEntreTours);
        }

        // --- 4. NETTOYAGE ---
        
        // Arrêter le timer au cas où le jeu s'est terminé par un KO
        // (pour éviter qu'il ne se déclenche après la fin du combat)
        simTimer.Stop();
    }
}