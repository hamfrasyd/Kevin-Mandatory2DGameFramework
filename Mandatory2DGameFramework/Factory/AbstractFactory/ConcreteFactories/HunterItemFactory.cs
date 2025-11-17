using Mandatory2DGameFramework.Core.Items;
using Mandatory2DGameFramework.Enums;
using Mandatory2DGameFramework.Interfaces;
using Mandatory2DGameFramework.Decorator.Interfaces;

namespace Mandatory2DGameFramework.Factory.AbstractFactory.ConcreteFactories
{
    /// <summary>
    /// Factory for creating Hunter-themed equipment.
    /// Creates medium Leather armor and ranged Gun weapons.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This factory produces a family of items suitable for Hunter class characters:
    /// <list type="bullet">
    /// <item><description><b>Weapon:</b> Gun (moderate damage, long range)</description></item>
    /// <item><description><b>Armor:</b> Leather material (medium defense values, balanced)</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Default values: Weapon damage 60, armor defense ranges from 5-15 depending on slot.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// IItemFactory factory = new HunterItemFactory();
    /// 
    /// // Create a full hunter equipment set
    /// IWeapon gun = factory.CreateWeapon("Hunting Rifle");
    /// IDefenceItem helmet = factory.CreateHelmet("Leather Cap");
    /// IDefenceItem chest = factory.CreateChestArmor("Leather Tunic");
    /// 
    /// // All armor is Leather type (consistent family)
    /// int totalDefense = helmet.Defense + chest.Defense; // 7 + 15 = 22
    /// </code>
    /// </example>
    /// <seealso cref="IItemFactory"/>
    /// <seealso cref="WarriorItemFactory"/>
    /// <seealso cref="MageItemFactory"/>
    public class HunterItemFactory : IItemFactory
    {
        /// <summary>
        /// Creates a Gun weapon for Hunters.
        /// </summary>
        /// <param name="name">The name of the weapon.</param>
        /// <param name="damage">The damage value. Default is 60.</param>
        /// <param name="range">The attack range. Default is 30.</param>
        /// <returns>A <see cref="IWeapon"/> implementing Gun weapon.</returns>
        public IWeapon CreateWeapon(string name, int damage = 60, int range = 30)
        {
            return new AttackItem(name, WeaponType.Gun, damage, range);
        }

        public IDefenceItem CreateHelmet(string name, int defense = 7)
        {
            return new DefenceItem(name, ArmorSlot.Head, ArmorType.Leather, defense);
        }

        public IDefenceItem CreateShoulderArmor(string name, int defense = 6)
        {
            return new DefenceItem(name, ArmorSlot.Shoulders, ArmorType.Leather, defense);
        }

        public IDefenceItem CreateChestArmor(string name, int defense = 15)
        {
            return new DefenceItem(name, ArmorSlot.Chest, ArmorType.Leather, defense);
        }

        public IDefenceItem CreateHandArmor(string name, int defense = 5)
        {
            return new DefenceItem(name, ArmorSlot.Hands, ArmorType.Leather, defense);
        }

        public IDefenceItem CreateLegArmor(string name, int defense = 10)
        {
            return new DefenceItem(name, ArmorSlot.Legs, ArmorType.Leather, defense);
        }

        public IDefenceItem CreateFeetArmor(string name, int defense = 6)
        {
            return new DefenceItem(name, ArmorSlot.Feet, ArmorType.Leather, defense);
        }
    }
}

