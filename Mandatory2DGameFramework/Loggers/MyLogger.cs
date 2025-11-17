using System;
using System.Diagnostics;
using System.IO;

namespace Mandatory2DGameFramework.Loggers
{
    /// <summary>
    /// TraceSource based Logger supporting multiple listeners: Console, Textfile, XML file, EventLog. Singleton pattern.
    /// Provides separate logging channels for combat and application events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// MyLogger uses separate TraceSource instances for different log types:
    /// <list type="bullet">
    /// <item><description>General logging (console, XML, EventLog)</description></item>
    /// <item><description>Combat logging (Combatlog.txt)</description></item>
    /// <item><description>Application logging (ApplicationLog.txt)</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Log files are written to GameFrameworkConsoleApp\LogInfo\ by default,
    /// or to the path specified in the LOG_DIRECTORY environment variable.
    /// </para>
    /// </remarks>
    public class MyLogger
    {
        private static MyLogger _instance = new MyLogger();
        
        /// <summary>
        /// Gets the singleton instance of MyLogger.
        /// </summary>
        /// <value>The single instance of MyLogger.</value>
        public static MyLogger Instance
        {
            get { return _instance; }
        }

        private TraceSource _traceSource;
        private TraceSource _combatTraceSource;
        private TraceSource _applicationTraceSource;
        private TraceListener _consoleListener;
        private TraceListener _combatLogListener;
        private TraceListener _xmlFileListener;
        private TraceListener _eventLogListener;
        private TraceListener _applicationLogListener;

        private MyLogger()
        {
            _traceSource = new TraceSource("Trace Source", SourceLevels.All);
            _traceSource.Switch = new SourceSwitch("Source Swtch", SourceLevels.All.ToString());
            
            // Separate TraceSource for combat logging
            _combatTraceSource = new TraceSource("Combat Trace Source", SourceLevels.All);
            _combatTraceSource.Switch = new SourceSwitch("Combat Source Switch", SourceLevels.All.ToString());
            
            // Separate TraceSource for application logging
            _applicationTraceSource = new TraceSource("Application Trace Source", SourceLevels.All);
            _applicationTraceSource.Switch = new SourceSwitch("Application Source Switch", SourceLevels.All.ToString());
        }

        #region Setup listeners and levels
        /// <summary>
        /// Adds a trace listener to the main trace source.
        /// </summary>
        /// <param name="listener">The trace listener to add.</param>
        /// <remarks>
        /// The listener will receive all general log messages (Info, Warning, Error, Critical).
        /// </remarks>
        public void AddListener(TraceListener listener)
        {
            _traceSource.Listeners.Add(listener);
        }
        
        /// <summary>
        /// Removes a trace listener from the main trace source.
        /// </summary>
        /// <param name="listener">The trace listener to remove.</param>
        public void RemoveListener(TraceListener listener)
        {
            _traceSource.Listeners.Remove(listener);
        }
        
        /// <summary>
        /// Sets the default trace level for the main trace source.
        /// </summary>
        /// <param name="level">The source level to set (e.g., All, Information, Warning, Error, Off).</param>
        /// <remarks>
        /// Controls which trace events are processed by the main trace source listeners.
        /// </remarks>
        public void SetDefaultLevel(SourceLevels level)
        {
            _traceSource.Switch.Level = level;
        }
        
        /// <summary>
        /// Stops all logging and closes all trace sources.
        /// Flushes all listeners before closing to ensure all log data is written.
        /// </summary>
        /// <remarks>
        /// Should be called when the application is shutting down to ensure all log data is persisted.
        /// </remarks>
        public void Stop()
        {
            // Flush all listeners before closing
            if (_combatLogListener != null)
            {
                _combatLogListener.Flush();
            }
            if (_applicationLogListener != null)
            {
                _applicationLogListener.Flush();
            }
            
            _traceSource.Close();
            _combatTraceSource.Close();
            _applicationTraceSource.Close();
        }

