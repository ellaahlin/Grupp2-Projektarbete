using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Grupp2Projektarbete
{
    public static class Statistics
    {
        // Det räcker med en public metod som hanterar all data med hjälp av privata metoder.
        // Då har man enkapsulerat statistik utav datan till en enda metod.
        public static dynamic DescriptiveStatistics(int[] source)
        {
            // Felhantering
            if (source == null)
            {
                throw new ArgumentNullException("Source is null");
            }
            else if (source.Length == 0)
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            // Returnerar all data i form utav en string.
            return
            $"Maximum : {Maximum(source)}\n" +
            $"Minimum : {Minimum(source)}\n" +
            $"Medelvärde : {Mean(source)} \n" +
            $"Median: {Median(source)}\n" +
            $"Typvärde : {Mode(source)}\n" +
            $"Variationsbredd : {Range(source)}\n" +
            $"Standardavvikelse : {StandardDeviation(source)}";
        }

        private static int Maximum(int[] source)
        {
            return source.Max();
        }

        private static double Mean(int[] source)
        {
            return Convert.ToDouble((source.Sum() / source.Length));
        }

        private static double Median(int[] source)
        {
            int[] sortedArray = (int[])source.Clone(); // Kopiera arrayn. Vi vill inte sortera den orginella.
            Array.Sort(sortedArray);

            int size = sortedArray.Length;
            int mid = size / 2;

            // Om arrayn har ett ojämt antal med element finns det en mittpunkt.
            if (size % 2 != 0)
                return Convert.ToDouble(sortedArray[mid]);

            // Annars är medianen mitten plus närliggande element / 2
            return Convert.ToDouble((sortedArray[mid] + sortedArray[mid - 1]) / 2);
        }

        private static int Minimum(int[] source)
        {
            return source.Min();
        }

       private static int Mode(int[] source)
        {
            int mode = source.GroupBy(v => v)
            .OrderByDescending(g => g.Count())
            .First()
            .Key;
            return mode;
        }

        private static int Range(int[] source)
        {
            return (source.Max() - source.Min());
        }

        private static double StandardDeviation(int[] source)
        {
            // 1. Räkna ut medelvärdet
            double medelVärde = Mean(source);

            // 2. beräkna hur mycket varje enskilt observationsvärde avviker från medelvärdet
            List<double> list = new List<double>();
            foreach (int i in source)
                list.Add(i - medelVärde);

            // 3. Kvadrera var och en av dessa avvikelser från medelvärdet
            for (int i = 0; i < list.Count; i++)
                list[i] = Math.Pow(list[i], 2);

            // 4. Summera samtliga kvadrerade avvikelser
            // 5. Dividera denna summa med antalet observationsvärden
            // 6. Beräkna roten ur vår genomsnittliga kvadrerade avvikelse.
            return Math.Sqrt(list.Sum() / list.Count);
        }

    }
}
