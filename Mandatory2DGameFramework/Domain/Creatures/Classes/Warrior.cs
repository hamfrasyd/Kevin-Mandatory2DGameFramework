using Mandatory2DGameFramework.Domain.Combat.Strategies;
using Mandatory2DGameFramework.Domain.Creatures.Base;

namespace Mandatory2DGameFramework.Domain.Creatures.Classes
{
    /// <summary>
    /// Warrior creature class - melee fighter with high HP and heavy armor.
    /// Overrides <see cref="ApplyAttackModifiers"/> to add bonus damage at low HP.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Class Characteristics:</b>
    /// <list type="bullet">
    /// <item><description><b>HP:</b> 1000 (highest of all classes)</description></item>
    /// <item><description><b>Strategy:</b> Uses <see cref="MeleeAttackStrategy"/> for close combat</description></item>
    /// <item><description><b>Armor:</b> Plate material (highest defense)</description></item>
    /// <item><description><b>Weapon:</b> Sword (high melee damage)</description></item>
    /// <item><description><b>Special:</b> Bonus +10 damage when HP drops below 50 (critical strikes)</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create warrior
    /// Warrior warrior = new Warrior("Bob");
    /// 
    /// // Equip heavy gear
    /// IItemFactory factory = new WarriorItemFactory();
    /// warrior.EquipWeapon(factory.CreateWeapon("Battle Sword"));
    /// warrior.EquippedArmor.Add(factory.CreateChestArmor("Plate Mail"));
    /// 
    /// // Normal attack
    /// warrior.PerformAttack(enemy, range: 1); // Uses base damage
    /// 
    /// // Critical low HP attack
    /// warrior.TakeDamage(600); // HP now 400/1000 (below 50)
    /// warrior.PerformAttack(enemy, range: 1); // +10 bonus from low HP!
    /// </code>
    /// </example>
    /// <seealso cref="Creature"/>
    /// <seealso cref="MeleeAttackStrategy"/>
    public class Warrior : Creature
    {
        /// <summary>
        /// Initializes a new Warrior with 1000 HP and melee attack strategy.
        /// </summary>
        /// <param name="name">The name of the warrior.</param>
        public Warrior(string name) : base(name, maxHP: 1000)
        {
            SetAttackStrategy(new MeleeAttackStrategy());
        }

        /// <summary>
        /// Applies Warrior-specific damage modifiers.
        /// </summary>
        /// <param name="baseDamage">The base damage from the attack strategy.</param>
        /// <returns>Base damage plus 10 if HP is below 50, otherwise unchanged.</returns>
        /// <remarks>
        /// Provides bonus damage when in critical condition (below 50 HP),
        /// representing desperate strength or berserker fury.
        /// </remarks>
        protected override int ApplyAttackModifiers(int baseDamage)
        {
            if (HitPoint < 50)
            {
                return baseDamage + 10;
            }
            return baseDamage;
        }
    }
}