        /// <summary>
        /// Gets the log directory path from environment variable or uses default.
        /// Checks for LOG_DIRECTORY environment variable first, then falls back to default path.
        /// </summary>
        /// <returns>The log directory path.</returns>
        private string GetLogDirectory()
        {
            // Check for environment variable first
            string? envLogDir = Environment.GetEnvironmentVariable("LOG_DIRECTORY");
            if (!string.IsNullOrEmpty(envLogDir))
            {
                return Path.GetFullPath(envLogDir);
            }

            // Navigate up from bin/Debug/net8.0 or bin/Release/net8.0
            // Structure: GameFrameworkConsoleApp\bin\Debug\net8.0
            // We need to go up 3 levels to reach GameFrameworkConsoleApp
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo? currentDir = new DirectoryInfo(baseDir);
            
            // Go up 3 levels: net8.0 -> Debug/Release -> bin -> GameFrameworkConsoleApp
            for (int i = 0; i < 3 && currentDir != null; i++)
            {
                currentDir = currentDir.Parent;
            }
            
            if (currentDir is null)
            {
                // Fallback: use base directory if we can't navigate up
                return Path.Combine(baseDir, "LogInfo");
            }
            
            // Verify we're at the right level by checking if the directory name is GameFrameworkConsoleApp
            // If not, we might need to adjust
            string projectRoot = currentDir.FullName;
            
            // If the current directory is already GameFrameworkConsoleApp, just add LogInfo
            // If it's the solution root, we need to add GameFrameworkConsoleApp\LogInfo
            if (currentDir.Name.Equals("GameFrameworkConsoleApp", StringComparison.OrdinalIgnoreCase))
            {
                // We're at GameFrameworkConsoleApp, just add LogInfo
                return Path.Combine(projectRoot, "LogInfo");
            }
            else
            {
                // We're probably at solution root, add GameFrameworkConsoleApp\LogInfo
                return Path.Combine(projectRoot, "GameFrameworkConsoleApp", "LogInfo");
            }
        }
        #endregion

        #region Console Logger - Start/Stop
        /// <summary>
        /// Starts console logging. Logs will be written to the console output.
        /// </summary>
        /// <remarks>
        /// If console logging is already started, this method does nothing.
        /// Console logging writes to the standard output stream.
        /// </remarks>
        public void StartConsoleLogger()
        {
            if(_consoleListener == null)
            {
                _consoleListener = new ConsoleTraceListener();
                _traceSource.Listeners.Add(_consoleListener);
            }
        }
        
        /// <summary>
        /// Stops console logging and removes the console listener.
        /// </summary>
        /// <remarks>
        /// If console logging is not active, this method does nothing.
        /// </remarks>
        public void StopConsoleLogger()
        {
            if(_consoleListener != null)
            {
                _traceSource.Listeners.Remove(_consoleListener);
                _consoleListener = null;
            }
        }
        #endregion

        #region Combat log file logger - Start/Stop
        /// <summary>
        /// Starts combat log file logging. Combat events will be written to Combatlog.txt.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If combat logging is already started, this method does nothing.
        /// The log file is created in the directory specified by GetLogDirectory().
        /// The directory is created automatically if it doesn't exist.
        /// </para>
        /// <para>
        /// Combat logs are written to a separate TraceSource, ensuring they don't mix with application logs.
        /// </para>
        /// </remarks>
        public void StartCombatLog()
        {
            if(_combatLogListener == null)
            {
                string logDirectory = GetLogDirectory();
                string logFilePath = Path.Combine(logDirectory, "Combatlog.txt");
                
                // Ensure directory exists
                Directory.CreateDirectory(logDirectory);
                
                // Use absolute path to ensure correct location
                string absolutePath = Path.GetFullPath(logFilePath);
                
                // Debug: Write path to console (can be removed later)
                System.Diagnostics.Debug.WriteLine($"Combat log path: {absolutePath}");
                Console.WriteLine($"Combat log path: {absolutePath}");
                
                _combatLogListener = new TextWriterTraceListener(absolutePath);
                _combatLogListener.Flush();
                _combatTraceSource.Listeners.Add(_combatLogListener);
            }
        }
        
        /// <summary>
        /// Stops combat log file logging and removes the combat log listener.
        /// </summary>
        /// <remarks>
        /// If combat logging is not active, this method does nothing.
        /// </remarks>
        public void StopCombatLog()
        {
            if(_combatLogListener != null)
            {
                _combatTraceSource.Listeners.Remove(_combatLogListener);
                _combatLogListener = null;
            }
        }
        #endregion

        #region Application log file logger - Start/Stop
        /// <summary>
        /// Starts application log file logging. Application events will be written to ApplicationLog.txt.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If application logging is already started, this method does nothing.
        /// The log file is created in the directory specified by GetLogDirectory().
        /// The directory is created automatically if it doesn't exist.
        /// </para>
        /// <para>
        /// Application logs are written to a separate TraceSource, ensuring they don't mix with combat logs.
        /// </para>
        /// </remarks>
        public void StartApplicationLog()
        {
            if(_applicationLogListener == null)
            {
                string logDirectory = GetLogDirectory();
                string logFilePath = Path.Combine(logDirectory, "ApplicationLog.txt");
                
                // Ensure directory exists
                Directory.CreateDirectory(logDirectory);
                
                // Use absolute path to ensure correct location
                string absolutePath = Path.GetFullPath(logFilePath);
                
                // Debug: Write path to console (can be removed later)
                System.Diagnostics.Debug.WriteLine($"Application log path: {absolutePath}");
                Console.WriteLine($"Application log path: {absolutePath}");
                
                _applicationLogListener = new TextWriterTraceListener(absolutePath);
                _applicationLogListener.Flush();
                _applicationTraceSource.Listeners.Add(_applicationLogListener);
            }
        }
        
