using Mandatory2DGameFramework.Domain.Enums;
using Mandatory2DGameFramework.Domain.Environment;
using Mandatory2DGameFramework.Domain.Logging.Observers;

namespace Mandatory2DGameFramework.Domain.Equipment.Weapons
{
    /// <summary>
    /// Represents an attack item/weapon in the game.
    /// Implements <see cref="IWeapon"/> interface.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Weapons are created by item factories and can be equipped by creatures.
    /// Supports operator overloading (+) for dual wielding functionality.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create a weapon
    /// AttackItem sword = new AttackItem("Excalibur", WeaponType.Sword, 50, 1);
    /// 
    /// // Dual wield
    /// AttackItem dagger = new AttackItem("Dagger", WeaponType.Dagger, 10, 1);
    /// AttackItem dualWield = sword + dagger; // Combined: 60 damage
    /// </code>
    /// </example>
    /// <seealso cref="IWeapon"/>
    public class AttackItem : WorldObject, IWeapon
    {
        private int _hit;
        private int _range;
        private int _value;

        /// <summary>
        /// Gets or sets the damage dealt by this attack item.
        /// </summary>
        /// <value>The damage value. Cannot be negative.</value>
        public int Hit
        {
            get => _hit;
            set
            {
                int oldValue = _hit;
                _hit = value < 0 ? 0 : value;
                if (oldValue != _hit)
                {
                    ApplicationLogger.Instance.LogPropertyChanged(nameof(AttackItem), Id, Name, nameof(Hit), oldValue, _hit);
                }
            }
        }

        /// <summary>
        /// Gets or sets the range of the attack.
        /// </summary>
        /// <value>The range value. 1 for melee, higher for ranged weapons. Cannot be negative.</value>
        public int Range
        {
            get => _range;
            set
            {
                int oldValue = _range;
                _range = value < 0 ? 0 : value;
                if (oldValue != _range)
                {
                    ApplicationLogger.Instance.LogPropertyChanged(nameof(AttackItem), Id, Name, nameof(Range), oldValue, _range);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of this weapon (for trading/economy systems).
        /// </summary>
        /// <value>The weapon's value. Cannot be negative.</value>
        public int Value
        {
            get => _value;
            set
            {
                int oldValue = _value;
                _value = value < 0 ? 0 : value;
                if (oldValue != _value)
                {
                    ApplicationLogger.Instance.LogPropertyChanged(nameof(AttackItem), Id, Name, nameof(Value), oldValue, _value);
                }
            }
        }

        /// <summary>
        /// Gets the type of weapon.
        /// </summary>
        /// <value>The weapon type.</value>
        /// <remarks>Cannot be changed after construction to maintain weapon identity.</remarks>
        public WeaponType Type { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackItem"/> class with required parameters.
        /// </summary>
        /// <param name="name">Name of the weapon.</param>
        /// <param name="weaponType">Type of weapon.</param>
        /// <param name="damage">Damage value the weapon deals.</param>
        /// <param name="range">Attack range of the weapon.</param>
        /// <example>
        /// <code>
        /// AttackItem sword = new AttackItem("Iron Sword", WeaponType.Sword, 30, 1);
        /// </code>
        /// </example>
        public AttackItem(string name, WeaponType weaponType, int damage, int range)
        {
            Name = string.IsNullOrEmpty(name) ? "Unknown Weapon" : name;
            Type = weaponType;
            Hit = damage; // Property setter validates
            Range = range; // Property setter validates
            ApplicationLogger.Instance.LogObjectCreated(nameof(AttackItem), Id, Name, $"Type: {weaponType}, Damage: {Hit}, Range: {Range}");
        }

        /// <summary>
        /// Combines two weapons for dual wielding using the + operator.
        /// </summary>
        /// <param name="left">The first weapon (main hand).</param>
        /// <param name="right">The second weapon (off hand).</param>
        /// <returns>
        /// A new <see cref="AttackItem"/> with combined damage and maximum range.
        /// Returns non-null weapon if only one is provided, or empty hands if both are null.
        /// </returns>
        /// <remarks>
        /// <para>Demonstrates operator overload requirement.</para>
        /// <para>Combined weapon inherits the type from the left weapon.</para>
        /// <para>Damage is additive, range uses the maximum of the two weapons.</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// AttackItem sword = new AttackItem("Sword", WeaponType.Sword, 30, 1);
        /// AttackItem dagger = new AttackItem("Dagger", WeaponType.Dagger, 15, 1);
        /// AttackItem dualWield = sword + dagger;
        /// // Result: "Sword &amp; Dagger" with 45 damage
        /// </code>
        /// </example>
        public static AttackItem operator +(AttackItem? left, AttackItem? right)
        {
            if (left is null && right is null)
            {
                return new AttackItem("Empty Hands", WeaponType.Unarmed, 0, 0);
            }

            if (left is null)
            {
                return right!;
            }

            if (right is null)
            {
                return left!;
            }

            // Combine weapons for dual wielding
            int combinedRange = left.Range > right.Range ? left.Range : right.Range;
            int combinedDamage = left.Hit + right.Hit;

            string leftName = string.IsNullOrEmpty(left.Name) ? "Unknown" : left.Name;
            string rightName = string.IsNullOrEmpty(right.Name) ? "Unknown" : right.Name;

            var combined = new AttackItem(
                $"{leftName} & {rightName}",
                left.Type,  // Use left weapon type
                combinedDamage,
                combinedRange
            );

            combined.Removeable = left.Removeable || right.Removeable;
            combined.Value = left.Value + right.Value; // Property setters validate

            return combined;
        }

        // IWeapon interface methods

        /// <summary>
        /// Gets the damage value this weapon deals.
        /// </summary>
        /// <returns>The damage value from <see cref="Hit"/> property.</returns>
        /// <remarks>Implements <see cref="IWeapon.GetDamage"/>.</remarks>
        public int GetDamage()
        {
            return Hit;
        }

        /// <summary>
        /// Gets the attack range of this weapon.
        /// </summary>
        /// <returns>The range value from <see cref="Range"/> property.</returns>
        /// <remarks>Implements <see cref="IWeapon.GetRange"/>.</remarks>
        public int GetRange()
        {
            return Range;
        }

        /// <summary>
        /// Returns a string representation of this weapon.
        /// </summary>
        /// <returns>A formatted string containing the weapon's name, damage, and range.</returns>
        public override string ToString()
        {
            string safeName = string.IsNullOrEmpty(Name) ? "Unnamed" : Name;
            return $"[{typeof(AttackItem).Name}: {safeName}] Damage: {Hit} - Range: {Range}";
        }
    }
}

