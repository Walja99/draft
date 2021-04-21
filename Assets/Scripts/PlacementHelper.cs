using System;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{
    public static class PlacementHelper
    {
        public static List<Direction> FindNeighbour(Vector3 position, ICollection<Vector3> collection, Quaternion rotation)
        {
            List<Direction> neighbourDirections = new List<Direction>();
             if (collection.Contains(position + (Quaternion.Euler(1, 0, 0)).eulerAngles))
             {
                 neighbourDirections.Add(Direction.Right);
             }
 
             if (collection.Contains(position - (Quaternion.Euler(1, 0, 0)).eulerAngles))
             {
                 neighbourDirections.Add(Direction.Left);
             }
 
             if (collection.Contains(position + (Quaternion.Euler(0, 0, 1)).eulerAngles))
             {
                 neighbourDirections.Add(Direction.Up);
             }
                  if (collection.Contains(position - (Quaternion.Euler(0, 0, 1)).eulerAngles))
                  {
                      neighbourDirections.Add(Direction.Down);
                  }
      
            return neighbourDirections;
        }

        internal static Vector3 GetOffsetFromDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Vector3(0, 0, 1);
                case Direction.Down:
                    return new Vector3(0, 0, -1);
                case Direction.Left:
                    return Vector3.left;
                case Direction.Right:
                    return Vector3.right;
                default:
                    break;
            }
            throw new System.Exception("No direction such as " + direction);
        }

        public static Direction GetReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    break;
            }
            throw new System.Exception("No direction such as " + direction);
        }
    }
}

