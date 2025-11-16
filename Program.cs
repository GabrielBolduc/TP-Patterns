using System;
using System.Threading; 

// Patterns utiliser : Obeserver + decorator 

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            RunSimulation();

            Console.WriteLine("\nSimulation terminée. Appuyez sur [Espace] pour rejouer, ou [Échap] pour quitter.");

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Spacebar && key != ConsoleKey.Escape);

            if (key == ConsoleKey.Escape)
            {
                break; 
            }
        }
    }

    public static void RunSimulation()
    {

        GameSettings.Instance.IsSimulationRunning = true;

        Random rng = GameSettings.Instance.Rng;

        int durationMs = rng.Next(
            GameSettings.Instance.MinSimulationDuration,
            GameSettings.Instance.MaxSimulationDuration
        );

        var simTimer = new System.Timers.Timer(durationMs);

        simTimer.Elapsed += (sender, e) =>
        {
            Console.WriteLine("\n--- TEMPS ÉCOULÉ (MATCH NUL) ---");
            GameSettings.Instance.IsSimulationRunning = false;
        };
        simTimer.AutoReset = false; 
        simTimer.Start();

        Console.Clear();
        Console.WriteLine($"--- NOUVELLE SIMULATION  ---");

        Player player1 = new Player("Yvan, le Barbare");
        Player player2 = new Player("Cedrick, le chevalier");

        GameOverManager gameOverManager = new GameOverManager();

        player1.Attach(gameOverManager);
        // ET les notifications de player2
        player2.Attach(gameOverManager);



        bool isPlayer1Turn = true;


        while (GameSettings.Instance.IsSimulationRunning)
        {
            if (isPlayer1Turn)
            {
                Console.WriteLine($"\nTour de {player1.Name} :");
    
                IWeapon weapon = WeaponFactory.CreateRandomWeapon();

                Console.WriteLine($"\t{player1.Name} attaque avec: {weapon.GetDescription()}");

               
                player2.TakeDamage(weapon.GetBaseDamage());
            }
            else
            {
                Console.WriteLine($"\nTour de {player2.Name} :");
                IWeapon weapon = WeaponFactory.CreateRandomWeapon();

                Console.WriteLine($"\t{player2.Name} attaque avec: {weapon.GetDescription()}");

                player1.TakeDamage(weapon.GetBaseDamage());
            }

            isPlayer1Turn = !isPlayer1Turn;

            Thread.Sleep(GameSettings.Instance.TempsAttenteEntreTours);
        }

        simTimer.Stop();
    }
}