using System.Text.RegularExpressions;

namespace la_mia_pizzeria.Utility
{
    public static class Helper
    {
        public static List<string[]> GetCSVContent(string filePath, string separator)
        {
            if (separator.Trim() == "" || separator.Length != 1)
                throw new Exception("Splitter must be one char. Space is not allowed.");

            List<string[]> data = new List<string[]>();
            try
            {
                StreamReader file = File.OpenText(filePath);
                try
                {
                    while (!file.EndOfStream)
                    {
                        string? row = file.ReadLine();
                        if (row is not null)
                        {
                            string[] columns = row.Split(separator);
                            data.Add(columns);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Error while reading file.");
                }
                finally
                {
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error while opening file.");
            }
            return data;
        }

        public static string GetSlugFromString(string? s)
        {
            if (s is null)
                return "";
            else
            {
                //TODO function with REGEX
                string output = Regex.Replace(s, "[^A-Za-z0-9 ]", "");
                return Regex.Replace(output, @"\s+", "-").ToLower();
            }
        }
    }
}
