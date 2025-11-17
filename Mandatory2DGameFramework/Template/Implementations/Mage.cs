using Mandatory2DGameFramework.Template.Base;
using Mandatory2DGameFramework.Strategy.Implementations;

namespace Mandatory2DGameFramework.Template.Implementations
{
    /// <summary>
    /// Mage creature class - ranged caster with medium HP and light armor.
    /// Overrides <see cref="ApplyAttackModifiers"/> to add bonus damage at high HP.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Class Characteristics:</b>
    /// <list type="bullet">
    /// <item><description><b>HP:</b> 800 (medium, lower than Warrior)</description></item>
    /// <item><description><b>Strategy:</b> Uses <see cref="RangedAttackStrategy"/> for long-distance attacks</description></item>
    /// <item><description><b>Armor:</b> Cloth material (lowest defense, prioritizes mobility)</description></item>
    /// <item><description><b>Weapon:</b> Staff (moderate damage, medium-long range)</description></item>
    /// <item><description><b>Special:</b> Bonus +10 damage when HP is above 80 (focused casting)</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create mage
    /// Mage mage = new Mage("Alice");
    /// 
    /// // Equip light gear
    /// IItemFactory factory = new MageItemFactory();
    /// mage.EquipWeapon(factory.CreateWeapon("Arcane Staff"));
    /// mage.EquippedArmor.Add(factory.CreateChestArmor("Magic Robe"));
    /// 
    /// // Focused attack at high HP
    /// mage.PerformAttack(enemy, range: 25); // +10 bonus if HP > 80
    /// 
    /// // Normal attack after taking damage
    /// mage.TakeDamage(50); // HP now 750/800
    /// mage.PerformAttack(enemy, range: 25); // No bonus
    /// </code>
    /// </example>
    /// <seealso cref="Creature"/>
    /// <seealso cref="RangedAttackStrategy"/>
    public class Mage : Creature
    {
        /// <summary>
        /// Initializes a new Mage with 800 HP and ranged attack strategy.
        /// </summary>
        /// <param name="name">The name of the mage.</param>
        public Mage(string name) : base(name, maxHP: 800)
        {
            SetAttackStrategy(new RangedAttackStrategy());
        }

        /// <summary>
        /// Applies Mage-specific damage modifiers.
        /// </summary>
        /// <param name="baseDamage">The base damage from the attack strategy.</param>
        /// <returns>Base damage plus 10 if HP is above 80, otherwise unchanged.</returns>
        /// <remarks>
        /// Provides bonus damage when at high HP (above 80),
        /// representing focused magical power when uninjured.
        /// </remarks>
        protected override int ApplyAttackModifiers(int baseDamage)
        {
            if (HitPoint > 80)
            {
                return baseDamage + 10;
            }
            return baseDamage;
        }
    }
}
