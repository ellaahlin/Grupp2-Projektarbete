using System.Text.Json; // Använder .NET's egna sätt att hantera JSON.
using System.Text.Json.Serialization;

namespace Grupp2Projektarbete
{
    internal class Program
    {
        static string path = "Data.json"; // Se till att ha filen i samma map som programmet
        static void Main(string[] args)
        {
            FileStream? fs = null;

            try
            {
                fs = File.OpenRead(path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Kunde inte hitta Data.json! Se till att lägga den i samma mapp som programmet");
                Console.ReadKey(); return;
            }

            try
            {
                StreamReader reader = new StreamReader(fs);
                string data = reader.ReadToEnd();
                int[] source = JsonSerializer.Deserialize<int[]>(data);
                Console.WriteLine(Statistics.DescriptiveStatistics(source));
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey(); return;
            }

        }
    }
}