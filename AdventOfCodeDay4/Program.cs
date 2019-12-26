using System;
using System.Text.RegularExpressions;

/// <summary>
/// Advent of Code - Day 4 namespace
/// </summary>
namespace AdventOfCodeDay4
{
    /// <summary>
    /// Program claass
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Input regular expression
        /// </summary>
        private static readonly Regex inputRegex = new Regex(@"([1-9][0-9]+)-([1-9][0-9]+)");

        /// <summary>
        /// Validate brut force rule
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>"true" if number matches with brut force rule, otherwise "false"</returns>
        private static bool ValidateBrutForceRule(uint number)
        {
            bool ret = false;
            string number_string = number.ToString();
            char last_digit = '0';
            if (number_string.Length == 6)
            {
                foreach (char digit in number_string)
                {
                    if (digit < last_digit)
                    {
                        ret = false;
                        break;
                    }
                    else
                    {
                        if (digit == last_digit)
                        {
                            ret = true;
                        }
                        else
                        {
                            last_digit = digit;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Validate stricter brut force rule
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>"true" if number matches with brut force rule, otherwise "false"</returns>
        private static bool ValidateStricterBrutForceRule(uint number)
        {
            bool ret = false;
            string number_string = number.ToString();
            char last_digit = '0';
            uint adjacent_matching_digits_count = 0U;
            bool has_adjacent_matching_digits_pair = false;
            if (number_string.Length == 6)
            {
                foreach (char digit in number_string)
                {
                    if (digit < last_digit)
                    {
                        has_adjacent_matching_digits_pair = false;
                        ret = false;
                        break;
                    }
                    else
                    {
                        if (digit == last_digit)
                        {
                            ++adjacent_matching_digits_count;
                            if (adjacent_matching_digits_count >= 2U)
                            {
                                has_adjacent_matching_digits_pair = (adjacent_matching_digits_count == 2U);
                            }
                        }
                        else
                        {
                            adjacent_matching_digits_count = 1U;
                            last_digit = digit;
                            if (has_adjacent_matching_digits_pair)
                            {
                                ret = true;
                            }
                        }
                    }
                }
                if (has_adjacent_matching_digits_pair)
                {
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static void Main(string[] args)
        {
            bool use_stricter_rule = false;
            BrutForcer brut_forcer = new BrutForcer();
            foreach (string arg in args)
            {
                string trimmed_arg = arg.Trim().ToLower();
                if ((trimmed_arg == "-u") || (trimmed_arg == "--use-stricter-rule"))
                {
                    use_stricter_rule = true;
                }
                else
                {
                    Match match = inputRegex.Match(arg);
                    if (match.Success)
                    {
                        if (match.Groups.Count > 2)
                        {
                            uint left;
                            uint right;
                            if (uint.TryParse(match.Groups[1].Value, out left) && uint.TryParse(match.Groups[2].Value, out right))
                            {
                                uint[] solutions = brut_forcer.GetSolutions(left, right, use_stricter_rule ? (BrutForceConditionDelegate)ValidateStricterBrutForceRule : ValidateBrutForceRule);
                                Console.WriteLine(solutions.Length);
                            }
                            else
                            {
                                Console.Error.WriteLine("Input contains incompatible input.");
                            }
                        }
                    }
                }
            }
        }
    }
}
