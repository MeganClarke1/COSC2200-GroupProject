using Newtonsoft.Json.Linq;
using System.IO;

namespace Group2_COSC2200_Project.viewmodel
{
    public static class Theme
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\theme.json");

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetTheme()
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                JObject jsonObject = JObject.Parse(jsonContent);
                return jsonObject["Theme"].ToString();
            }
            catch (Exception ex)
            {
                return "classic"; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        public static void SetTheme(string theme)
        {
            try
            {
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
