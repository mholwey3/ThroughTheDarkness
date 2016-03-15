using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WaypointScript))]
public class WaypointsEditor : Editor{

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		WaypointScript waypoint_script = (WaypointScript)target;

		if(GUILayout.Button("Remove Waypoint"))
		{
			waypoint_script.removeWaypoint();
		}

		if(GUILayout.Button("Add Waypoint Before"))
		{
			waypoint_script.addWaypointBefore();
		}

		if(GUILayout.Button("Add Waypoint After"))
		{
			waypoint_script.addWaypointAfter();
		}
	}
}
