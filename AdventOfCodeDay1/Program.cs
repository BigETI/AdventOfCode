using System;

/// <summary>
/// Advent of Code - Day 1 namespace
/// </summary>
namespace AdventOfCodeDay1
{
    /// <summary>
    /// Program class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static void Main(string[] args)
        {
            string line;
            uint line_count = 0U;
            bool calculate_with_fuel_mass = false;
            foreach (string arg in args)
            {
                string trimmed_arg = arg.Trim().ToLower();
                if ((trimmed_arg == "-c") || (trimmed_arg == "--calculate-with-fuel-mass"))
                {
                    calculate_with_fuel_mass = true;
                    break;
                }
            }
            FuelCalculator fuel_calculator = new FuelCalculator(calculate_with_fuel_mass);
            while ((line = Console.ReadLine()) != null)
            {
                int mass;
                ++line_count;
                if (int.TryParse(line, out mass))
                {
                    fuel_calculator.Add(mass);
                }
                else
                {
                    Console.Error.WriteLine("Can't parse line " + line_count + ": \"" + line + "\" is not an integer.");
                }
            }
            Console.WriteLine(fuel_calculator.Fuel);
        }
    }
}
