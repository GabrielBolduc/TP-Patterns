using System;

public sealed class GameSettings
{
    private static readonly Lazy<GameSettings> _lazyInstance = new Lazy<GameSettings>(() => new GameSettings());

    
    public static GameSettings Instance => _lazyInstance.Value;

    private GameSettings()
    {
        Rng = new Random();
        Console.WriteLine("--- Instance GameSettings créée (Mode Lazy) ---");
    }

    public Random Rng { get; private set; }

    public bool IsSimulationRunning { get; set; }

    public int TempsAttenteEntreTours => 700;

    public float DamageModifier => 1.1f; 

    public int BaseHealth => 200;
    public int MinSimulationDuration => 20000;
    public int MaxSimulationDuration => 40001; 
}