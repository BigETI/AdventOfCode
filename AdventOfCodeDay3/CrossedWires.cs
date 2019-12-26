using System;
using System.Collections.Generic;

/// <summary>
/// Advent of Code - Day 3 namespace
/// </summary>
namespace AdventOfCodeDay3
{
    /// <summary>
    /// Crossed wires class
    /// </summary>
    public class CrossedWires
    {
        /// <summary>
        /// Wires
        /// </summary>
        private List<Wire> wires = new List<Wire>();

        /// <summary>
        /// Intersections
        /// </summary>
        private HashSet<Vector2Int> intersections = new HashSet<Vector2Int>();

        /// <summary>
        /// Manhattan distance to closest intersection
        /// </summary>
        public uint ManhattanDistanceToClosestIntersection
        {
            get
            {
                uint ret = uint.MaxValue;
                foreach (Vector2Int intersection in intersections)
                {
                    uint manhattan_distance = (uint)(Math.Abs(intersection.X) + Math.Abs(intersection.Y));
                    if (manhattan_distance < ret)
                    {
                        ret = manhattan_distance;
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Fewest combined steps to intersection
        /// </summary>
        public uint FewestCombinedStepsToIntersection
        {
            get
            {
                uint ret = uint.MaxValue;
                foreach (Vector2Int intersection in intersections)
                {
                    uint steps = 0U;
                    foreach (Wire wire in wires)
                    {
                        if (wire.ContainsPosition(intersection))
                        {
                            foreach (Vector2Int point in wire.Path)
                            {
                                ++steps;
                                if (point == intersection)
                                {
                                    break;
                                }
                            }
                            if (wire.Path.Count > 0)
                            {
                                --steps;
                            }
                        }
                    }
                    if (steps < ret)
                    {
                        ret = steps;
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CrossedWires()
        {
            AddNewWire();
        }

        /// <summary>
        /// Move
        /// </summary>
        /// <param name="direction">Direction</param>
        /// <param name="steps">Steps</param>
        public void Move(EGridDirection direction, uint steps)
        {
            int index = wires.Count - 1;
            Wire wire = wires[index];
            for (uint i = 0U, len = steps; i != len; i++)
            {
                if (wire.Move(direction) != Vector2Int.zero)
                {
                    for (int j = 0; j < wires.Count; j++)
                    {
                        if (j != index)
                        {
                            Wire other_wire = wires[j];
                            if (other_wire.ContainsPosition(wire.CurrentPosition))
                            {
                                if (!(intersections.Contains(wire.CurrentPosition)))
                                {
                                    intersections.Add(wire.CurrentPosition);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add new wire
        /// </summary>
        public void AddNewWire()
        {
            wires.Add(new Wire());
        }
    }
}
