using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PathsScript))]
public class PathsEditor : Editor {

	float position;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PathsScript paths_script = (PathsScript)target;

		if(GUILayout.Button("Add Waypoint"))
		{
			paths_script.addWaypoint();
		}
		
		if(GUILayout.Button("Clear Waypoints"))
		{
			paths_script.clearWaypoints();
		}

	}

	[MenuItem("Tools/Pathing/New Path")]
	static void newPath()
	{
		GameObject path = new GameObject();
		path.name = "Path";
		path.transform.position = new Vector3(0,0,0);
		path.AddComponent<PathsScript>();
	}
}