namespace GameFrameworkConsoleApp.Services
{
    /// <summary>
    /// Interface for running the game simulation.
    /// Follows Dependency Inversion Principle.
    /// </summary>
    public interface IGameSimulationRunner
    {
        void Run();
    }
}

