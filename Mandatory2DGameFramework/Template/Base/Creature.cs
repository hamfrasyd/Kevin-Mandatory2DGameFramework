using Mandatory2DGameFramework.Observer.Interfaces;
using Mandatory2DGameFramework.Strategy.Interfaces;
using Mandatory2DGameFramework.Interfaces;
using Mandatory2DGameFramework.Composite.Implementations;
using Mandatory2DGameFramework.Observer.Implementations;

namespace Mandatory2DGameFramework.Template.Base
{
    /// <summary>
    /// Abstract base class for all creatures in the game.
    /// Provides combat, equipment, and event notification capabilities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Creatures can:
    /// <list type="bullet">
    /// <item><description>Perform attacks using attack strategies</description></item>
    /// <item><description>Equip <see cref="IWeapon"/> weapons and armor</description></item>
    /// <item><description>Notify observers when taking damage or dying</description></item>
    /// <item><description>Override attack modifiers for class-specific bonuses</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create creature
    /// Creature warrior = new Warrior("Bob", 1000);
    /// 
    /// // Observer pattern - attach logger (singleton, auto-starts combat logging)
    /// warrior.Attach(CombatLogger.Instance);
    /// 
    /// // Equip items
    /// warrior.EquipWeapon(new AttackItem("Sword", WeaponType.Sword, 30, 1));
    /// warrior.EquippedArmor.Add(new DefenceItem("Helmet", ArmorSlot.Head, ArmorType.Plate, 10));
    /// 
    /// // Strategy + Template Method pattern - perform attack
    /// Creature enemy = new Mage("Evil Mage", 800);
    /// warrior.PerformAttack(enemy, range: 1);
    /// </code>
    /// </example>
    /// <seealso cref="IAttackable"/>
    /// <seealso cref="IAttackStrategy"/>
    /// <seealso cref="ICreatureObserver"/>
    public abstract class Creature : IAttackable
    {
        private static int _nextId = 1;

        protected IAttackStrategy? _attackStrategy;
        protected int _baseAutoAttackDamage;

        /// <summary>
        /// Gets the unique identifier for this creature.
        /// </summary>
        /// <value>The unique ID assigned at creation.</value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the default base damage when unarmed.
        /// </summary>
        /// <value>The default base damage value.</value>
        public int BaseAutoAttackDamage => _baseAutoAttackDamage;

        private int _hp;
        private int _maxHP;
        private readonly List<ICreatureObserver> _observers;

        /// <summary>
        /// Gets or sets the name of the creature.
        /// </summary>
        /// <value>The creature's name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the equipped armor set.
        /// </summary>
        /// <value>The armor collection containing all equipped armor pieces.</value>
        public EquippedArmorSet EquippedArmor { get; private set; }

        /// <summary>
        /// Gets the currently equipped weapon.
        /// </summary>
        /// <value>The equipped <see cref="IWeapon"/>, or <c>null</c> if unarmed.</value>
        public IWeapon? EquippedWeapon { get; private set; }

        /// <summary>
        /// Gets or sets the X coordinate position in the world.
        /// </summary>
        /// <value>The X coordinate.</value>
        public int XCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate position in the world.
        /// </summary>
        /// <value>The Y coordinate.</value>
        public int YCoordinate { get; set; }

        /// <summary>
        /// Gets a value indicating whether this creature is alive.
        /// </summary>
        /// <value><c>true</c> if HP > 0; otherwise, <c>false</c>.</value>
        public bool IsAlive { get => HitPoint > 0; }

        /// <summary>
        /// Gets the maximum hit points for this creature.
        /// </summary>
        /// <value>The maximum HP value set at construction.</value>
        public int MaxHP { get => _maxHP; }

