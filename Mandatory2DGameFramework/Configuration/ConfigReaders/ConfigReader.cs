using Mandatory2DGameFramework.Domain.Logging.Infrastructure;
using System.Xml;

namespace Mandatory2DGameFramework.Configuration.ConfigReaders
{
    public static class ConfigReader
    {
        /// <summary>
        /// Reads game configuration from an XML file and populates the GameConfig singleton.
        /// Expected XML structure:
        /// <GameConfig>
        ///   <MaxX>10</MaxX>
        ///   <MaxY>10</MaxY>
        ///   <DifficultyLevel>normal</DifficultyLevel>
        /// </GameConfig>
        /// </summary>
        /// <param name="fullFileName">Full path to the XML configuration file</param>
        /// <returns>The populated GameConfig instance</returns>
        public static GameConfig ReadGameConfig(string fullFileName)
        {
            GameConfig config = GameConfig.Instance;
            MyLogger logger = MyLogger.Instance;

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fullFileName);

                XmlNode? maxXNode = xmlDocument.DocumentElement?.SelectSingleNode("MaxX");
                if (maxXNode != null)
                {
                    config.MaxX = Convert.ToInt32(maxXNode.InnerText.Trim());
                    logger.LogInfo($"Loaded MaxX: {config.MaxX}");
                }

                XmlNode? maxYNode = xmlDocument.DocumentElement?.SelectSingleNode("MaxY");
                if (maxYNode != null)
                {
                    config.MaxY = Convert.ToInt32(maxYNode.InnerText.Trim());
                    logger.LogInfo($"Loaded MaxY: {config.MaxY}");
                }

                XmlNode? difficultyNode = xmlDocument.DocumentElement?.SelectSingleNode("DifficultyLevel");
                if (difficultyNode != null)
                {
                    config.DifficultyLevel = difficultyNode.InnerText.Trim();
                    logger.LogInfo($"Loaded DifficultyLevel: {config.DifficultyLevel}");
                }

                return config;
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to read game configuration: {ex.Message}");
                return config;
            }
        }
    }
}
