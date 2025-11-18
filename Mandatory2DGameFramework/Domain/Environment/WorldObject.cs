using Mandatory2DGameFramework.Domain.Equipment.Armor;
namespace Mandatory2DGameFramework.Domain.Environment
{
    /// <summary>
    /// Base class for all objects that exist in the game world.
    /// Provides common properties for positioning and management.
    /// </summary>
    /// <remarks>
    /// This serves as the foundation for all items, weapons, armor, and other world objects.
    /// Inheriting from this class allows objects to be placed in the world and tracked.
    /// </remarks>
    /// <seealso cref="AttackItem"/>
    /// <seealso cref="DefenceItem"/>
    public class WorldObject
    {
        private static int _nextId = 1;

        /// <summary>
        /// Gets the unique identifier for this world object.
        /// </summary>
        /// <value>The unique ID assigned at creation.</value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the name of the world object.
        /// </summary>
        /// <value>The object's name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate position in the world.
        /// </summary>
        /// <value>The X coordinate.</value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate position in the world.
        /// </summary>
        /// <value>The Y coordinate.</value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this object can be removed from the world.
        /// </summary>
        /// <value><c>true</c> if removeable; otherwise, <c>false</c>.</value>
        public bool Removeable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldObject"/> class with default property values.
        /// </summary>
        /// <remarks>
        /// Default values: Name = empty string, Position = (0, 0), Removeable = false.
        /// A unique ID is automatically assigned.
        /// </remarks>
        public WorldObject()
        {
            Id = _nextId++;
            Name = string.Empty;
            X = 0;
            Y = 0;
            Removeable = false;
        }

        /// <summary>
        /// Returns a string representation of this world object.
        /// </summary>
        /// <returns>A formatted string containing the object's type, name, ID, position, and removeable status.</returns>
        public override string ToString()
        {
            string safeName = string.IsNullOrEmpty(Name) ? "Unnamed" : Name;
            string removeable = Removeable ? "Yes" : "No";
            string position = $"({X}, {Y})";
            return $"[{typeof(WorldObject).Name}: {safeName}] ID: {Id} - Position: {position} - Removeable: {removeable}";
        }
    }
}
