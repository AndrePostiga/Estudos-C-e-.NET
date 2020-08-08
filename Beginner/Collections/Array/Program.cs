using System;

namespace Array
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\andre\Desktop\C# Pluralsight\Beginner\Collections\Array\Pop by Largest Final.csv";
            CsvReader reader = new CsvReader(filePath);

            Country[] countries = reader.ReadFirstNCountries(10);

            foreach (var country in countries)
            {
                Console.WriteLine($"{PopulationFormatter.FormatPopulation(country.Population).PadLeft(15)}: {country.Name}");
            }

            
        }
    }
}
