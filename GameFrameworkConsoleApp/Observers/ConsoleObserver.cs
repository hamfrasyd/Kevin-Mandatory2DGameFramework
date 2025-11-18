using Mandatory2DGameFramework.Domain.Logging.Interfaces;

namespace GameFrameworkConsoleApp.Observers
{
    /// <summary>
    /// Observer that displays creature events to the console.
    /// Demonstrates Open/Closed Principle - extends framework without modifying it.
    /// Implements ICreatureObserver interface from framework.
    /// </summary>
    public class ConsoleObserver : ICreatureObserver
    {
        public void OnDamageDone(Creature attacker, string action, IAttackable target, int damage)
        {
            string targetName = target is Creature c ? c.Name : target.GetType().Name;
            Console.WriteLine($"{attacker.Name} attacks {targetName} for {damage} damage");
        }

        public void OnCreatureHit(Creature creature, string action, int damageTaken, int damageMitigated)
        {
            if (damageMitigated > 0)
            {
                Console.WriteLine($"{creature.Name} takes {damageTaken} damage (blocked {damageMitigated})");
            }
            else
            {
                Console.WriteLine($"{creature.Name} takes {damageTaken} damage");
            }
        }

        public void OnCreatureDied(Creature creature)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{creature.Name} has died!");
            Console.ResetColor();
        }
    }
}

