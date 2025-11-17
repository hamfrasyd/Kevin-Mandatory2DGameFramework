using Mandatory2DGameFramework.Decorator.Base;
using Mandatory2DGameFramework.Decorator.Interfaces;

namespace Mandatory2DGameFramework.Decorator.ConcreteDecorators
{
    /// <summary>
    /// Concrete decorator that boosts defense value.
    /// Increases the defense value of a wrapped defense item.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This decorator wraps a defense item and increases its defense value by <see cref="BoostAmount"/>.
    /// The boost can be applied to any <see cref="IDefenceItem"/>, including other decorated items,
    /// allowing for decorator chaining.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// IDefenceItem helmet = new DefenceItem("Iron Helmet", ArmorSlot.Head, ArmorType.Plate, 10);
    /// IDefenceItem boosted = new BoostDefenceDecorator(helmet);
    /// 
    /// Console.WriteLine(boosted.Defense); // 15 (10 + 5)
    /// Console.WriteLine(boosted.Name);    // "Iron Helmet (Boosted)"
    /// 
    /// // Chain decorators:
    /// IDefenceItem doubleBoosted = new BoostDefenceDecorator(boosted);
    /// Console.WriteLine(doubleBoosted.Defense); // 20 (10 + 5 + 5)
    /// </code>
    /// </example>
    /// <seealso cref="DefenceItemDecorator"/>
    /// <seealso cref="WeakenDefenceDecorator"/>
    public class BoostDefenceDecorator : DefenceItemDecorator
    {
        /// <summary>
        /// The amount by which defense is boosted.
        /// </summary>
        private int BoostAmount = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoostDefenceDecorator"/> class.
        /// </summary>
        /// <param name="item">The defense item to boost.</param>
        public BoostDefenceDecorator(IDefenceItem item) : base(item)
        {
        }

        /// <summary>
        /// Gets the name with "(Boosted)" suffix to indicate the boost effect.
        /// </summary>
        /// <value>The wrapped item's name with " (Boosted)" appended.</value>
        public override string Name
        {
            get
            {
                return $"{_wrappedItem.Name} (Boosted)";
            }
        }

        /// <summary>
        /// Gets the boosted defense value.
        /// </summary>
        /// <value>The wrapped item's defense plus <see cref="BoostAmount"/> (default is 5).</value>
        public override int Defense
        {
            get
            {
                return _wrappedItem.Defense + BoostAmount;
            }
        }
    }
}
