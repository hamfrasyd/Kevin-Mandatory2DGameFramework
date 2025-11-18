using Mandatory2DGameFramework.Domain.Environment;
using Mandatory2DGameFramework.Domain.Logging.Observers;

namespace Mandatory2DGameFramework.Domain.Equipment.Armor
{
    /// <summary>
    /// Manages equipped armor pieces as a collection.
    /// Can contain both individual armor pieces and other armor sets.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Both <see cref="DefenceItem"/> and <see cref="EquippedArmorSet"/> implement <see cref="IGameItem"/>,
    /// allowing them to be treated uniformly.
    /// </para>
    /// <para>
    /// Uses recursive aggregation to calculate total values from nested structures.
    /// This allows for nested armor sets and flexible armor organization.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Simple usage with leafs:
    /// EquippedArmorSet armor = new EquippedArmorSet("Full Plate");
    /// armor.Add(new DefenceItem("Helmet", ArmorSlot.Head, ArmorType.Plate, 10));
    /// armor.Add(new DefenceItem("Chest", ArmorSlot.Chest, ArmorType.Plate, 20));
    /// int totalDefense = armor.GetTotalValue(); // 30
    /// 
    /// // Nested composites:
    /// EquippedArmorSet upperBody = new EquippedArmorSet("Upper Body");
    /// upperBody.Add(new DefenceItem("Helmet", ArmorSlot.Head, ArmorType.Plate, 10));
    /// 
    /// EquippedArmorSet fullSet = new EquippedArmorSet("Full Set");
    /// fullSet.Add(upperBody); // Composite contains another composite!
    /// int total = fullSet.GetTotalValue(); // Recursive aggregation
    /// </code>
    /// </example>
    /// <seealso cref="IGameItem"/>
    /// <seealso cref="DefenceItem"/>
    public class EquippedArmorSet : IGameItem
    {
        /// <summary>
        /// Gets or sets the name of this armor set.
        /// </summary>
        /// <value>The armor set's name.</value>
        public string Name { get; set; }

        /// <summary>
        /// The collection of items in this composite. Can hold both DefenceItem and EquippedArmorSet.
        /// </summary>
        private readonly List<IGameItem> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquippedArmorSet"/> class.
        /// </summary>
        /// <param name="name">Optional name for this armor set. Defaults to "Equipped Armor".</param>
        public EquippedArmorSet(string name = "Equipped Armor")
        {
            Name = name;
            _items = new List<IGameItem>();
        }

        /// <summary>
        /// Adds an item to this armor set.
        /// </summary>
        /// <param name="item">The item to add. Can be a <see cref="DefenceItem"/> or another <see cref="EquippedArmorSet"/>.</param>
        /// <remarks>
        /// <para>Accepts any <see cref="IGameItem"/>, allowing nested armor sets.</para>
        /// <para>Duplicate items are not added. Null items are ignored.</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// armor.Add(new DefenceItem("Helmet", ArmorSlot.Head, ArmorType.Plate, 10));
        /// armor.Add(new EquippedArmorSet("Nested Set")); // Can add composites!
        /// </code>
        /// </example>
        public void Add(IGameItem item)
        {
            if (item is not null && !_items.Contains(item))
            {
                _items.Add(item);
                string itemId = item is WorldObject wo ? wo.Id.ToString() : "N/A";
                ApplicationLogger.Instance.LogObjectModified(nameof(EquippedArmorSet), 0, Name, $"Item added: {item.GetType().Name}-{item.Name} (ID: {itemId})");
            }
        }

        /// <summary>
        /// Removes an item from this armor set.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><c>true</c> if the item was removed; otherwise, <c>false</c>.</returns>
        public bool Remove(IGameItem item)
        {
            bool removed = _items.Remove(item);
            if (removed)
            {
                string itemId = item is WorldObject wo ? wo.Id.ToString() : "N/A";
                ApplicationLogger.Instance.LogObjectModified(nameof(EquippedArmorSet), 0, Name, $"Item removed: {item.GetType().Name}-{item.Name} (ID: {itemId})");
            }
            return removed;
        }

        /// <summary>
        /// Gets all items directly contained in this armor set.
        /// </summary>
        /// <returns>A copy of the items list to prevent external modification.</returns>
        /// <remarks>Does not recursively return nested items; only direct children.</remarks>
        public List<IGameItem> GetAllItems()
        {
            return new List<IGameItem>(_items);
        }

        // IGameItem interface methods

        /// <summary>
        /// Gets the total value of all items in this armor set.
        /// </summary>
        /// <returns>The sum of all item values, recursively aggregated from all children.</returns>
        /// <remarks>
        /// <para>
        /// For armor items, value represents defense.
        /// </para>
        /// <para>
        /// Individual items return their own defense value, armor sets sum all children's values.
        /// This enables proper aggregation in nested structures.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// EquippedArmorSet set = new EquippedArmorSet();
        /// set.Add(new DefenceItem("Helmet", ArmorSlot.Head, ArmorType.Plate, 10));
        /// set.Add(new DefenceItem("Chest", ArmorSlot.Chest, ArmorType.Plate, 20));
        /// int total = set.GetTotalValue(); // Returns 30 (10 + 20)
        /// </code>
        /// </example>
        public int GetTotalValue()
        {
            int total = 0;
            foreach (var item in _items)
            {
                total += item.GetTotalValue();
            }
            return total;
        }

        /// <summary>
        /// Gets the total count of all items in this armor set.
        /// </summary>
        /// <returns>The total number of individual items, recursively counted from all nested structures.</returns>
        /// <remarks>
        /// <para>
        /// Each individual item returns 1, armor sets return the sum of their children.
        /// </para>
        /// <para>
        /// This allows accurate counting even with deeply nested structures.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// EquippedArmorSet set = new EquippedArmorSet();
        /// set.Add(new DefenceItem("Helmet", ArmorSlot.Head, ArmorType.Plate, 10));
        /// set.Add(new DefenceItem("Chest", ArmorSlot.Chest, ArmorType.Plate, 20));
        /// int count = set.GetItemCount(); // Returns 2
        /// </code>
        /// </example>
        public int GetItemCount()
        {
            int count = 0;
            foreach (var item in _items)
            {
                count += item.GetItemCount();
            }
            return count;
        }
    }
}
