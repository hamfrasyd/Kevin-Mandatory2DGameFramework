using Mandatory2DGameFramework.Decorator.Interfaces;
using Mandatory2DGameFramework.Decorator.ConcreteDecorators;
namespace Mandatory2DGameFramework.Decorator.Base
{
    /// <summary>
    /// Base decorator class for defense items.
    /// Wraps an <see cref="IDefenceItem"/> and delegates to it while allowing subclasses to modify behavior.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This abstract class serves as the foundation for concrete decorators that modify
    /// defense item properties (boost or weaken defense values).
    /// </para>
    /// <para>
    /// Concrete decorators override the virtual properties to modify values while
    /// maintaining the wrapped item's identity.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Concrete decorator usage:
    /// IDefenceItem armor = new DefenceItem("Helmet", ArmorSlot.Head, ArmorType.Plate, 10);
    /// IDefenceItem boosted = new BoostDefenceDecorator(armor);
    /// int defense = boosted.Defense; // Returns 15
    /// </code>
    /// </example>
    /// <seealso cref="IDefenceItem"/>
    /// <seealso cref="BoostDefenceDecorator"/>
    /// <seealso cref="WeakenDefenceDecorator"/>
    public abstract class DefenceItemDecorator : IDefenceItem
    {
        /// <summary>
        /// The wrapped defense item that this decorator modifies.
        /// </summary>
        protected IDefenceItem _wrappedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefenceItemDecorator"/> class.
        /// </summary>
        /// <param name="item">The defense item to wrap/decorate.</param>
        public DefenceItemDecorator(IDefenceItem item)
        {
            _wrappedItem = item;
        }

        /// <summary>
        /// Gets the name of the defense item. Can be overridden to modify the name.
        /// </summary>
        /// <value>The item's name, potentially modified by decorators.</value>
        public virtual string Name
        {
            get
            {
                return _wrappedItem.Name;
            }
        }
        
        /// <summary>
        /// Gets the defense value. Can be overridden to modify the defense value.
        /// </summary>
        /// <value>The defense value, potentially modified by decorators.</value>
        public virtual int Defense
        {
            get
            {
                return _wrappedItem.Defense;
            }
        }
    }
}

