using UnityEngine;
//using UnityEditor;
using System.Collections;

public class WaypointScript : MonoBehaviour {

void OnDrawGizmos()
	{
//		PathsScript path_script = this.transform.GetComponentInParent<PathsScript>();
//		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), path_script.waypoint_radius);
//
//		if(path_script.waypoints.Count == 2)
//		{
//			//Gizmos.DrawLine(path_script.waypoints[0].transform.position, path_script.waypoints[1].transform.position);
//			Handles.DrawLine(path_script.waypoints[0].transform.position, path_script.waypoints[1].transform.position);
//		}
//		else if(path_script.waypoints.Count > 2)
//		{
//			int index = 0;
//			while(index < path_script.waypoints.Count - 1)
//			{
//				//Gizmos.DrawLine(path_script.waypoints[index].transform.position, path_script.waypoints[index+1].transform.position);
//				Handles.DrawLine(path_script.waypoints[index].transform.position, path_script.waypoints[index+1].transform.position);
//				index++;
//			}
//			//Gizmos.DrawLine(path_script.waypoints[index].transform.position, path_script.waypoints[0].transform.position);
//			Handles.DrawLine(path_script.waypoints[index].transform.position, path_script.waypoints[0].transform.position);
//		}
	}

	public void removeWaypoint()
	{
//		PathsScript path_script = this.transform.GetComponentInParent<PathsScript>();
//		int index = path_script.waypoints.IndexOf(gameObject);
//		DestroyImmediate(path_script.waypoints[index]);
//		path_script.waypoints.RemoveAt(index);
	}

	#region extra methods
	public void addWaypointBefore()
	{
//		PathsScript path_script = this.transform.GetComponentInParent<PathsScript>();
//		int last_waypoint;
//		if(path_script.waypoints.IndexOf(this.gameObject) == 0)
//		{
//			last_waypoint = path_script.waypoints.Count-1;
//		}
//		else
//		{
//			last_waypoint = path_script.waypoints.IndexOf(this.gameObject)-1;
//		}
//		GameObject temp = new GameObject();
//		temp.name = "Waypoint";
//		temp.transform.parent = this.transform.parent;
//		temp.transform.position = (this.transform.position + path_script.waypoints[last_waypoint].transform.position)/2;
//		temp.AddComponent<WaypointScript>();
//		path_script.waypoints.Insert(last_waypoint+1,temp);
	}

	public void addWaypointAfter()
	{
//		PathsScript path_script = this.transform.GetComponentInParent<PathsScript>();
//		int next_waypoint;
//		if(path_script.waypoints.IndexOf(this.gameObject) == path_script.waypoints.Count-1)
//		{
//			next_waypoint = 0;
//		}
//		else
//		{
//			next_waypoint = path_script.waypoints.IndexOf(this.gameObject)+1;
//		}
//		GameObject temp = new GameObject();
//		temp.name = "Waypoint";
//		temp.transform.parent = this.transform.parent;
//		temp.transform.position = (this.transform.position + path_script.waypoints[next_waypoint].transform.position)/2;
//		temp.AddComponent<WaypointScript>();
//		path_script.waypoints.Insert(next_waypoint,temp);
	}
	#endregion
}
