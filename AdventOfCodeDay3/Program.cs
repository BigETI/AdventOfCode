using System;
using System.Text.RegularExpressions;

/// <summary>
/// Advent of Code - Day 3 namespace
/// </summary>
namespace AdventOfCodeDay3
{
    /// <summary>
    /// Program class
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Move regular expression
        /// </summary>
        private static readonly Regex moveRegex = new Regex(@"([UDLR])([1-9][0-9]*)");

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            bool find_fewest_combined_steps_to_intersection = false;
            string line;
            CrossedWires crossed_wires = new CrossedWires();
            bool success = true;
            bool first = true;
            foreach (string arg in args)
            {
                string trimmed_arg = arg.Trim().ToLower();
                if ((trimmed_arg == "-f") || (trimmed_arg == "--find-fewest-combined-steps-to-intersection"))
                {
                    find_fewest_combined_steps_to_intersection = true;
                    break;
                }
            }
            while ((line = Console.ReadLine()) != null)
            {
                string[] move_strings = line.Split(',');
                if (first)
                {
                    first = false;
                }
                else
                {
                    crossed_wires.AddNewWire();
                }
                foreach (string move_string in move_strings)
                {
                    Match match = moveRegex.Match(move_string);
                    if (match.Success)
                    {
                        if (match.Groups.Count == 3)
                        {
                            char direction = match.Groups[1].Value[0];
                            uint steps;
                            if (uint.TryParse(match.Groups[2].Value, out steps))
                            {
                                switch (direction)
                                {
                                    case 'U':
                                        crossed_wires.Move(EGridDirection.Up, steps);
                                        break;
                                    case 'D':
                                        crossed_wires.Move(EGridDirection.Down, steps);
                                        break;
                                    case 'L':
                                        crossed_wires.Move(EGridDirection.Left, steps);
                                        break;
                                    case 'R':
                                        crossed_wires.Move(EGridDirection.Right, steps);
                                        break;
                                }
                            }
                            else
                            {
                                success = false;
                                Console.Error.WriteLine(match.Groups[2].Value + " is not an unsigned integer");
                                break;
                            }
                        }
                    }
                }
            }
            if (success)
            {
                Console.WriteLine(find_fewest_combined_steps_to_intersection ? crossed_wires.FewestCombinedStepsToIntersection : crossed_wires.ManhattanDistanceToClosestIntersection);
            }
        }
    }
}
