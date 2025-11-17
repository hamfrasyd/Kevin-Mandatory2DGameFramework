using Mandatory2DGameFramework.Template.Base;
namespace Mandatory2DGameFramework.Interfaces
{
    /// <summary>
    /// Interface for objects that can be attacked and take damage.
    /// Implemented by creatures and other damageable entities in the game world.
    /// </summary>
    /// <remarks>
    /// This interface enables polymorphic targeting in combat. Any object implementing
    /// IAttackable can be the target of an attack, allowing for flexible combat mechanics.
    /// </remarks>
    /// <example>
    /// <code>
    /// IAttackable target = new Warrior("Enemy", 1000);
    /// warrior.PerformAttack(target, range: 5);
    /// 
    /// if (!target.IsAlive)
    /// {
    ///     Console.WriteLine("Target defeated!");
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="Creature"/>
    public interface IAttackable
    {
        /// <summary>
        /// Takes damage from an attack. Defense values are applied internally.
        /// </summary>
        /// <param name="damage">The amount of incoming damage before defense reduction.</param>
        /// <remarks>
        /// The actual damage taken may be less than the incoming damage due to armor and defense modifiers.
        /// If damage reduces HP to 0 or below, the object dies.
        /// </remarks>
        void TakeDamage(int damage);
        
        /// <summary>
        /// Gets a value indicating whether this object is still alive.
        /// </summary>
        /// <value><c>true</c> if alive (HP > 0); otherwise, <c>false</c>.</value>
        bool IsAlive { get; }
    }
}

