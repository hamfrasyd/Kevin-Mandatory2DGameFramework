using Mandatory2DGameFramework.Template.Base;
using Mandatory2DGameFramework.Observer.Implementations;

namespace Mandatory2DGameFramework.Core.World
{
    /// <summary>
    /// Manages the game world including boundaries, objects, and creatures.
    /// Provides centralized management for all entities in the game.
    /// </summary>
    /// <remarks>
    /// The World class maintains collections of world objects and creatures,
    /// and enforces world boundaries through validation.
    /// </remarks>
    /// <example>
    /// <code>
    /// World world = new World(100, 100, "Fantasy World");
    /// Creature warrior = new Warrior("Bob", 1000);
    /// world.AddCreature(warrior);
    /// 
    /// List&lt;Creature&gt; allCreatures = world.GetCreatures();
    /// </code>
    /// </example>
    public class World
    {
        private int _maxX;
        private int _maxY;

        /// <summary>
        /// World objects present in the world
        /// </summary>
        private readonly List<WorldObject> _worldObjects;

        /// <summary>
        /// Creatures present in the world
        /// </summary>
        private readonly List<Creature> _creatures;

        /// <summary>
        /// Gets or sets the name of the world.
        /// </summary>
        /// <value>The world's name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the maximum X-coordinate value allowed in the world.
        /// </summary>
        /// <value>The maximum X coordinate. Negative values are clamped to 0.</value>
        public int MaxX
        {
            get
            {
                return _maxX;
            }
            set
            {
                if (value < 0)
                {
                    _maxX = 0;
                }
                else
                {
                    _maxX = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum Y-coordinate value allowed in the world.
        /// </summary>
        /// <value>The maximum Y coordinate. Negative values are clamped to 0.</value>
        public int MaxY
        {
            get
            {
                return _maxY;
            }
            set
            {
                if (value < 0)
                {
                    _maxY = 0;
                }
                else
                {
                    _maxY = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class with the specified boundaries.
        /// </summary>
        /// <param name="maxX">The maximum X coordinate for the world. Negative values are clamped to 0.</param>
        /// <param name="maxY">The maximum Y coordinate for the world. Negative values are clamped to 0.</param>
        /// <param name="name">Optional name for the world. Defaults to "Basic World" if null or empty.</param>
        public World(int maxX, int maxY, string name = "Basic World")
        {
            MaxX = maxX;
            MaxY = maxY;
            _worldObjects = new List<WorldObject>();
            _creatures = new List<Creature>();
            Name = string.IsNullOrEmpty(name) ? "Basic World" : name;
            ApplicationLogger.Instance.LogObjectCreated(nameof(World), 0, Name, $"MaxX: {MaxX}, MaxY: {MaxY}");
        }

        /// <summary>
        /// Adds a world object to the world.
        /// </summary>
        /// <param name="worldObject">The world object to add.</param>
        /// <remarks>
        /// Objects added to the world can be tracked and managed centrally.
        /// Null objects are ignored.
        /// </remarks>
        public void AddWorldObject(WorldObject worldObject)
        {
            if (worldObject is not null)
            {
                _worldObjects.Add(worldObject);
                ApplicationLogger.Instance.LogObjectAdded(worldObject.GetType().Name, worldObject.Id, worldObject.Name, Name);
            }
        }

        /// <summary>
        /// Removes a world object from the world if it is marked as removeable.
        /// </summary>
        /// <param name="worldObject">The world object to remove.</param>
        /// <remarks>
        /// Only objects with <see cref="WorldObject.Removeable"/> set to <c>true</c> will be removed.
        /// Null objects are ignored.
        /// </remarks>
        public void RemoveWorldObject(WorldObject worldObject)
        {
            if (worldObject is not null && worldObject.Removeable)
            {
                _worldObjects.Remove(worldObject);
                ApplicationLogger.Instance.LogObjectRemoved(worldObject.GetType().Name, worldObject.Id, worldObject.Name, Name);
            }
        }

        /// <summary>
        /// Gets all world objects currently in the world.
        /// </summary>
        /// <returns>A copy of the world objects list to prevent external modification.</returns>
        public List<WorldObject> GetWorldObjects()
        {
            return new List<WorldObject>(_worldObjects);
        }

        /// <summary>
        /// Adds a creature to the world.
        /// </summary>
        /// <param name="creature">The creature to add.</param>
        /// <remarks>
        /// Creatures are tracked separately from world objects for easier management.
        /// Null creatures are ignored.
        /// </remarks>
        /// <seealso cref="Creature"/>
        public void AddCreature(Creature creature)
        {
            if (creature is not null)
            {
                _creatures.Add(creature);
                ApplicationLogger.Instance.LogObjectAdded(creature.GetType().Name, creature.Id, creature.Name, Name);
            }
        }

        /// <summary>
        /// Removes a creature from the world.
        /// </summary>
        /// <param name="creature">The creature to remove.</param>
        /// <remarks>
        /// Null creatures are ignored.
        /// </remarks>
        public void RemoveCreature(Creature creature)
        {
            if (creature is not null)
            {
                _creatures.Remove(creature);
                ApplicationLogger.Instance.LogObjectRemoved(creature.GetType().Name, creature.Id, creature.Name, Name);
            }
        }

        /// <summary>
        /// Gets all creatures currently in the world.
        /// </summary>
        /// <returns>A copy of the creatures list to prevent external modification.</returns>
        public List<Creature> GetCreatures()
        {
            return new List<Creature>(_creatures);
        }

        /// <summary>
        /// Returns a string representation of this world.
        /// </summary>
        /// <returns>A formatted string containing the world's name and boundaries.</returns>
        public override string ToString()
        {
            string safeName = string.IsNullOrEmpty(Name) ? "Unnamed" : Name;
            return $"{{{nameof(Name)}={safeName}, {nameof(MaxX)} = {MaxX.ToString()}, {nameof(MaxY)} = {MaxY.ToString()}}}";
        }
    }
}
