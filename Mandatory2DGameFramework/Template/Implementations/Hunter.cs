using Mandatory2DGameFramework.Template.Base;
using Mandatory2DGameFramework.Strategy.Implementations;

namespace Mandatory2DGameFramework.Template.Implementations
{
    /// <summary>
    /// Hunter creature class - ranged attacker with medium-high HP and medium armor.
    /// Uses default attack modifier implementation (no class-specific bonuses).
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Class Characteristics:</b>
    /// <list type="bullet">
    /// <item><description><b>HP:</b> 900 (between Warrior and Mage)</description></item>
    /// <item><description><b>Strategy:</b> Uses <see cref="RangedAttackStrategy"/> for long-range attacks</description></item>
    /// <item><description><b>Armor:</b> Leather material (medium defense, balanced)</description></item>
    /// <item><description><b>Weapon:</b> Gun (moderate damage, very long range)</description></item>
    /// <item><description><b>Special:</b> No class-specific modifiers, relies on strategy bonuses</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create hunter
    /// Hunter hunter = new Hunter("Robin");
    /// 
    /// // Equip medium gear
    /// IItemFactory factory = new HunterItemFactory();
    /// hunter.EquipWeapon(factory.CreateWeapon("Hunting Rifle"));
    /// hunter.EquippedArmor.Add(factory.CreateChestArmor("Leather Tunic"));
    /// 
    /// // Long range attack with strategy bonus
    /// hunter.PerformAttack(enemy, range: 30); // +50% from RangedAttackStrategy
    /// </code>
    /// </example>
    /// <seealso cref="Creature"/>
    /// <seealso cref="RangedAttackStrategy"/>
    public class Hunter : Creature
    {
        /// <summary>
        /// Initializes a new Hunter with 900 HP and ranged attack strategy.
        /// </summary>
        /// <param name="name">The name of the hunter.</param>
        public Hunter(string name) : base(name, maxHP: 900)
        {
            SetAttackStrategy(new RangedAttackStrategy());
        }
    }
}

