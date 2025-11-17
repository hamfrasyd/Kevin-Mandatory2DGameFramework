using Mandatory2DGameFramework.Core.Items;
using Mandatory2DGameFramework.Enums;
using Mandatory2DGameFramework.Interfaces;
using Mandatory2DGameFramework.Decorator.Interfaces;

namespace Mandatory2DGameFramework.Factory.AbstractFactory.ConcreteFactories
{
    /// <summary>
    /// Factory for creating Mage-themed equipment.
    /// Creates light Cloth armor and magical Staff weapons.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This factory produces a family of items suitable for Mage class characters:
    /// <list type="bullet">
    /// <item><description><b>Weapon:</b> Staff (moderate damage, short-medium range)</description></item>
    /// <item><description><b>Armor:</b> Cloth material (lowest defense values, emphasizes mobility)</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Default values: Weapon damage 76, armor defense ranges from 3-12 depending on slot.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// IItemFactory factory = new MageItemFactory();
    /// 
    /// // Create a full mage equipment set
    /// IWeapon staff = factory.CreateWeapon("Arcane Staff");
    /// IDefenceItem helmet = factory.CreateHelmet("Silk Hood");
    /// IDefenceItem chest = factory.CreateChestArmor("Magic Robe");
    /// 
    /// // All armor is Cloth type (consistent family)
    /// int totalDefense = helmet.Defense + chest.Defense; // 5 + 12 = 17
    /// </code>
    /// </example>
    /// <seealso cref="IItemFactory"/>
    /// <seealso cref="WarriorItemFactory"/>
    /// <seealso cref="HunterItemFactory"/>
    public class MageItemFactory : IItemFactory
    {
        /// <summary>
        /// Creates a Staff weapon for Mages.
        /// </summary>
        /// <param name="name">The name of the weapon.</param>
        /// <param name="damage">The damage value. Default is 76.</param>
        /// <param name="range">The attack range. Default is 30.</param>
        /// <returns>A <see cref="IWeapon"/> implementing Staff weapon.</returns>
        public IWeapon CreateWeapon(string name, int damage = 76, int range = 30)
        {
            return new AttackItem(name, WeaponType.Staff, damage, range);
        }

        public IDefenceItem CreateHelmet(string name, int defense = 5)
        {
            return new DefenceItem(name, ArmorSlot.Head, ArmorType.Cloth, defense);
        }

        public IDefenceItem CreateShoulderArmor(string name, int defense = 4)
        {
            return new DefenceItem(name, ArmorSlot.Shoulders, ArmorType.Cloth, defense);
        }

        public IDefenceItem CreateChestArmor(string name, int defense = 12)
        {
            return new DefenceItem(name, ArmorSlot.Chest, ArmorType.Cloth, defense);
        }

        public IDefenceItem CreateHandArmor(string name, int defense = 3)
        {
            return new DefenceItem(name, ArmorSlot.Hands, ArmorType.Cloth, defense);
        }

        public IDefenceItem CreateLegArmor(string name, int defense = 8)
        {
            return new DefenceItem(name, ArmorSlot.Legs, ArmorType.Cloth, defense);
        }

        public IDefenceItem CreateFeetArmor(string name, int defense = 4)
        {
            return new DefenceItem(name, ArmorSlot.Feet, ArmorType.Cloth, defense);
        }
    }
}

