using System;

public class GameOverManager : IObserver
{
    public void Update(ISubject subject)
    {
        Player player = (Player)subject;

        if (player.Health <= 0)
        {
            Console.WriteLine($"\n {player.Name} a été vaincu ");
            Console.WriteLine("--- GAME OVER ---");
            
            GameSettings.Instance.IsSimulationRunning = false;
        }
    }
}