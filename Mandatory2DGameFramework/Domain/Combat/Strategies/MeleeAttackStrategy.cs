using Mandatory2DGameFramework.Domain.Combat.Interfaces;
using Mandatory2DGameFramework.Domain.Creatures.Base;
using Mandatory2DGameFramework.Domain.Creatures.Classes;

namespace Mandatory2DGameFramework.Domain.Combat.Strategies
{
    /// <summary>
    /// Melee attack strategy that provides bonus damage when attacker's HP is low.
    /// Designed for close-range combat.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This strategy is designed for melee fighters and provides a "berserker" mechanic:
    /// <list type="bullet">
    /// <item><description><b>Normal:</b> Uses <see cref="IWeapon.GetDamage"/> or default base damage if unarmed</description></item>
    /// <item><description><b>Low HP (below 50%):</b> Applies 50% damage bonus</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Used by default by <see cref="Warrior"/> class.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Warrior with MeleeAttackStrategy
    /// Creature warrior = new Warrior("Bob", 1000);
    /// warrior.EquipWeapon(new AttackItem("Sword", WeaponType.Sword, 30, 1));
    /// 
    /// // Normal attack: 30 damage
    /// warrior.PerformAttack(enemy, range: 1);
    /// 
    /// // Take damage to go below 50% HP
    /// warrior.TakeDamage(600); // Now at 400/1000 HP
    /// 
    /// // Berserk attack: 45 damage (30 * 1.5)
    /// warrior.PerformAttack(enemy, range: 1);
    /// </code>
    /// </example>
    /// <seealso cref="IAttackStrategy"/>
    /// <seealso cref="RangedAttackStrategy"/>
    public class MeleeAttackStrategy : IAttackStrategy
    {
        /// <summary>
        /// Calculates melee attack damage with HP-based bonus.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="range">The distance to target (not used for melee calculations).</param>
        /// <returns>
        /// Base damage from <see cref="IWeapon.GetDamage"/> (or default base damage if unarmed), 
        /// multiplied by 1.5 if attacker's HP is below 50%.
        /// </returns>
        public int Attack(Creature attacker, int range)
        {
            int baseDamage = attacker.BaseAutoAttackDamage;
            
            if (attacker.EquippedWeapon is not null)
            {
                baseDamage = attacker.EquippedWeapon.GetDamage();
            }

            if (attacker.HitPoint < attacker.MaxHP / 2)
            {
                baseDamage = (int)(baseDamage * 1.5);
            }

            return baseDamage;
        }
    }
}

