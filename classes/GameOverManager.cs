using System;

// Le Manager IMPLÉMENTE IObserver.
// Il va surveiller les joueurs.
public class GameOverManager : IObserver
{
    // C'est la méthode que le Joueur va appeler
    public void Update(ISubject subject)
    {
        // 1. On "cast" le sujet pour récupérer l'objet Joueur
        Player player = (Player)subject;

        // 2. On vérifie son état (la raison de la notification)
        if (player.Health <= 0)
        {
            // 3. On agit !
            Console.WriteLine($"\n!!! {player.Name} a été vaincu ! !!!");
            Console.WriteLine("--- GAME OVER ---");
            
            // On utilise le Singleton (Étape 1) pour arrêter la boucle de jeu
            GameSettings.Instance.IsSimulationRunning = false;
        }
    }
}