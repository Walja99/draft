using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SVS
{
	public class RoadHelper : MonoBehaviour
	{
		public Action finishedCoroutine;
		public GameObject roadStraight, roadCorner, road3way, road4way, roadEnd;
		Dictionary<Vector3, GameObject> roadDictionary = new Dictionary<Vector3, GameObject>();
		Dictionary<Vector3, bool> roadRotation = new Dictionary<Vector3, bool>();
		Dictionary<Vector3, Quaternion> fixRoadCandidates = new Dictionary<Vector3, Quaternion>();
		public float animationTime = 0.01f;

		public List<Vector3> GetRoadPositions()
		{
			return roadDictionary.Keys.ToList();
		}

		public IEnumerator PlaceStreetPositions(Vector3 startPosition, Vector3 direction, int length)
		{
			var rotation = Quaternion.identity;
			if(direction.z * direction.x != 0)
			{
				rotation = Quaternion.Euler(0, -45 * direction.z * direction.x, 0);
				
			}
			else if (direction.x == 0)
			{
				rotation = Quaternion.Euler(0, 90, 0);
			}
			for (int i = 0; i < length; i++)
			{
				
				var position = startPosition + direction *i;
				if (roadDictionary.ContainsKey(position))
				{
					continue;
				}
				if (fixRoadCandidates.ContainsKey(position))
				{
					continue;
				}
				
				var road = Instantiate(roadStraight, position, rotation, transform);
				
				road.AddComponent<FallTween>();
				roadDictionary.Add(position, road);
				roadRotation.Add(position, (direction.z * direction.x != 0));
				if(i==0 || i==length-1)
				{
					fixRoadCandidates.Add(position, rotation);
				}
				yield return new WaitForSeconds(animationTime);
			}
			finishedCoroutine?.Invoke();
		}

		public void FixRoad()
		{
			foreach (var position in fixRoadCandidates.Keys)
			{
				List<Direction> neighbourDirections = PlacementHelper.FindNeighbour(position, roadDictionary.Keys, fixRoadCandidates[position]);

				Quaternion rotation = Quaternion.identity;

				if (neighbourDirections.Count == 1)
				{
					Destroy(roadDictionary[position]);
					if (neighbourDirections.Contains(Direction.Down))
					{
						rotation = Quaternion.Euler(0, 90, 0);
					} else if (neighbourDirections.Contains(Direction.Left))
					{
						rotation = Quaternion.Euler(0, 180, 0);
					}
					else if (neighbourDirections.Contains(Direction.Up))
					{
						rotation = Quaternion.Euler(0, -90, 0);
					}
					else if (neighbourDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, 0, 0);
					}
					roadDictionary[position] = Instantiate(roadEnd, position, rotation, transform);
				}
				else if (neighbourDirections.Count == 2)
				{
					if(
						neighbourDirections.Contains(Direction.Up) && neighbourDirections.Contains(Direction.Down)
						|| neighbourDirections.Contains(Direction.Right) && neighbourDirections.Contains(Direction.Left)
						)
					{
						continue;
					}
					Destroy(roadDictionary[position]);
					if (neighbourDirections.Contains(Direction.Up) && neighbourDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, 90, 0);
					}
					else if (neighbourDirections.Contains(Direction.Right) && neighbourDirections.Contains(Direction.Down))
					{
						rotation = Quaternion.Euler(0, 180, 0);
					}
					else if (neighbourDirections.Contains(Direction.Down) && neighbourDirections.Contains(Direction.Left))
					{
						rotation = Quaternion.Euler(0, -90, 0);
					}
					roadDictionary[position] = Instantiate(roadCorner, position, rotation, transform);
				}
				else if(neighbourDirections.Count == 3)
				{
					Destroy(roadDictionary[position]);
					if (neighbourDirections.Contains(Direction.Right) 
						&& neighbourDirections.Contains(Direction.Down) 
						&& neighbourDirections.Contains(Direction.Left)
						)
					{
						rotation = Quaternion.Euler(0, 90, 0);
					}
					else if (neighbourDirections.Contains(Direction.Down) 
						&& neighbourDirections.Contains(Direction.Left)
						&& neighbourDirections.Contains(Direction.Up))
					{
						rotation = Quaternion.Euler(0, 180, 0);
					}
					else if (neighbourDirections.Contains(Direction.Left) 
						&& neighbourDirections.Contains(Direction.Up)
						&& neighbourDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, -90, 0);
					}
					roadDictionary[position] = Instantiate(road3way, position, rotation, transform);
				}
				else if (neighbourDirections.Count == 4)
				{
					Destroy(roadDictionary[position]);
					if (roadRotation[position])
					{
						rotation = Quaternion.Euler(0, 45, 0);
					}
					roadDictionary[position] = Instantiate(road4way, position, rotation, transform);
					

				}
				else
				{
					Debug.Log((neighbourDirections.Count).ToString());
				}
			}
		}
		public void Reset()
		{
			foreach (var item in roadDictionary.Values)
			{
				Destroy(item);
			}
			roadDictionary.Clear();
			fixRoadCandidates.Clear();
		}
	}
}

