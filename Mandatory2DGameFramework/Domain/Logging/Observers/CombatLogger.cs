using Mandatory2DGameFramework.Domain.Creatures.Base;
using Mandatory2DGameFramework.Domain.Creatures.Interfaces;
using Mandatory2DGameFramework.Domain.Logging.Infrastructure;
using Mandatory2DGameFramework.Domain.Logging.Interfaces;

namespace Mandatory2DGameFramework.Domain.Logging.Observers
{
    /// <summary>
    /// Observer that logs combat events to a file.
    /// Writes detailed combat information using the logger system.
    /// Singleton pattern - automatically starts combat logging on first access.
    /// </summary>
    /// <remarks>
    /// <para>
    /// CombatLogger observes creature events and writes detailed combat information to Combatlog.txt
    /// using the <see cref="MyLogger"/> singleton. This enables:
    /// <list type="bullet">
    /// <item><description>Combat history tracking</description></item>
    /// <item><description>Debugging and analysis</description></item>
    /// <item><description>Event auditing</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Logs include creature name, damage amounts, and HP status.
    /// </para>
    /// <para>
    /// The logger automatically initializes combat logging when first accessed.
    /// Uses singleton pattern to ensure only one instance exists.
    /// Writes only to Combatlog.txt, separate from application logs.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create and attach logger (combat logging starts automatically)
    /// CombatLogger logger = CombatLogger.Instance;
    /// Creature warrior = new Warrior("Bob", 1000);
    /// Creature mage = new Mage("Alice", 800);
    /// warrior.Attach(logger);
    /// mage.Attach(logger);
    /// 
    /// // Attack triggers both damage done and damage taken logs
    /// warrior.PerformAttack(mage, range: 1);
    /// // Log output:
    /// // [Damage Done] Bob-CreatureId: 1, Action: Attack, Target: Alice, Damage: 30
    /// // [Damage Taken] Alice-CreatureId: 2, Action: Attack, Damage: 25 (Damage mitigated: 5)
    /// 
    /// mage.TakeDamage(1000);
    /// // Log output: [Death] Alice-CreatureId: 2 has died!
    /// </code>
    /// </example>
    /// <seealso cref="ICreatureObserver"/>
    /// <seealso cref="MyLogger"/>
    public class CombatLogger : ICreatureObserver
    {
        private static CombatLogger? _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets the singleton instance of CombatLogger.
        /// Automatically starts combat logging on first access.
        /// </summary>
        public static CombatLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CombatLogger();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor to enforce singleton pattern.
        /// Automatically starts combat logging.
        /// </summary>
        private CombatLogger()
        {
            MyLogger.Instance.StartCombatLog();
        }
        /// <summary>
        /// Logs when a creature performs an attack (damage done).
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="action">The action performed.</param>
        /// <param name="target">The target being attacked.</param>
        /// <param name="damage">The amount of damage dealt.</param>
        public void OnDamageDone(Creature attacker, string action, IAttackable target, int damage)
        {
            string attackerName = string.IsNullOrEmpty(attacker.Name) ? "Unknown" : attacker.Name;
            string targetName = target is Creature c ? (string.IsNullOrEmpty(c.Name) ? "Unknown" : c.Name) : target.GetType().Name;
            string safeAction = string.IsNullOrEmpty(action) ? "Unknown" : action;

            MyLogger.Instance.LogCombatInfo($"[Damage Done] {attackerName}-CreatureId: {attacker.Id}, Action: {safeAction}, Target: {targetName}, Damage: {damage}");
        }

        /// <summary>
        /// Logs when a creature is hit, including damage amount and mitigation.
        /// </summary>
        /// <param name="creature">The creature that was hit.</param>
        /// <param name="action">The action that caused damage.</param>
        /// <param name="damageTaken">The amount of damage taken.</param>
        /// <param name="damageMitigated">The amount of damage mitigated.</param>
        public void OnCreatureHit(Creature creature, string action, int damageTaken, int damageMitigated)
        {
            string creatureName = string.IsNullOrEmpty(creature.Name) ? "Unknown" : creature.Name;
            string safeAction = string.IsNullOrEmpty(action) ? "Unknown" : action;
            
            MyLogger.Instance.LogCombatInfo($"[Damage Taken] {creatureName}-CreatureId: {creature.Id}, Action: {safeAction}, Damage: {damageTaken} (Damage mitigated: {damageMitigated})");
        }

        /// <summary>
        /// Logs when a creature dies.
        /// </summary>
        /// <param name="creature">The creature that died.</param>
        public void OnCreatureDied(Creature creature)
        {
            string creatureName = string.IsNullOrEmpty(creature.Name) ? "Unknown" : creature.Name;
            
            MyLogger.Instance.LogCombatInfo($"[Death] {creatureName}-CreatureId: {creature.Id} has died!");
        }
    }
}

