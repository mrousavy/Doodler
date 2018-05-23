using Newtonsoft.Json;
using System;
using System.IO;

namespace DoodlerTests
{
    public class Preferences
    {
        public string Database { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }

        // TODO: BAD RAW PASSWORD STORING
        public string Password { get; set; }

        public string LastEmail { get; set; }
        public string LastPassword { get; set; }

        public static string ConfigDir { get; } =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Doodler");

        public string LogFile { get; set; } = Path.Combine(ConfigDir, "logfile.log");

        private static string ConfigPath { get; } = Path.Combine(ConfigDir, "config.json");

        /// <summary>
        ///     Serialize and save the given preferences to a file
        /// </summary>
        /// <param name="preferences">The preferences object to save</param>
        /// <param name="filename">(Optional) Another filename</param>
        public static void Save(Preferences preferences, string filename = null)
        {
            filename = filename ?? ConfigPath;
            if (!Directory.Exists(ConfigDir))
                Directory.CreateDirectory(ConfigDir);

            string json = JsonConvert.SerializeObject(preferences);
            File.WriteAllText(filename, json);
        }

        /// <summary>
        ///     Load and deserialize the preferences from file
        /// </summary>
        /// <param name="filename">(Optional) Another filename</param>
        /// <returns>The deserialized preferences (or default ctor if not found)</returns>
        public static Preferences Load(string filename = null)
        {
            filename = filename ?? ConfigPath;

            if (File.Exists(filename))
            {
                string content = File.ReadAllText(filename);
                return JsonConvert.DeserializeObject<Preferences>(content);
            }

            var preferences = new Preferences();
            Save(preferences);
            return preferences;
        }
    }
}