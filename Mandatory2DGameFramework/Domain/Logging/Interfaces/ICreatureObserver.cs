using Mandatory2DGameFramework.Domain.Creatures.Base;
using Mandatory2DGameFramework.Domain.Creatures.Interfaces;
using Mandatory2DGameFramework.Domain.Logging.Observers;

namespace Mandatory2DGameFramework.Domain.Logging.Interfaces
{
    /// <summary>
    /// Interface for receiving notifications about creature events.
    /// Enables loose coupling between creatures and event handlers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Observers are notified when creatures take damage or die.
    /// Multiple observers can be attached to a single creature, enabling features like:
    /// <list type="bullet">
    /// <item><description>Combat logging</description></item>
    /// <item><description>Statistics tracking</description></item>
    /// <item><description>UI updates</description></item>
    /// <item><description>Achievement systems</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create an observer (singleton, auto-starts combat logging)
    /// ICreatureObserver logger = CombatLogger.Instance;
    /// 
    /// // Attach to creature
    /// Creature warrior = new Warrior("Bob", 1000);
    /// warrior.Attach(logger);
    /// 
    /// // Events trigger notifications
    /// warrior.TakeDamage(100); // Triggers OnCreatureHit
    /// warrior.TakeDamage(1000); // Triggers OnCreatureDied
    /// </code>
    /// </example>
    /// <seealso cref="Creature"/>
    /// <seealso cref="CombatLogger"/>
    public interface ICreatureObserver
    {
        /// <summary>
        /// Called when a creature performs an attack (damage done).
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="action">The action performed (e.g., "Attack", "Defend").</param>
        /// <param name="target">The target being attacked.</param>
        /// <param name="damage">The amount of damage dealt.</param>
        void OnDamageDone(Creature attacker, string action, IAttackable target, int damage);
        
        /// <summary>
        /// Called when a creature takes damage.
        /// </summary>
        /// <param name="creature">The creature that was hit.</param>
        /// <param name="action">The action that caused damage (e.g., "Attack", "Defend").</param>
        /// <param name="damageTaken">The amount of damage taken (after defense reduction).</param>
        /// <param name="damageMitigated">The amount of damage mitigated by armor.</param>
        void OnCreatureHit(Creature creature, string action, int damageTaken, int damageMitigated);
        
        /// <summary>
        /// Called when a creature's hit points reach zero or below.
        /// </summary>
        /// <param name="creature">The creature that died.</param>
        void OnCreatureDied(Creature creature);
    }
}

