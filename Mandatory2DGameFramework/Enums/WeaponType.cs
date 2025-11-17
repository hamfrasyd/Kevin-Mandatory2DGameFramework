namespace Mandatory2DGameFramework.Enums
{
    /// <summary>
    /// Defines the types of weapons available in the game.
    /// Different weapon types have varying damage, range, and characteristics.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Weapons are categorized by their attack style:
    /// <list type="bullet">
    /// <item><description><b>Melee weapons:</b> Sword, Axe, Mace, Dagger (Range: 1)</description></item>
    /// <item><description><b>Ranged weapons:</b> Bow, Gun, Wand, Staff (Range: 2-30)</description></item>
    /// <item><description><b>Unarmed:</b> Default when no weapon equipped (Range: 1, low damage)</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// AttackItem sword = new AttackItem("Excalibur", WeaponType.Sword, 50, 1);
    /// AttackItem gun = new AttackItem("Rifle", WeaponType.Gun, 20, 10);
    /// </code>
    /// </example>
    public enum WeaponType
    {
        /// <summary>
        /// Sword - melee weapon (Warriors, Berserkers)
        /// </summary>
        Sword,

        /// <summary>
        /// Axe - heavy melee weapon (Warriors, Berserkers)
        /// </summary>
        Axe,

        /// <summary>
        /// Mace - blunt melee weapon (Warriors)
        /// </summary>
        Mace,

        /// <summary>
        /// Staff - magical weapon (Mages)
        /// </summary>
        Staff,

        /// <summary>
        /// Wand - light magical weapon (Mages)
        /// </summary>
        Wand,

        /// <summary>
        /// Bow - ranged weapon (Archers)
        /// </summary>
        Bow,

        /// <summary>
        /// Gun - ranged weapon (Hunters)
        /// </summary>
        Gun,

        /// <summary>
        /// Dagger - light weapon (all classes)
        /// </summary>
        Dagger,

        /// <summary>
        /// Fists/unarmed - default for all classes
        /// </summary>
        Unarmed
    }
}

