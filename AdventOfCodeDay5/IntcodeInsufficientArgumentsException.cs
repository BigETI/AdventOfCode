using System;

/// <summary>
/// Advent of Code - Day 5 namespace
/// </summary>
namespace AdventOfCodeDay5
{
    /// <summary>
    /// Intcode insufficient argument exception class
    /// </summary>
    public class IntcodeInsufficientArgumentsException : Exception
    {
        /// <summary>
        /// Number of arguments
        /// </summary>
        public uint NumArguments { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numArguments">Number of arguments</param>
        internal IntcodeInsufficientArgumentsException(uint numArguments) : base("Insufficient number of arguments. (" + numArguments + ")")
        {
            NumArguments = numArguments;
        }
    }
}
