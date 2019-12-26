using System;

/// <summary>
/// Advent of Code - Day 5 namespace
/// </summary>
namespace AdventOfCodeDay5
{
    /// <summary>
    /// Intcode invalid parameter mode exception
    /// </summary>
    public class IntcodeInvalidParameterModeException : Exception
    {
        /// <summary>
        /// Parameter mode
        /// </summary>
        public byte ParameterMode { get; private set; }

        /// <summary>
        /// Address
        /// </summary>
        public uint Address { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parameterMode">Opcode</param>
        /// <param name="address">Address</param>
        internal IntcodeInvalidParameterModeException(byte parameterMode, uint address) : base("Invalid parameter mode " + parameterMode + " at address " + address)
        {
            ParameterMode = parameterMode;
            Address = address;
        }
    }
}
