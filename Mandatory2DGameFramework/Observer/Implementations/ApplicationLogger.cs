using Mandatory2DGameFramework.Loggers;

namespace Mandatory2DGameFramework.Observer.Implementations
{
    /// <summary>
    /// Logger that tracks system-wide application events.
    /// Logs object creation, modification, retrieval, and state changes.
    /// Singleton pattern - automatically starts application logging on first access.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ApplicationLogger writes detailed information about application lifecycle events to ApplicationLog.txt.
    /// This includes:
    /// <list type="bullet">
    /// <item><description>Object creation (Creatures, Items, World objects)</description></item>
    /// <item><description>Property value changes</description></item>
    /// <item><description>Object retrieval operations</description></item>
    /// <item><description>Object modifications (equipment, armor, etc.)</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// The logger automatically initializes application logging when first accessed.
    /// Uses singleton pattern to ensure only one instance exists.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Application logging starts automatically on first access
    /// ApplicationLogger.Instance.LogObjectCreated("Warrior", 1, "Bob");
    /// 
    /// // Create objects - automatically logged
    /// Creature warrior = new Warrior("Bob");
    /// AttackItem sword = new AttackItem("Sword", WeaponType.Sword, 30, 1);
    /// 
    /// // Modify properties - automatically logged
    /// sword.Hit = 35; // Logs: "[Property Changed] AttackItem-Sword (ID: 1): Hit changed from 30 to 35"
    /// </code>
    /// </example>
    /// <seealso cref="MyLogger"/>
    public class ApplicationLogger
    {
        private static ApplicationLogger? _instance;
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets the singleton instance of ApplicationLogger.
        /// Automatically starts application logging on first access.
        /// </summary>
        public static ApplicationLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ApplicationLogger();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor to enforce singleton pattern.
        /// Automatically starts application logging.
        /// </summary>
        private ApplicationLogger()
        {
            MyLogger.Instance.StartApplicationLog();
        }

        /// <summary>
        /// Logs when an object is created.
        /// </summary>
        /// <param name="objectType">The type of object being created.</param>
        /// <param name="objectId">The unique ID of the object.</param>
        /// <param name="objectName">The name of the object.</param>
        /// <param name="details">Optional additional details about the object.</param>
        public void LogObjectCreated(string objectType, int objectId, string objectName, string? details = null)
        {
            string detailText = string.IsNullOrEmpty(details) ? "" : $" - {details}";
            MyLogger.Instance.LogApplicationInfo($"[Object Created] {objectType}-{objectName} (ID: {objectId}){detailText}");
        }

        /// <summary>
        /// Logs when a property value is changed.
        /// </summary>
        /// <param name="objectType">The type of object.</param>
        /// <param name="objectId">The unique ID of the object.</param>
        /// <param name="objectName">The name of the object.</param>
        /// <param name="propertyName">The name of the property being changed.</param>
        /// <param name="oldValue">The previous value.</param>
        /// <param name="newValue">The new value.</param>
        public void LogPropertyChanged(string objectType, int objectId, string objectName, string propertyName, object oldValue, object newValue)
        {
            MyLogger.Instance.LogApplicationInfo($"[Property Changed] {objectType}-{objectName} (ID: {objectId}): {propertyName} changed from {oldValue} to {newValue}");
        }

        /// <summary>
        /// Logs when an object is retrieved or accessed.
        /// </summary>
        /// <param name="objectType">The type of object being retrieved.</param>
        /// <param name="objectId">The unique ID of the object.</param>
        /// <param name="objectName">The name of the object.</param>
        /// <param name="operation">The operation being performed (e.g., "Retrieved", "Accessed").</param>
        public void LogObjectRetrieved(string objectType, int objectId, string objectName, string operation = "Retrieved")
        {
            MyLogger.Instance.LogApplicationInfo($"[Object {operation}] {objectType}-{objectName} (ID: {objectId})");
        }

        /// <summary>
        /// Logs when an object is modified (equipment changes, etc.).
        /// </summary>
        /// <param name="objectType">The type of object being modified.</param>
        /// <param name="objectId">The unique ID of the object.</param>
        /// <param name="objectName">The name of the object.</param>
        /// <param name="modification">Description of the modification.</param>
        public void LogObjectModified(string objectType, int objectId, string objectName, string modification)
        {
            MyLogger.Instance.LogApplicationInfo($"[Object Modified] {objectType}-{objectName} (ID: {objectId}): {modification}");
        }

        /// <summary>
        /// Logs when an object is added to a collection or world.
        /// </summary>
        /// <param name="objectType">The type of object being added.</param>
        /// <param name="objectId">The unique ID of the object.</param>
        /// <param name="objectName">The name of the object.</param>
        /// <param name="collectionName">The name of the collection or world it's being added to.</param>
        public void LogObjectAdded(string objectType, int objectId, string objectName, string collectionName)
        {
            MyLogger.Instance.LogApplicationInfo($"[Object Added] {objectType}-{objectName} (ID: {objectId}) added to {collectionName}");
        }

        /// <summary>
        /// Logs when an object is removed from a collection or world.
        /// </summary>
        /// <param name="objectType">The type of object being removed.</param>
        /// <param name="objectId">The unique ID of the object.</param>
        /// <param name="objectName">The name of the object.</param>
        /// <param name="collectionName">The name of the collection or world it's being removed from.</param>
        public void LogObjectRemoved(string objectType, int objectId, string objectName, string collectionName)
        {
            MyLogger.Instance.LogApplicationInfo($"[Object Removed] {objectType}-{objectName} (ID: {objectId}) removed from {collectionName}");
        }
    }
}

