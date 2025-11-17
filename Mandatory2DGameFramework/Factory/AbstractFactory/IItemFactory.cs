using Mandatory2DGameFramework.Interfaces;
using Mandatory2DGameFramework.Decorator.Interfaces;
using Mandatory2DGameFramework.Factory.AbstractFactory.ConcreteFactories;

namespace Mandatory2DGameFramework.Factory.AbstractFactory
{
    /// <summary>
    /// Interface for creating families of related game items.
    /// Each concrete factory creates items that work well together thematically and mechanically.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Factories return product interfaces (<see cref="IWeapon"/>, <see cref="IDefenceItem"/>) instead of concrete classes,
    /// ensuring item families are consistent (Warriors get Plate armor, Mages get Cloth, etc.)
    /// and providing a complete set of equipment through a single factory.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create a consistent set of Warrior items
    /// IItemFactory warriorFactory = new WarriorItemFactory();
    /// IWeapon sword = warriorFactory.CreateWeapon("Iron Sword", 30, 1);
    /// IDefenceItem helmet = warriorFactory.CreateHelmet("Iron Helmet", 10);
    /// IDefenceItem chest = warriorFactory.CreateChestArmor("Iron Plate", 20);
    /// 
    /// // All items are Plate armor (consistent family)
    /// warrior.EquipWeapon(sword);
    /// warrior.EquippedArmor.Add(helmet);
    /// warrior.EquippedArmor.Add(chest);
    /// </code>
    /// </example>
    /// <seealso cref="WarriorItemFactory"/>
    /// <seealso cref="MageItemFactory"/>
    /// <seealso cref="HunterItemFactory"/>
    public interface IItemFactory
    {
        /// <summary>
        /// Creates the default weapon for this factory's item family.
        /// </summary>
        /// <param name="name">The name of the weapon.</param>
        /// <param name="damage">The damage value (default varies by factory).</param>
        /// <param name="range">The attack range (default varies by factory).</param>
        /// <returns>A weapon implementing <see cref="IWeapon"/> interface.</returns>
        IWeapon CreateWeapon(string name, int damage, int range);

        /// <summary>
        /// Creates a helmet for this factory's item family.
        /// </summary>
        /// <param name="name">The name of the helmet.</param>
        /// <param name="defense">The defense value (default varies by factory).</param>
        /// <returns>A helmet implementing <see cref="IDefenceItem"/> for the <see cref="ArmorSlot.Head"/> slot.</returns>
        IDefenceItem CreateHelmet(string name, int defense);

        /// <summary>
        /// Creates shoulder armor for this factory's item family.
        /// </summary>
        /// <param name="name">The name of the shoulder armor.</param>
        /// <param name="defense">The defense value (default varies by factory).</param>
        /// <returns>Shoulder armor implementing <see cref="IDefenceItem"/> for the <see cref="ArmorSlot.Shoulders"/> slot.</returns>
        IDefenceItem CreateShoulderArmor(string name, int defense);

        /// <summary>
        /// Creates chest armor for this factory's item family.
        /// </summary>
        /// <param name="name">The name of the chest armor.</param>
        /// <param name="defense">The defense value (default varies by factory).</param>
        /// <returns>Chest armor implementing <see cref="IDefenceItem"/> for the <see cref="ArmorSlot.Chest"/> slot.</returns>
        IDefenceItem CreateChestArmor(string name, int defense);

        /// <summary>
        /// Creates hand armor for this factory's item family.
        /// </summary>
        /// <param name="name">The name of the hand armor.</param>
        /// <param name="defense">The defense value (default varies by factory).</param>
        /// <returns>Hand armor implementing <see cref="IDefenceItem"/> for the <see cref="ArmorSlot.Hands"/> slot.</returns>
        IDefenceItem CreateHandArmor(string name, int defense);

        /// <summary>
        /// Creates leg armor for this factory's item family.
        /// </summary>
        /// <param name="name">The name of the leg armor.</param>
        /// <param name="defense">The defense value (default varies by factory).</param>
        /// <returns>Leg armor implementing <see cref="IDefenceItem"/> for the <see cref="ArmorSlot.Legs"/> slot.</returns>
        IDefenceItem CreateLegArmor(string name, int defense);

        /// <summary>
        /// Creates feet armor for this factory's item family.
        /// </summary>
        /// <param name="name">The name of the feet armor.</param>
        /// <param name="defense">The defense value (default varies by factory).</param>
        /// <returns>Feet armor implementing <see cref="IDefenceItem"/> for the <see cref="ArmorSlot.Feet"/> slot.</returns>
        IDefenceItem CreateFeetArmor(string name, int defense);
    }
}

