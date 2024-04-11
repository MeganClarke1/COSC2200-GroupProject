using Newtonsoft.Json.Linq;
using System.IO;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// Helper class that provides functionality for managing and applying themes within the application. This class 
    /// includes methods to get the current theme and set a new theme by reading from and writing to a configuration file.
    /// </summary>
    public static class Theme
    {
        /// <summary>
        /// Path to the theme configuration file.
        /// </summary>
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\theme.json");

        /// <summary>
        /// Retrieves the currently set theme by reading the theme configuration file. If an error occurs, 
        /// the "classic" theme is returned.
        /// </summary>
        /// <returns>The name of the current theme as a string.</returns>
        public static string GetTheme()
        {
            try
            {
                // Read the theme configuration file and parse the theme.
                string jsonContent = File.ReadAllText(filePath);
                JObject jsonObject = JObject.Parse(jsonContent);
                return jsonObject["Theme"].ToString();
            }
            catch (Exception ex)
            {
                // Return a "classic" theme in case of any errors.
                return "classic"; 
            }
        }

        /// <summary>
        /// Sets the application theme by writing the specified theme name to the theme configuration file.
        /// </summary>
        /// <param name="theme">The name of the theme to set.</param>
        public static void SetTheme(string theme)
        {
            try
            {
                // Create a JSON object for the new theme and write it to the configuration file.
                JObject themeObject = new JObject
            {
                { "Theme", theme }
            };

                string newTheme = themeObject.ToString();
                File.WriteAllText(filePath, newTheme);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
