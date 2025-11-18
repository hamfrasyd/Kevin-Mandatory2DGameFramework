namespace Mandatory2DGameFramework.Domain.Enums
{
    /// <summary>
    /// Defines the material types for armor pieces.
    /// Different materials provide different levels of protection and weight.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Material types are associated with creature classes through item factories:
    /// <list type="bullet">
    /// <item><description><b>Plate:</b> Heavy armor for Warriors - highest defense</description></item>
    /// <item><description><b>Leather:</b> Medium armor for Hunters - balanced defense</description></item>
    /// <item><description><b>Cloth:</b> Light armor for Mages - lowest defense</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Warriors use Plate armor
    /// IItemFactory warriorFactory = new WarriorItemFactory();
    /// IDefenceItem plateHelmet = warriorFactory.CreateHelmet("Iron Helmet");
    /// // Creates: ArmorType.Plate
    /// 
    /// // Mages use Cloth armor
    /// IItemFactory mageFactory = new MageItemFactory();
    /// IDefenceItem clothHelmet = mageFactory.CreateHelmet("Silk Hood");
    /// // Creates: ArmorType.Cloth
    /// </code>
    /// </example>
    /// <seealso cref="ArmorSlot"/>
    public enum ArmorType
    {
        /// <summary>
        /// Leather armor - light, flexible
        /// </summary>
        Leather,

        /// <summary>
        /// Plate armor - heavy, strong protection
        /// </summary>
        Plate,

        /// <summary>
        /// Cloth armor - very light, minimal protection
        /// </summary>
        Cloth
    }
}

