namespace Mandatory2DGameFramework.Domain.Enums
{
    /// <summary>
    /// Defines the equipment slots where armor can be equipped on a creature.
    /// Each creature can have one armor piece per slot.
    /// </summary>
    /// <remarks>
    /// Used in conjunction with <see cref="ArmorType"/> to fully define an armor piece.
    /// ArmorSlot determines where the armor is worn, while ArmorType determines the material.
    /// </remarks>
    /// <example>
    /// <code>
    /// DefenceItem helmet = new DefenceItem("Iron Helmet", ArmorSlot.Head, ArmorType.Plate, 10);
    /// creature.EquippedArmor.Add(helmet);
    /// </code>
    /// </example>
    /// <seealso cref="ArmorType"/>
    public enum ArmorSlot
    {
        /// <summary>
        /// Head armor slot - helmet, hood, etc.
        /// </summary>
        Head,

        /// <summary>
        /// Shoulder armor slot - pauldrons, spaulders, etc.
        /// </summary>
        Shoulders,

        /// <summary>
        /// Chest armor slot - breastplate, tunic, etc.
        /// </summary>
        Chest,

        /// <summary>
        /// Hand armor slot - gloves, gauntlets, etc.
        /// </summary>
        Hands,

        /// <summary>
        /// Leg armor slot - greaves, leggings, etc.
        /// </summary>
        Legs,

        /// <summary>
        /// Feet armor slot - boots, shoes, etc.
        /// </summary>
        Feet
    }
}