        /// <summary>
        /// Stops application log file logging and removes the application log listener.
        /// </summary>
        /// <remarks>
        /// If application logging is not active, this method does nothing.
        /// </remarks>
        public void StopApplicationLog()
        {
            if(_applicationLogListener != null)
            {
                _applicationTraceSource.Listeners.Remove(_applicationLogListener);
                _applicationLogListener = null;
            }
        }
        #endregion

        #region XML Logger - Start/Stop
        /// <summary>
        /// Starts XML file logging. Logs will be written to XmlFileLog.xml in XML format.
        /// </summary>
        /// <remarks>
        /// If XML logging is already started, this method does nothing.
        /// The XML file is created in the application's base directory.
        /// </remarks>
        public void StartXmlLogger()
        {
            if(_xmlFileListener == null)
            {
                _xmlFileListener = new XmlWriterTraceListener("XmlFileLog.xml");
                _traceSource.Listeners.Add(_xmlFileListener);
            }
        }
        
        /// <summary>
        /// Stops XML file logging and removes the XML file listener.
        /// </summary>
        /// <remarks>
        /// If XML logging is not active, this method does nothing.
        /// </remarks>
        public void StopXmlLogger()
        {
            if(_xmlFileListener != null)
            {
                _traceSource.Listeners.Remove(_xmlFileListener);
                _xmlFileListener = null;
            }
        }
        #endregion

        #region EventLog Logger - Start/Stop
        /// <summary>
        /// Starts Windows Event Log logging. Logs will be written to the Windows Event Log.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If Event Log logging is already started, this method does nothing.
        /// This feature is only available on Windows platforms.
        /// </para>
        /// <para>
        /// Logs are written to the "Application" event log source.
        /// </para>
        /// </remarks>
        public void StartEventLogLogger()
        {
            if(_eventLogListener == null)
            {
                _eventLogListener = new EventLogTraceListener("Application");
                _traceSource.Listeners.Add(_eventLogListener);
            }
        }
        
        /// <summary>
        /// Stops Windows Event Log logging and removes the Event Log listener.
        /// </summary>
        /// <remarks>
        /// If Event Log logging is not active, this method does nothing.
        /// </remarks>
        public void StopEventLogLogger()
        {
            if(_eventLogListener != null)
            {
                _traceSource.Listeners.Remove(_eventLogListener);
                _eventLogListener = null;
            }
        }
        #endregion

        #region Logging Methods
        /// <summary>
        /// Logs an informational message to the main trace source.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        /// <remarks>
        /// This message will be sent to all listeners attached to the main trace source
        /// (console, XML, EventLog, etc.), but not to combat or application log files.
        /// </remarks>
        public void LogInfo(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Information, 2, message);
        }

        /// <summary>
        /// Logs a warning message to the main trace source.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        /// <remarks>
        /// This message will be sent to all listeners attached to the main trace source
        /// (console, XML, EventLog, etc.), but not to combat or application log files.
        /// </remarks>
        public void LogWarning(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Warning, 2, message);
        }

        /// <summary>
        /// Logs an error message to the main trace source.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <remarks>
        /// This message will be sent to all listeners attached to the main trace source
        /// (console, XML, EventLog, etc.), but not to combat or application log files.
        /// </remarks>
        public void LogError(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Error, 2, message);
        }

        /// <summary>
        /// Logs a critical error message to the main trace source.
        /// </summary>
        /// <param name="message">The critical error message to log.</param>
        /// <remarks>
        /// This message will be sent to all listeners attached to the main trace source
        /// (console, XML, EventLog, etc.), but not to combat or application log files.
        /// </remarks>
        public void LogCritical(string message)
        {
            _traceSource.TraceEvent(TraceEventType.Critical, 2, message);
        }

        /// <summary>
        /// Logs combat information to Combatlog.txt only.
        /// </summary>
        /// <param name="message">The combat message to log.</param>
        public void LogCombatInfo(string message)
        {
            _combatTraceSource.TraceEvent(TraceEventType.Information, 2, message);
            _combatTraceSource.Flush();
        }

        /// <summary>
        /// Logs application information to ApplicationLog.txt only.
        /// </summary>
        /// <param name="message">The application message to log.</param>
        public void LogApplicationInfo(string message)
        {
            _applicationTraceSource.TraceEvent(TraceEventType.Information, 2, message);
            _applicationTraceSource.Flush();
        }
        #endregion

    }
}
