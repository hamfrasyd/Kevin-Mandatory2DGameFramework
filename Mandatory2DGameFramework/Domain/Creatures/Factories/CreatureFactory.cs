using Mandatory2DGameFramework.Domain.Creatures.Base;
using Mandatory2DGameFramework.Domain.Creatures.Classes;
using Mandatory2DGameFramework.Domain.Enums;
using Mandatory2DGameFramework.Domain.Equipment.Factories;

namespace Mandatory2DGameFramework.Domain.Creatures.Factories
{
    /// <summary>
    /// Factory for creating creatures with appropriate equipment.
    /// Provides a centralized way to instantiate creatures.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Uses a switch-based implementation to create different creature types.
    /// Each creature type is created with class-appropriate starting equipment using
    /// item factories (WarriorItemFactory, MageItemFactory, HunterItemFactory).
    /// </para>
    /// <para>
    /// Starting attributes by class:
    /// <list type="table">
    /// <listheader>
    /// <term>Class</term>
    /// <description>HP / Weapon / Armor Type</description>
    /// </listheader>
    /// <item>
    /// <term>Warrior</term>
    /// <description>1000 HP / Sword / Plate armor</description>
    /// </item>
    /// <item>
    /// <term>Mage</term>
    /// <description>800 HP / Staff / Cloth armor</description>
    /// </item>
    /// <item>
    /// <term>Hunter</term>
    /// <description>900 HP / Gun / Leather armor</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create a warrior
    /// Creature warrior = CreatureFactory.CreateCreature(ClassType.Warrior, "Bob");
    /// // Bob is created with 1000 HP and a Sword weapon
    /// 
    /// // Create a mage
    /// Creature mage = CreatureFactory.CreateCreature(ClassType.Mage, "Alice");
    /// // Alice is created with 800 HP and a Staff weapon
    /// 
    /// // Combat
    /// warrior.PerformAttack(mage, range: 1);
    /// </code>
    /// </example>
    /// <seealso cref="ClassType"/>
    /// <seealso cref="Warrior"/>
    /// <seealso cref="Mage"/>
    /// <seealso cref="Hunter"/>
    public abstract class CreatureFactory
    {
        /// <summary>
        /// Creates a creature of the specified type with default equipment.
        /// </summary>
        /// <param name="type">The type of creature to create.</param>
        /// <param name="name">The name for the creature.</param>
        /// <returns>
        /// A fully initialized <see cref="Creature"/> instance with:
        /// <list type="bullet">
        /// <item><description>Class-appropriate HP value</description></item>
        /// <item><description>Default <see cref="IWeapon"/> from corresponding item factory</description></item>
        /// <item><description>Attack strategy matching the class type</description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Uses item factories to create appropriate equipment for each class.
        /// If an unknown type is provided, defaults to creating a Warrior.
        /// </remarks>
        public static Creature CreateCreature(ClassType type, string name)
        {
            // Ensure name is not null or empty
            string safeName = string.IsNullOrEmpty(name) ? "Unknown" : name;

            switch (type)
            {
                case ClassType.Warrior:
                    var warrior = new Warrior(name);
                    var sword = new WarriorItemFactory().CreateWeapon(name: "Great Sword");
                    warrior.EquipWeapon(sword);
                    return warrior;

                case ClassType.Mage:
                    var mage = new Mage(name);
                    var staff = new MageItemFactory().CreateWeapon(name: "Staff of Magic");
                    mage.EquipWeapon(staff);
                    return mage;

                case ClassType.Hunter:
                    var hunter = new Hunter(name);
                    var gun = new HunterItemFactory().CreateWeapon(name: "Hunting Rifle");
                    hunter.EquipWeapon(gun);
                    return hunter;

                default:
                    // Default to warrior
                    return CreateCreature(ClassType.Warrior, safeName);
            }
        }
    }
}

