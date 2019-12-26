using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Advent of Code - Day 4 namespace
/// </summary>
namespace AdventOfCodeDay4
{
    /// <summary>
    /// Brut forcer class
    /// </summary>
    public class BrutForcer
    {
        /// <summary>
        /// Get solutions
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <param name="condition">Condition</param>
        /// <returns>Solutions</returns>
        public uint[] GetSolutions(uint left, uint right, BrutForceConditionDelegate condition)
        {
            uint[] ret = Array.Empty<uint>();
            if (condition != null)
            {
                ConcurrentBag<uint> concurrent_bag = new ConcurrentBag<uint>();
                uint min = Math.Min(left, right);
                uint max = Math.Max(left, right);
                Parallel.For(min, max + 1U, (i) =>
                {
                    if (condition((uint)i))
                    {
                        concurrent_bag.Add((uint)i);
                    }
                });
                List<uint> list = new List<uint>(concurrent_bag);
                concurrent_bag.Clear();
                list.Sort();
                ret = list.ToArray();
                list.Clear();
            }
            return ret;
        }
    }
}
