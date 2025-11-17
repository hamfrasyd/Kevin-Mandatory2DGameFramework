using Mandatory2DGameFramework.Core.Items;
using Mandatory2DGameFramework.Enums;
using Mandatory2DGameFramework.Interfaces;
using Mandatory2DGameFramework.Decorator.Interfaces;

namespace Mandatory2DGameFramework.Factory.AbstractFactory.ConcreteFactories
{
    /// <summary>
    /// Factory for creating Warrior-themed equipment.
    /// Creates heavy Plate armor and melee Sword weapons.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This factory produces a family of items suitable for Warrior class characters:
    /// <list type="bullet">
    /// <item><description><b>Weapon:</b> Sword (high damage, melee range)</description></item>
    /// <item><description><b>Armor:</b> Plate material (highest defense values)</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Default values: Weapon damage 90, armor defense ranges from 6-20 depending on slot.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// IItemFactory factory = new WarriorItemFactory();
    /// 
    /// // Create a full warrior equipment set
    /// IWeapon sword = factory.CreateWeapon("Battle Sword");
    /// IDefenceItem helmet = factory.CreateHelmet("Plate Helmet");
    /// IDefenceItem chest = factory.CreateChestArmor("Plate Mail");
    /// 
    /// // All armor is Plate type (consistent family)
    /// int totalDefense = helmet.Defense + chest.Defense; // 10 + 20 = 30
    /// </code>
    /// </example>
    /// <seealso cref="IItemFactory"/>
    /// <seealso cref="MageItemFactory"/>
    /// <seealso cref="HunterItemFactory"/>
    public class WarriorItemFactory : IItemFactory
    {
        /// <summary>
        /// Creates a Sword weapon for Warriors.
        /// </summary>
        /// <param name="name">The name of the weapon.</param>
        /// <param name="damage">The damage value. Default is 90.</param>
        /// <param name="range">The attack range. Default is 2.</param>
        /// <returns>A <see cref="IWeapon"/> implementing Sword weapon.</returns>
        public IWeapon CreateWeapon(string name, int damage = 90, int range = 2)
        {
            return new AttackItem(name, WeaponType.Sword, damage, range);
        }

        public IDefenceItem CreateHelmet(string name, int defense = 10)
        {
            return new DefenceItem(name, ArmorSlot.Head, ArmorType.Plate, defense);
        }

        public IDefenceItem CreateShoulderArmor(string name, int defense = 8)
        {
            return new DefenceItem(name, ArmorSlot.Shoulders, ArmorType.Plate, defense);
        }

        public IDefenceItem CreateChestArmor(string name, int defense = 20)
        {
            return new DefenceItem(name, ArmorSlot.Chest, ArmorType.Plate, defense);
        }

        public IDefenceItem CreateHandArmor(string name, int defense = 6)
        {
            return new DefenceItem(name, ArmorSlot.Hands, ArmorType.Plate, defense);
        }

        public IDefenceItem CreateLegArmor(string name, int defense = 12)
        {
            return new DefenceItem(name, ArmorSlot.Legs, ArmorType.Plate, defense);
        }

        public IDefenceItem CreateFeetArmor(string name, int defense = 7)
        {
            return new DefenceItem(name, ArmorSlot.Feet, ArmorType.Plate, defense);
        }
    }
}

