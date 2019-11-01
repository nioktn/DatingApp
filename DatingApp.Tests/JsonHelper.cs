using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace DatingApp.Tests
{

    public static class JsonHelper
    {
        /// <summary>
        /// Serializes object into JSON string without null values
        /// </summary>
        /// <typeparam name="T">Type that will be serialized</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns></returns>
        public static string SerializeHandlingNullValues<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        /// <summary>
        /// Parses JSON file into given type
        /// Requires JSON file location in current current project starting from folder
        /// e.g. Utility/Configs/some.json
        /// </summary>
        /// <typeparam name="T">Type to be deserialized into</typeparam>
        /// <param name="jsonLocation">JSON file location</param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string jsonLocation)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(
                    File.ReadAllText(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                        throw new InvalidOperationException(),
                        jsonLocation)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Tries to parse JSON string into given type
        /// Returns null in case there was a deserialization exception
        /// </summary>
        /// <typeparam name="T">Type to be deserialized into</typeparam>
        /// <param name="json">JSON string</param>
        /// <param name="result">Resulting out parameter of parsed JSON</param>
        /// <returns>Bool value whether or not JSON can be parsed</returns>
        public static bool TryParseJson<T>(string json, out T result)
        {
            result = default(T);
            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch (JsonSerializationException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}