namespace Mandatory2DGameFramework.Domain.Equipment.Weapons
{
    /// <summary>
    /// Interface for all weapons in the game.
    /// Defines the contract for weapon types that can be equipped and used in combat.
    /// </summary>
    /// <remarks>
    /// Weapons are created by item factories and can be equipped by creatures.
    /// All weapons provide damage and range values for combat calculations.
    /// </remarks>
    /// <example>
    /// <code>
    /// IWeapon sword = new AttackItem("Excalibur", WeaponType.Sword, 50, 1);
    /// int damage = sword.GetDamage(); // Returns 50
    /// creature.EquipWeapon(sword);
    /// </code>
    /// </example>
    /// <seealso cref="AttackItem"/>
    public interface IWeapon
    {
        /// <summary>
        /// Gets the name of the weapon.
        /// </summary>
        /// <value>The weapon's name.</value>
        string Name { get; }
        
        /// <summary>
        /// Gets the damage value this weapon deals.
        /// </summary>
        /// <returns>The damage value as an integer.</returns>
        int GetDamage();
        
        /// <summary>
        /// Gets the attack range of this weapon.
        /// </summary>
        /// <returns>The range value. 1 for melee weapons, higher for ranged weapons.</returns>
        int GetRange();
    }
}

