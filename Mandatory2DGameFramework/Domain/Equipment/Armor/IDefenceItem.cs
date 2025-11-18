using Mandatory2DGameFramework.Domain.Equipment.Decorators.Base;

namespace Mandatory2DGameFramework.Domain.Equipment.Armor
{
    /// <summary>
    /// Interface for defense items that can be modified with decorators.
    /// Defines the contract for armor pieces that can be wrapped with buffs/debuffs.
    /// </summary>
    /// <remarks>
    /// Defense items can be wrapped with decorators that modify their defense values dynamically.
    /// Decorators can be chained to apply multiple modifications.
    /// </remarks>
    /// <example>
    /// <code>
    /// IDefenceItem armor = new DefenceItem("Iron Plate", ArmorSlot.Chest, ArmorType.Plate, 20);
    /// IDefenceItem boosted = new BoostDefenceDecorator(armor);
    /// int defense = boosted.Defense; // Returns 25 (20 + 5 boost)
    /// </code>
    /// </example>
    /// <seealso cref="DefenceItem"/>
    /// <seealso cref="DefenceItemDecorator"/>
    public interface IDefenceItem
    {
        /// <summary>
        /// Gets the name of the defense item.
        /// </summary>
        /// <value>The item's name.</value>
        string Name { get; }
        
        /// <summary>
        /// Gets the defense value provided by this item.
        /// </summary>
        /// <value>The defense value. Higher values provide more protection.</value>
        int Defense { get; }
    }
}

