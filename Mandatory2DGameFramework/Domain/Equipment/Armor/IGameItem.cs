namespace Mandatory2DGameFramework.Domain.Equipment.Armor
{
    /// <summary>
    /// Interface for game items that can be grouped into collections.
    /// Both individual items and collections implement this interface.
    /// </summary>
    /// <remarks>
    /// Individual items return their own values, while collections aggregate values from all contained items.
    /// This allows treating individual items and collections uniformly.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Leaf usage:
    /// IGameItem helmet = new DefenceItem("Iron Helmet", ArmorSlot.Head, ArmorType.Plate, 10);
    /// int value = helmet.GetTotalValue(); // Returns 10
    /// 
    /// // Composite usage:
    /// IGameItem armorSet = new EquippedArmorSet("Full Set");
    /// armorSet.Add(helmet);
    /// int totalValue = armorSet.GetTotalValue(); // Returns sum of all items
    /// </code>
    /// </example>
    /// <seealso cref="DefenceItem"/>
    /// <seealso cref="EquippedArmorSet"/>
    public interface IGameItem
    {
        /// <summary>
        /// Gets the name of this item.
        /// </summary>
        /// <value>The name of the item.</value>
        string Name { get; }
        
        /// <summary>
        /// Gets the total value of this item. For armor items, this represents defense value.
        /// Composite implementations recursively sum values from all children.
        /// </summary>
        /// <returns>The total value. For leafs returns own value, for composites returns sum of children.</returns>
        int GetTotalValue();
        
        /// <summary>
        /// Gets the count of items. Leafs return 1, composites return sum of all child items.
        /// </summary>
        /// <returns>The item count.</returns>
        int GetItemCount();
    }
}

