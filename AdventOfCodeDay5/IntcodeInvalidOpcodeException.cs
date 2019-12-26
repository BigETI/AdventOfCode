using System;

/// <summary>
/// Advent of Code - Day 5 namespace
/// </summary>
namespace AdventOfCodeDay5
{
    /// <summary>
    /// Intcode invalid opcode exception class
    /// </summary>
    public class IntcodeInvalidOpcodeException : Exception
    {
        /// <summary>
        /// Opcode
        /// </summary>
        public int Opcode { get; private set; }

        /// <summary>
        /// Address
        /// </summary>
        public uint Address { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="opcode">Opcode</param>
        /// <param name="address">Address</param>
        internal IntcodeInvalidOpcodeException(int opcode, uint address) : base("Invalid opcode " + opcode + " at address " + address)
        {
            Opcode = opcode;
            Address = address;
        }
    }
}
