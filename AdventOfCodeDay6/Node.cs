using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Advent of Code - Day 6 namespace
/// </summary>
namespace AdventOfCodeDay6
{
    /// <summary>
    /// Node class
    /// </summary>
    public class Node : IComparable<Node>
    {
        /// <summary>
        /// Node name
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// Node children
        /// </summary>
        private HashSet<Node> children = new HashSet<Node>();

        /// <summary>
        /// Node parent
        /// </summary>
        public Node Parent { get; private set; }

        /// <summary>
        /// Node children
        /// </summary>
        public IEnumerable<Node> Children => children;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public Node(string name)
        {
            if (name != null)
            {
                this.name = name;
            }
        }

        /// <summary>
        /// Add node
        /// </summary>
        /// <param name="child">Child node</param>
        public void Add(Node child)
        {
            if (child != null)
            {
                children.Add(child);
                child.Parent = this;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Comparison result</returns>
        public int CompareTo([AllowNull] Node other)
        {
            int ret = -1;
            if (other != null)
            {
                ret = name.CompareTo(other.name);
            }
            return ret;
        }
    }
}
