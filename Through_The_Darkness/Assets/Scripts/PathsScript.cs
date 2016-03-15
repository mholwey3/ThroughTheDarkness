using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathsScript : MonoBehaviour {

	public List<GameObject> waypoints = new List<GameObject>();
	public float waypoint_radius = .5f;

	public void addWaypoint()
	{
		GameObject temp = new GameObject();
		temp.name = "Waypoint";
		temp.transform.parent = transform;
		temp.transform.position = transform.position;
		temp.AddComponent<WaypointScript>();
		waypoints.Insert(waypoints.Count,temp);
	}

	public void clearWaypoints()
	{
		foreach(GameObject item in waypoints)
		{
			DestroyImmediate(item);
		}
		waypoints.Clear();
	}

	public GameObject ClosestPoint(GameObject obj)
	{
		GameObject closest = waypoints[0];
		float shortest_distance = DistanceFormula(obj.gameObject, waypoints[0]);
		if(waypoints.Count > 1)
		{
			int count = 0;
			foreach(GameObject point in waypoints)
			{
				count++;
				if(DistanceFormula(obj.gameObject, point) < shortest_distance)
				{
					closest = point;
					shortest_distance = DistanceFormula(obj.gameObject, point);
				}
			}
		}
		return closest;
	}

	private float DistanceFormula(GameObject one, GameObject two)
	{
		return Mathf.Abs(Mathf.Sqrt(Mathf.Pow((two.transform.position.x - one.transform.position.x),2f) + Mathf.Pow((two.transform.position.y - one.transform.position.y), 2f) + Mathf.Pow((two.transform.position.z - one.transform.position.z), 2f)));
	}
}