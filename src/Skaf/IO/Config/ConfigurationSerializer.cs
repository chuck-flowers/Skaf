using System.IO;
using Newtonsoft.Json;

namespace Skaf.IO.Config
{
    /// <summary>
    /// Responsible for the serialization and deserialization of the configuration object
    /// </summary>
    internal static class ConfigurationSerializer
    {
        /// <summary>
        /// Deserializes some given JSON text into a configuration object
        /// </summary>
        /// <param name="jsonText">The JSON text to deserialize</param>
        /// <returns>The newly created configuration object</returns>
        public static Configuration Deserialize(string jsonText) =>
            JsonConvert.DeserializeObject<Configuration>(jsonText, settings);

        /// <summary>
        /// Loads a <see cref="Configuration"/> from the given path
        /// </summary>
        /// <param name="path">The path of the configuration file to load.</param>
        /// <returns>The configuration file stored at the path.</returns>
        public static Configuration Load(string path) => Deserialize(File.ReadAllText(path));

        /// <summary>
        /// Serializes a configuration object into some JSON text
        /// </summary>
        /// <param name="config">The configuration object to serialize</param>
        /// <returns>The JSON text that represents the configuration object</returns>
        public static string Serialize(Configuration config) =>
            JsonConvert.SerializeObject(config, settings);

        /// <summary>
        /// The serialization settings to use when serializing and deserializing a <see cref="Configuration"/>
        /// </summary>
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };
    }
}