        /// <summary>
        /// Gets or sets the current hit points. Automatically notifies observers when changed.
        /// </summary>
        /// <value>The current HP, clamped between 0 and <see cref="MaxHP"/>.</value>
        /// <remarks>
        /// When HP decreases, triggers <see cref="ICreatureObserver.OnCreatureHit"/>.
        /// When HP reaches 0, triggers <see cref="ICreatureObserver.OnCreatureDied"/>.
        /// </remarks>
        public int HitPoint
        {
            get => _hp;
            set
            {
                int oldValue = _hp;

                int newValue = value;

                if (newValue > _maxHP) // Prevent hp from exceeding max hp
                    newValue = _maxHP;

                if (newValue < 0) // Prevent hp from going below 0
                    newValue = 0;

                _hp = newValue;

                if (_hp <= 0 && oldValue > 0)
                    NotifyObserversDied(); // Notify observers of death
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class with required parameters.
        /// </summary>
        /// <param name="name">Name of the creature.</param>
        /// <param name="maxHP">Maximum hit points. Also sets current HP to this value.</param>
        /// <remarks>
        /// Subclasses must call this constructor and should set their attack strategy in their own constructor.
        /// A unique ID is automatically assigned.
        /// </remarks>
        protected Creature(string name, int maxHP)
        {
            Id = _nextId++;
            Name = string.IsNullOrEmpty(name) ? "Unknown" : name;

            // Ensure maxHP is positive
            if (maxHP <= 0)
            {
                maxHP = 100; // Default to 100 if invalid
            }

            _hp = maxHP;
            _maxHP = maxHP;
            _baseAutoAttackDamage = 16; 
            _attackStrategy = null;
            _observers = new List<ICreatureObserver>();
            EquippedArmor = new EquippedArmorSet("Equipped Armor");
            ApplicationLogger.Instance.LogObjectCreated(GetType().Name, Id, Name, $"MaxHP: {_maxHP}");
        }

        #region Observer Methods

        /// <summary>
        /// Attaches an observer to receive notifications about this creature's events.
        /// </summary>
        /// <param name="observer">The observer to attach.</param>
        /// <remarks>
        /// Observers are notified when creature takes damage or dies.
        /// Duplicate observers are not added.
        /// </remarks>
        public void Attach(ICreatureObserver observer)
        {
            if (observer is not null && !_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        /// <summary>
        /// Detaches an observer from this creature.
        /// </summary>
        /// <param name="observer">The observer to detach.</param>
        public void Detach(ICreatureObserver observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// Notifies all observers that this creature performed an attack.
        /// </summary>
        /// <param name="action">The action performed.</param>
        /// <param name="target">The target being attacked.</param>
        /// <param name="damage">The amount of damage dealt.</param>
        private void NotifyObserversDamageDone(string action, IAttackable target, int damage)
        {
            foreach (var observer in _observers)
            {
                observer.OnDamageDone(this, action, target, damage);
            }
        }

        /// <summary>
        /// Notifies all observers that this creature was hit.
        /// </summary>
        /// <param name="action">The action that caused damage.</param>
        /// <param name="damageTaken">The amount of damage taken.</param>
        /// <param name="damageMitigated">The amount of damage mitigated.</param>
        private void NotifyObserversHit(string action, int damageTaken, int damageMitigated)
        {
            foreach (var observer in _observers)
            {
                observer.OnCreatureHit(this, action, damageTaken, damageMitigated);
            }
        }

        /// <summary>
        /// Notifies all observers that this creature has died.
        /// </summary>
        private void NotifyObserversDied()
        {
            foreach (var observer in _observers)
            {
                observer.OnCreatureDied(this);
            }
        }

        #endregion

        #region Attack Strategy Methods

        /// <summary>
        /// Sets the attack strategy for this creature.
        /// </summary>
        /// <param name="strategy">The attack strategy to use. Null values are ignored.</param>
        /// <remarks>
        /// Allows runtime changing of attack behavior.
        /// If null is provided, the strategy remains unchanged.
        /// </remarks>
        public void SetAttackStrategy(IAttackStrategy strategy)
        {
            if (strategy is not null)
            {
                _attackStrategy = strategy;
            }
        }

        #endregion

        #region Attack Methods

        /// <summary>
        /// Performs an attack on the target.
        /// </summary>
        /// <param name="target">The target to attack.</param>
        /// <param name="range">The distance to the target. Default is 0 for melee range.</param>
        /// <remarks>
        /// <para>
        /// Attack algorithm:
        /// <list type="number">
        /// <item><description>Validate attacker and target are alive</description></item>
        /// <item><description>Calculate base damage using attack strategy</description></item>
        /// <item><description>Apply class-specific modifiers via <see cref="ApplyAttackModifiers"/></description></item>
        /// <item><description>Apply damage to target</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// Creature warrior = new Warrior("Bob", 1000);
        /// Creature enemy = new Mage("Alice", 800);
        /// 
        /// // Melee attack
        /// warrior.PerformAttack(enemy, range: 1);
        /// 
        /// // Ranged attack
        /// enemy.PerformAttack(warrior, range: 25);
        /// </code>
        /// </example>
        public void PerformAttack(IAttackable target, int range = 0)
        {
            if (IsAlive is false || _attackStrategy is null || target is null || !target.IsAlive)
            {
                return;
            }

            if (range < 0)
            {
                range = 0;
            }

            int baseDamage = _attackStrategy.Attack(this, range);
            int finalDamage = ApplyAttackModifiers(baseDamage);

            if (finalDamage < 0)
            {
                finalDamage = 0;
            }

            NotifyObserversDamageDone("Attack", target, finalDamage);
            target.TakeDamage(finalDamage);
        }

        /// <summary>
        /// Applies class-specific modifiers to base damage.
        /// </summary>
        /// <param name="baseDamage">The base damage calculated by the attack strategy.</param>
        /// <returns>The modified damage value.</returns>
        /// <remarks>
        /// <para>
        /// Base implementation returns damage unchanged.
        /// Subclasses override to add class-specific bonuses (e.g., Warrior bonus at low HP, Mage bonus at high HP).
        /// </para>
        /// </remarks>
        protected virtual int ApplyAttackModifiers(int baseDamage)
        {
            return baseDamage;
        }

        #endregion

        /// <summary>
        /// Takes damage from an attack, applying defense reduction.
        /// </summary>
        /// <param name="damage">The incoming damage amount. Negative values are treated as 0.</param>
        /// <remarks>
        /// Defense is calculated from all equipped armor pieces via <see cref="EquippedArmor.GetTotalValue"/>.
        /// Damage is reduced by total defense, minimum 0. Triggers observer notifications.
        /// </remarks>
        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                damage = 0;
            }

            int totalDefense = EquippedArmor.GetTotalValue();
            int damageMitigated = damage > totalDefense ? totalDefense : damage;
            int actualDamage = damage - totalDefense;

            if (actualDamage < 0)
            {
                actualDamage = 0;
            }

            NotifyObserversHit("Attack", actualDamage, damageMitigated);
            HitPoint -= actualDamage;
        }

        /// <summary>
        /// Equips a weapon for this creature.
        /// </summary>
        /// <param name="weapon">The <see cref="IWeapon"/> to equip.</param>
        /// <returns><c>true</c> if weapon was equipped; <c>false</c> if weapon was null.</returns>
        public bool EquipWeapon(IWeapon weapon)
        {
            if (weapon is null)
            {
                return false;
            }
            string oldWeaponName = EquippedWeapon?.Name ?? "None";
            EquippedWeapon = weapon;
            ApplicationLogger.Instance.LogObjectModified(GetType().Name, Id, Name, $"Weapon equipped: {oldWeaponName} -> {weapon.Name}");
            return true;
        }

        /// <summary>
        /// Returns a comprehensive string representation of this creature.
        /// </summary>
        /// <returns>A formatted string with ID, name, HP, status, weapon, and armor information.</returns>
        public override string ToString()
        {
            string safeName = string.IsNullOrEmpty(Name) ? "Unknown" : Name;
            string weaponInfo = "Unarmed";

            if (EquippedWeapon is not null)
            {
                weaponInfo = string.IsNullOrEmpty(EquippedWeapon.Name) ? "Unnamed Weapon" : EquippedWeapon.Name;
            }

            string status = IsAlive ? "Alive" : "Dead";

            int armorCount = EquippedArmor.GetItemCount();
            int totalDefense = EquippedArmor.GetTotalValue();

            string position = $"({XCoordinate}, {YCoordinate})";

            return $"[{typeof(Creature).Name}: {safeName}] ID: {Id} - HP: {HitPoint}/{MaxHP} - Status: {status} - Weapon: {weaponInfo} - Armor: {armorCount} pieces ({totalDefense} defense) - Position: {position}";
        }

    }
}
