using System.Collections.Generic;

/// <summary>
/// Advent of Code - Day 6 namespace
/// </summary>
namespace AdventOfCodeDay6
{
    /// <summary>
    /// Orbital map class
    /// </summary>
    public class OrbitalMap
    {
        /// <summary>
        /// Nodes
        /// </summary>
        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        /// <summary>
        /// Number of direct and indirect orbits
        /// </summary>
        public uint NumberOfDirectAndIndirectOrbits
        {
            get
            {
                uint ret = 0U;
                foreach (Node node in nodes.Values)
                {
                    for (Node current_node = node; current_node != null; current_node = current_node.Parent)
                    {
                        ++ret;
                    }
                    --ret;
                }
                return ret;
            }
        }

        /// <summary>
        /// Add relationship
        /// </summary>
        /// <param name="parent">Parent</param>
        /// <param name="child">Child</param>
        public void AddRelationship(string parent, string child)
        {
            if ((parent != null) && (child != null))
            {
                Node parent_node;
                Node child_node;
                if (nodes.ContainsKey(parent))
                {
                    parent_node = nodes[parent];
                }
                else
                {
                    parent_node = new Node(parent);
                    nodes.Add(parent, parent_node);
                }
                if (nodes.ContainsKey(child))
                {
                    child_node = nodes[child];
                }
                else
                {
                    child_node = new Node(child);
                    nodes.Add(child, child_node);
                }
                parent_node.Add(child_node);
            }
        }

        /// <summary>
        /// Get common ancestor node
        /// </summary>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        /// <returns>Common ancestor node if successful, otherwise "null"</returns>
        public Node GetCommonAncestorNode(string left, string right)
        {
            Node ret = null;
            if ((left != null) && (right != null))
            {
                if (nodes.ContainsKey(left) && nodes.ContainsKey(right))
                {
                    Node left_node = nodes[left];
                    Node right_node = nodes[right];
                    for (Node current_left_node = left_node; current_left_node != null; current_left_node = current_left_node.Parent)
                    {
                        for (Node current_right_node = right_node; current_right_node != null; current_right_node = current_right_node.Parent)
                        {
                            if (current_left_node == current_right_node)
                            {
                                ret = current_left_node;
                                break;
                            }
                        }
                        if (ret != null)
                        {
                            break;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Get number of orbital maneuvers
        /// </summary>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <returns>Number of orbital maneuvers</returns>
        public uint GetNumberOfOrbitalManeuvers(string from, string to)
        {
            uint ret = 0U;
            if ((from != null) && (to != null))
            {
                Node common_ancestor_node = GetCommonAncestorNode(from, to);
                if (common_ancestor_node != null)
                {
                    Node from_node = nodes[from];
                    Node to_node = nodes[to];
                    for (Node current_from_node = from_node.Parent; current_from_node != common_ancestor_node; current_from_node = current_from_node.Parent)
                    {
                        ++ret;
                    }
                    for (Node current_to_node = to_node.Parent; current_to_node != common_ancestor_node; current_to_node = current_to_node.Parent)
                    {
                        ++ret;
                    }
                }
            }
            return ret;
        }
    }
}
