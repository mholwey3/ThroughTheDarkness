using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UnitPathfinding))]
public class UnitPathFindingEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		UnitPathfinding path_finding_script = (UnitPathfinding)target;

		if(GUILayout.Button("Go To Path"))
		{
			path_finding_script.GoToPath();
		}

	}
}
