namespace Mandatory2DGameFramework.Configraitons.ConfigReaders
{
    /// <summary>
    /// Singleton class that stores game configuration settings loaded from XML file.
    /// Provides centralized access to world size and difficulty level.
    /// </summary>
    public class GameConfig
    {
        private static GameConfig _instance = new GameConfig();

        /// <summary>
        /// Gets the singleton instance of GameConfig.
        /// </summary>
        public static GameConfig Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets or sets the maximum X coordinate of the game world.
        /// </summary>
        public int MaxX { get; set; }

        /// <summary>
        /// Gets or sets the maximum Y coordinate of the game world.
        /// </summary>
        public int MaxY { get; set; }

        /// <summary>
        /// Gets or sets the game difficulty level (beginner, normal, expert).
        /// </summary>
        public string DifficultyLevel { get; set; }

        private GameConfig()
        {
            MaxX = 0;
            MaxY = 0;
            DifficultyLevel = "N/A";
        }
    }
}
