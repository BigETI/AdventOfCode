using System;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Advent of Code - Day 2 namespace
/// </summary>
namespace AdventOfCodeDay2
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
            bool find_input_pair = false;
            foreach (string arg in args)
            {
                string trimmed_arg = arg.Trim().ToLower();
                if ((trimmed_arg == "-f") || (trimmed_arg == "--find-noun-verb-pair"))
                {
                    find_input_pair = true;
                    break;
                }
            }
            StringBuilder input_builder = new StringBuilder();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                input_builder.Append(line);
            }
            string[] intcode_strings = input_builder.ToString().Trim().Split(',');
            if (intcode_strings != null)
            {
                int[] intcode = new int[intcode_strings.Length];
                bool success = true;
                for (int i = 0; i < intcode_strings.Length; i++)
                {
                    if (!(int.TryParse(intcode_strings[i], out intcode[i])))
                    {
                        success = false;
                        Console.Error.WriteLine("Failed to parse string at address " + i + ". \"" + intcode_strings[i] + "\" is not an integer.");
                        break;
                    }
                }
                if (success)
                {
                    if (find_input_pair)
                    {
                        Parallel.For(0, 100, (noun) =>
                        {
                            for (int verb = 0; verb < 100; verb++)
                            {
                                if (GetIntcodeResult(intcode, noun, verb) == 19690720)
                                {
                                    Console.WriteLine(noun.ToString() + verb);
                                }
                            }
                        });
                    }
                    else
                    {
                        Console.WriteLine(GetIntcodeResult(intcode, 12, 2));
                    }

                }
            }
        }

        /// <summary>
        /// Get intcode result
        /// </summary>
        /// <param name="intcode">Intcode</param>
        /// <param name="noun">Noun</param>
        /// <param name="verb">Verb</param>
        /// <returns>Value from index 0</returns>
        private static int GetIntcodeResult(int[] intcode, int noun, int verb)
        {
            int ret = 0;
            try
            {
                Intcode intcode_vm = new Intcode(intcode);
                intcode_vm[1] = noun;
                intcode_vm[2] = verb;
                intcode_vm.Execute();
                ret = intcode_vm[0];
                intcode_vm.Clear();
            }
            catch (IntcodeInsufficientArgumentsException e)
            {
                Console.Error.WriteLine(e);
            }
            catch (IntcodeInvalidOpcodeException e)
            {
                Console.Error.WriteLine(e);
            }
            catch (IntcodeInvalidAddressException e)
            {
                Console.Error.WriteLine(e);
            }
            return ret;
        }
    }
}
