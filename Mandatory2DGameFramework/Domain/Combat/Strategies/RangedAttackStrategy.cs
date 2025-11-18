using Mandatory2DGameFramework.Domain.Combat.Interfaces;
using Mandatory2DGameFramework.Domain.Creatures.Base;
using Mandatory2DGameFramework.Domain.Creatures.Classes;

namespace Mandatory2DGameFramework.Domain.Combat.Strategies
{
    /// <summary>
    /// Ranged attack strategy that provides bonus damage at long distances.
    /// Designed for long-range combat.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This strategy is designed for ranged attackers and rewards positioning:
    /// <list type="bullet">
    /// <item><description><b>Short/Medium range (0-19):</b> Uses <see cref="IWeapon.GetDamage"/> or default base damage if unarmed</description></item>
    /// <item><description><b>Long range (20+):</b> Applies 50% damage bonus for optimal positioning</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Used by default by <see cref="Mage"/> 
    /// and <see cref="Hunter"/> classes.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Mage with RangedAttackStrategy
    /// Creature mage = new Mage("Alice", 800);
    /// mage.EquipWeapon(new AttackItem("Staff", WeaponType.Staff, 25, 2));
    /// 
    /// // Short range attack: 25 damage
    /// mage.PerformAttack(enemy, range: 10);
    /// 
    /// // Long range attack: 37 damage (25 * 1.5, rounded down)
    /// mage.PerformAttack(enemy, range: 25);
    /// </code>
    /// </example>
    /// <seealso cref="IAttackStrategy"/>
    /// <seealso cref="MeleeAttackStrategy"/>
    public class RangedAttackStrategy : IAttackStrategy
    {
        /// <summary>
        /// Calculates ranged attack damage with range-based bonus.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="range">The distance to target. Bonus applied if range ≥ 20.</param>
        /// <returns>
        /// Base damage from <see cref="IWeapon.GetDamage"/> (or default base damage if unarmed), 
        /// multiplied by 1.5 if range is 20 or more.
        /// </returns>
        public int Attack(Creature attacker, int range)
        {
            int baseDamage = attacker.BaseAutoAttackDamage;
            
            if (attacker.EquippedWeapon is not null)
            {
                baseDamage = attacker.EquippedWeapon.GetDamage();
            }

            if (range >= 20)
            {
                baseDamage = (int)(baseDamage * 1.5);
            }

            return baseDamage;
        }
    }
}

