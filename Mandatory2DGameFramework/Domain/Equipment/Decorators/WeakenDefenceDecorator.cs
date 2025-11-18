using Mandatory2DGameFramework.Domain.Equipment.Armor;
using Mandatory2DGameFramework.Domain.Equipment.Decorators.Base;

namespace Mandatory2DGameFramework.Domain.Equipment.Decorators
{
    /// <summary>
    /// Concrete decorator that weakens defense value.
    /// Decreases the defense value of a wrapped defense item.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This decorator wraps a defense item and decreases its defense value by <see cref="WeakenAmount"/> (default is 3).
    /// The defense value cannot go below zero - negative values are clamped to 0.
    /// </para>
    /// <para>
    /// Useful for representing damaged, cursed, or corroded armor.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// IDefenceItem helmet = new DefenceItem("Iron Helmet", ArmorSlot.Head, ArmorType.Plate, 10);
    /// IDefenceItem weakened = new WeakenDefenceDecorator(helmet);
    /// 
    /// Console.WriteLine(weakened.Defense); // 7 (10 - WeakenAmount)
    /// Console.WriteLine(weakened.Name);    // "Iron Helmet (Weakened)"
    /// 
    /// // Defense cannot go below 0:
    /// IDefenceItem weak = new DefenceItem("Cloth", ArmorSlot.Chest, ArmorType.Cloth, 2);
    /// IDefenceItem veryWeak = new WeakenDefenceDecorator(weak);
    /// Console.WriteLine(veryWeak.Defense); // 0 (not negative)
    /// </code>
    /// </example>
    /// <seealso cref="DefenceItemDecorator"/>
    /// <seealso cref="BoostDefenceDecorator"/>
    public class WeakenDefenceDecorator : DefenceItemDecorator
    {
        /// <summary>
        /// The amount by which defense is weakened.
        /// </summary>
        private int WeakenAmount = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakenDefenceDecorator"/> class.
        /// </summary>
        /// <param name="item">The defense item to weaken.</param>
        public WeakenDefenceDecorator(IDefenceItem item) : base(item)
        {
        }

        /// <summary>
        /// Gets the name with "(Weakened)" suffix to indicate the weakening effect.
        /// </summary>
        /// <value>The wrapped item's name with " (Weakened)" appended.</value>
        public override string Name
        {
            get
            {
                return $"{_wrappedItem.Name} (Weakened)";
            }
        }

        /// <summary>
        /// Gets the weakened defense value.
        /// </summary>
        /// <value>
        /// The wrapped item's defense minus <see cref="WeakenAmount"/>, minimum value of 0.
        /// </value>
        /// <remarks>Defense cannot be negative; values below 0 are clamped to 0.</remarks>
        public override int Defense
        {
            get
            {
                int newValue = _wrappedItem.Defense - WeakenAmount;
                if (newValue < 0)
                {
                    return 0;
                }
                return newValue;
            }
        }
    }
}
