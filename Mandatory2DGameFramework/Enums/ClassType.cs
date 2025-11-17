using Mandatory2DGameFramework.Factory.FactoryMethod;
namespace Mandatory2DGameFramework.Enums
{
    /// <summary>
    /// Defines the types of creature classes available in the game.
    /// Used by <see cref="CreatureFactory"/> to create different creature types.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each class type has unique characteristics:
    /// <list type="table">
    /// <listheader>
    /// <term>Class</term>
    /// <description>Characteristics</description>
    /// </listheader>
    /// <item>
    /// <term>Warrior</term>
    /// <description>Melee fighter with high HP (1000), uses Plate armor and Swords</description>
    /// </item>
    /// <item>
    /// <term>Mage</term>
    /// <description>Ranged caster with medium HP (800), uses Cloth armor and Staves</description>
    /// </item>
    /// <item>
    /// <term>Hunter</term>
    /// <description>Ranged attacker with medium-high HP (900), uses Leather armor and Guns</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// Creature warrior = CreatureFactory.CreateCreature(ClassType.Warrior, "Bob");
    /// Creature mage = CreatureFactory.CreateCreature(ClassType.Mage, "Alice");
    /// </code>
    /// </example>
    /// <seealso cref="Mandatory2DGameFramework.Factory.FactoryMethod.CreatureFactory"/>
    public enum ClassType
    {
        /// <summary>
        /// Warrior class - melee fighter with heavy armor.
        /// </summary>
        Warrior,
        
        /// <summary>
        /// Mage class - ranged caster with light armor.
        /// </summary>
        Mage,
        
        /// <summary>
        /// Hunter class - ranged attacker with medium armor.
        /// </summary>
        Hunter
    }
}

