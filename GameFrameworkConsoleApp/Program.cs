using GameFrameworkConsoleApp.Services;
using Mandatory2DGameFramework.Domain.Logging.Infrastructure;
using System.Diagnostics;
namespace GameFrameworkConsoleApp
{
    /// <summary>
    /// Main entry point for the Game Framework Console Application.
    /// Demonstrates the framework's capabilities while following SOLID principles.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MyLogger.Instance.SetDefaultLevel(SourceLevels.All);

            // Dependency Injection - follows Dependency Inversion Principle
            IConsoleDisplayService displayService = new ConsoleDisplayService();
            IGameWorldSetupService worldSetupService = new GameWorldSetupService();
            IGameSimulationRunner gameRunner = new GameSimulationRunner(displayService, worldSetupService);

            // Run the game simulation
            gameRunner.Run();

            // Cleanup
            MyLogger.Instance.Stop();
        }
    }
}
