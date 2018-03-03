using Newtonsoft.Json;

namespace TestSketch.IO.Config
{
    /// <summary>
    /// Responsible for the serialization and deserialization of the
    /// configuration object
    /// </summary>
    internal static class ConfigurationSerializer
    {
        /// <summary>
        /// Deserializes some given JSON text into a configuration object
        /// </summary>
        /// <param name="jsonText">The JSON text to deserialize</param>
        /// <returns>The newly created configuration object</returns>
        public static Configuration Deserialize(string jsonText) =>
            JsonConvert.DeserializeObject<Configuration>(jsonText);

        /// <summary>
        /// Serializes a configuration object into some JSON text
        /// </summary>
        /// <param name="config">The configuration object to serialize</param>
        /// <returns>The JSON text that represents the configuration object</returns>
        public static string Serialize(Configuration config) =>
            JsonConvert.SerializeObject(config);

        private static JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };
    }
}