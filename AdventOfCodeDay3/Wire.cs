using System.Collections.Generic;

/// <summary>
/// Advent Of Code - Day 3 namespace
/// </summary>
namespace AdventOfCodeDay3
{
    /// <summary>
    /// Wire clöass
    /// </summary>
    public class Wire
    {
        /// <summary>
        /// Lookup wire
        /// </summary>
        private HashSet<Vector2Int> lookup = new HashSet<Vector2Int>();

        /// <summary>
        /// Wire path
        /// </summary>
        private List<Vector2Int> path = new List<Vector2Int>();

        /// <summary>
        /// Current position
        /// </summary>
        private Vector2Int currentPosition = Vector2Int.zero;

        /// <summary>
        /// Current position
        /// </summary>
        public Vector2Int CurrentPosition => currentPosition;

        /// <summary>
        /// Path
        /// </summary>
        public IReadOnlyList<Vector2Int> Path => path;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Wire()
        {
            lookup.Add(Vector2Int.zero);
            path.Add(Vector2Int.zero);
        }

        /// <summary>
        /// Move
        /// </summary>
        /// <param name="direction">Direction</param>
        /// <returns>Current position</returns>
        public Vector2Int Move(EGridDirection direction)
        {
            switch (direction)
            {
                case EGridDirection.Up:
                    currentPosition += Vector2Int.up;
                    break;
                case EGridDirection.Down:
                    currentPosition += Vector2Int.down;
                    break;
                case EGridDirection.Left:
                    currentPosition += Vector2Int.left;
                    break;
                case EGridDirection.Right:
                    currentPosition += Vector2Int.right;
                    break;
            }
            lookup.Add(currentPosition);
            path.Add(currentPosition);
            return currentPosition;
        }

        /// <summary>
        /// Does wire contain position
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>"true" if wire contains given position, otherwise "false"</returns>
        public bool ContainsPosition(Vector2Int position) => lookup.Contains(position);
    }
}
