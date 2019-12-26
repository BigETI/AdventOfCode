using System;

/// <summary>
/// Advent of Code - Day 5 namespace
/// </summary>
namespace AdventOfCodeDay5
{
    /// <summary>
    /// Intcode invalid address exception class
    /// </summary>
    public class IntcodeInvalidAddressException : Exception
    {
        /// <summary>
        /// Address
        /// </summary>
        public int Address { get; private set; }

        /// <summary>
        /// At address
        /// </summary>
        public uint AtAddress { get; private set; }

        /// <summary>
        /// Cobnstructor
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="atAddress">At address</param>
        internal IntcodeInvalidAddressException(int address, uint atAddress) : base("Invalid address " + address + " at address" + atAddress)
        {
            Address = address;
            AtAddress = atAddress;
        }
    }
}
