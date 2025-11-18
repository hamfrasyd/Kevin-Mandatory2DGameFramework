namespace GameFrameworkConsoleApp.Services
{
    /// <summary>
    /// Interface for displaying game information to the console.
    /// Follows Interface Segregation Principle - clients only depend on what they need.
    /// </summary>
    public interface IConsoleDisplayService
    {
        void DisplayHeader(string title);
        void DisplaySeparator();
        void DisplayCreatureInfo(Creature creature);
        void DisplayCombatResult(string attacker, string target, int damage);
        void DisplayMessage(string message);
        void DisplayError(string error);
    }
}

