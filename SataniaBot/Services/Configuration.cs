﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Drawing;
using Console = Colorful.Console;

namespace SataniaBot.Services
{
    /// <summary> 
    /// A file that contains information you either don't want public
    /// or will want to change without having to compile another bot.
    /// </summary>
    public class Configuration
    {
        [JsonIgnore]
        /// <summary> The location and name of your bot's configuration file. </summary>
        public static string FileName { get; private set; } = "config/configuration.json";
        /// <summary> Ids of users who will have owner access to the bot. </summary>
        public ulong[] Owners { get; set; }
        /// <summary> Your bot's command prefix. </summary>
        public string Prefix { get; set; } = "run ";
        /// <summary> Your bot's login token. </summary>
        public string Token { get; set; } = "";
        /// <summary> Api key for using cleverbot. </summary>
        public string CleverbotApi { get; set; } = "";
        /// <summary> Api key for using cleverbot. </summary>
        public bool EnableCleverbot { get; set; } = false;

        /// <summary> Host adress database is hosted on </summary>
        public string DatabaseHost { get; set; } = "";
        /// <summary> Username for database </summary>
        public string DatabaseUsername { get; set; } = "";
        /// <summary> Password for database </summary>
        public string DatabasePassword { get; set; } = "";
        /// <summary> Name for the database hosted </summary>
        public string DatabaseName { get; set; } = "";

        public static void EnsureExists()
        {
            string file = Path.Combine(AppContext.BaseDirectory, FileName);
            if (!File.Exists(file))                                 // Check if the configuration file exists.
            {
                string path = Path.GetDirectoryName(file);          // Create config directory if doesn't exist.
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var config = new Configuration();                   // Create a new configuration object.

                Console.WriteLine("Please enter your token: ", System.Drawing.Color.LawnGreen);
                string token = Console.ReadLine();                  // Read the bot token from console.

                config.Token = token;
                config.SaveJson();                                  // Save the new configuration object to file.
            }
            Console.WriteLine("Configuration Loaded", System.Drawing.Color.LawnGreen);
        }

        /// <summary> Save the configuration to the path specified in FileName. </summary>
        public void SaveJson()
        {
            string file = Path.Combine(AppContext.BaseDirectory, FileName);
            File.WriteAllText(file, ToJson());
        }

        /// <summary> Load the configuration from the path specified in FileName. </summary>
        public static Configuration Load()
        {
            string file = Path.Combine(AppContext.BaseDirectory, FileName);
            return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(file));
        }

        /// <summary> Convert the configuration to a json string. </summary>
        public string ToJson()
            => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}