using Mandatory2DGameFramework.Core.World;
using Mandatory2DGameFramework.Decorator.Interfaces;
using Mandatory2DGameFramework.Composite.Interfaces;
using Mandatory2DGameFramework.Enums;
using Mandatory2DGameFramework.Observer.Implementations;
using Mandatory2DGameFramework.Composite.Implementations;

namespace Mandatory2DGameFramework.Core.Items
{
    /// <summary>
    /// Represents a defensive item/armor in the game.
    /// Implements <see cref="IDefenceItem"/> and <see cref="IGameItem"/> interfaces.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Armor pieces can be:
    /// <list type="bullet">
    /// <item><description>Wrapped with decorators to modify defense values</description></item>
    /// <item><description>Added to <see cref="EquippedArmorSet"/> to create armor collections</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create armor
    /// DefenceItem helmet = new DefenceItem("Iron Helmet", ArmorSlot.Head, ArmorType.Plate, 10);
    /// 
    /// // Use with Decorator
    /// IDefenceItem boosted = new BoostDefenceDecorator(helmet);
    /// int defense = boosted.Defense; // 15 (10 + 5)
    /// 
    /// // Use with Composite
    /// creature.EquippedArmor.Add(helmet);
    /// </code>
    /// </example>
    /// <seealso cref="IDefenceItem"/>
    /// <seealso cref="IGameItem"/>
    public class DefenceItem : WorldObject, IDefenceItem, IGameItem
    {
        private int _defense;

        /// <summary>
        /// Gets or sets the defense value - amount of damage this armor reduces.
        /// </summary>
        /// <value>The defense value. Higher values provide more protection. Cannot be negative.</value>
        public int Defense
        {
            get => _defense;
            set
            {
                int oldValue = _defense;
                _defense = value < 0 ? 0 : value;
                if (oldValue != _defense)
                {
                    ApplicationLogger.Instance.LogPropertyChanged(nameof(DefenceItem), Id, Name, nameof(Defense), oldValue, _defense);
                }
            }
        }

        /// <summary>
        /// Gets the armor slot where this item is equipped.
        /// </summary>
        /// <value>The armor slot (Head, Chest, Legs, etc.).</value>
        /// <remarks>Cannot be changed after construction to maintain armor identity.</remarks>
        public ArmorSlot ArmorSlot { get; private set; }

        /// <summary>
        /// Gets the material type of this armor.
        /// </summary>
        /// <value>The armor material (Leather, Plate, Cloth).</value>
        /// <remarks>Cannot be changed after construction to maintain armor identity.</remarks>
        public ArmorType ArmorType { get; private set; }

        /// <summary>
        /// Gets the total value of this armor piece. For armor, value equals defense.
        /// </summary>
        /// <returns>The defense value from <see cref="Defense"/> property.</returns>
        /// <remarks>
        /// Implements <see cref="IGameItem.GetTotalValue"/>.
        /// Individual armor pieces return their own defense value.
        /// </remarks>
        public int GetTotalValue()
        {
            return Defense;
        }

        /// <summary>
        /// Gets the count of items. Always returns 1 for individual armor pieces.
        /// </summary>
        /// <returns>1 (since this is a single armor piece).</returns>
        /// <remarks>Implements <see cref="IGameItem.GetItemCount"/>.</remarks>
        public int GetItemCount()
        {
            return 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefenceItem"/> class with required parameters.
        /// </summary>
        /// <param name="name">Name of the armor piece.</param>
        /// <param name="armorSlot">Slot where this armor is equipped.</param>
        /// <param name="armorType">Material type of the armor.</param>
        /// <param name="defense">Defense value provided by this armor.</param>
        /// <example>
        /// <code>
        /// DefenceItem helmet = new DefenceItem("Iron Helmet", ArmorSlot.Head, ArmorType.Plate, 10);
        /// </code>
        /// </example>
        public DefenceItem(string name, ArmorSlot armorSlot, ArmorType armorType, int defense)
        {
            Name = string.IsNullOrEmpty(name) ? "Unknown Armor" : name;
            ArmorSlot = armorSlot;
            ArmorType = armorType;
            Defense = defense; // Property setter validates
            ApplicationLogger.Instance.LogObjectCreated(nameof(DefenceItem), Id, Name, $"Slot: {armorSlot}, Type: {armorType}, Defense: {Defense}");
        }

        /// <summary>
        /// Returns a string representation of this armor piece.
        /// </summary>
        /// <returns>A formatted string containing the armor's name, slot, type, and defense value.</returns>
        public override string ToString()
        {
            string safeName = string.IsNullOrEmpty(Name) ? "Unnamed" : Name;
            return $"[{typeof(DefenceItem).Name}: {safeName}] Slot: {ArmorSlot} - Type: {ArmorType} - Defense: {Defense}";
        }
    }
}

