namespace GameFrameworkConsoleApp.Services
{
    /// <summary>
    /// Service responsible for displaying game information to the console.
    /// Follows Single Responsibility Principle - only handles console display logic.
    /// </summary>
    public class ConsoleDisplayService : IConsoleDisplayService
    {
        public void DisplayHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"--- {title} ---");
        }

        public void DisplaySeparator()
        {
            Console.WriteLine();
        }

        public void DisplayCreatureInfo(Creature creature)
        {
            string weaponInfo = creature.EquippedWeapon?.Name ?? "Unarmed";
            int totalDefense = creature.EquippedArmor.GetTotalValue();
            
            Console.WriteLine($"{creature.Name} ({creature.GetType().Name}): HP {creature.HitPoint}/{creature.MaxHP} | Weapon: {weaponInfo} | Defense: {totalDefense}");
        }

        public void DisplayCombatResult(string attacker, string target, int damage)
        {
            Console.WriteLine($"{attacker} attacks {target} for {damage} damage");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: {error}");
            Console.ResetColor();
        }
    }
}

