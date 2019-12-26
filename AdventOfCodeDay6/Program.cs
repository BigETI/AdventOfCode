using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// Advent of Code - Day 6 namespace
/// </summary>
namespace AdventOfCodeDay6
{
    /// <summary>
    /// Program class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Input regular expression
        /// </summary>
        private static readonly Regex inputRegex = new Regex(@"([0-9A-Za-z]+)\)([0-9A-Za-z]+)");

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static void Main(string[] args)
        {
            OrbitalMap orbital_map = new OrbitalMap();
            string line;
            List<Tuple<string, string>> orbits = new List<Tuple<string, string>>();
            foreach (string arg in args)
            {
                Match match = inputRegex.Match(arg);
                if (match.Success)
                {
                    if (match.Groups.Count > 2)
                    {
                        orbits.Add(new Tuple<string, string>(match.Groups[1].Value, match.Groups[2].Value));
                    }
                }
            }
            while ((line = Console.ReadLine()) != null)
            {
                Match match = inputRegex.Match(line);
                if (match.Success)
                {
                    if (match.Groups.Count > 2)
                    {
                        orbital_map.AddRelationship(match.Groups[1].Value, match.Groups[2].Value);
                    }
                }
            }
            if (orbits.Count > 0)
            {
                foreach (Tuple<string, string> orbit in orbits)
                {
                    Console.WriteLine(orbital_map.GetNumberOfOrbitalManeuvers(orbit.Item1, orbit.Item2));
                }
                orbits.Clear();
            }
            else
            {
                Console.WriteLine(orbital_map.NumberOfDirectAndIndirectOrbits);
            }
        }
    }
}
