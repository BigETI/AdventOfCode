using System;
using System.IO;

/// <summary>
/// Advent of Code - Day 5 namespace
/// </summary>
namespace AdventOfCodeDay5
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
            foreach (string arg in args)
            {
                try
                {
                    if (File.Exists(arg))
                    {
                        using (FileStream file_stream = File.OpenRead(arg))
                        {
                            using (StreamReader reader = new StreamReader(file_stream))
                            {
                                string[] intcode_strings = reader.ReadToEnd().Trim().Split(',');
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
                                        try
                                        {
                                            Intcode intcode_vm = new Intcode(intcode);
                                            intcode_vm.Execute();
                                            intcode_vm.Clear();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.Error.WriteLine(e);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }
    }
}
