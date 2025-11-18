using Mandatory2DGameFramework.Domain.Creatures.Base;

namespace Mandatory2DGameFramework.Domain.Combat.Interfaces
{
    /// <summary>
    /// Interface for attack damage calculations.
    /// Enables different attack algorithms that can be swapped at runtime.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Different strategies provide different combat mechanics:
    /// <list type="bullet">
    /// <item><description><b>MeleeAttackStrategy:</b> Bonus damage when HP &lt; 50%</description></item>
    /// <item><description><b>RangedAttackStrategy:</b> Bonus damage when range ≥ 20</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Strategies are set via <see cref="Creature.SetAttackStrategy"/> and used in <see cref="Creature.PerformAttack"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create creature with strategy
    /// Creature warrior = new Warrior("Bob", 1000);
    /// // Warrior uses MeleeAttackStrategy by default
    /// 
    /// // Change strategy at runtime
    /// warrior.SetAttackStrategy(new RangedAttackStrategy());
    /// 
    /// // Attack uses the current strategy
    /// warrior.PerformAttack(enemy, range: 25);
    /// </code>
    /// </example>
    /// <seealso cref="MeleeAttackStrategy"/>
    /// <seealso cref="RangedAttackStrategy"/>
    /// <seealso cref="Creature"/>
    public interface IAttackStrategy
    {
        /// <summary>
        /// Calculates attack damage based on attacker state and range.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="range">The distance to the target.</param>
        /// <returns>The calculated base damage before additional modifiers.</returns>
        int Attack(Creature attacker, int range);
    }
}

