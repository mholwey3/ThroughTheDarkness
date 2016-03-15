using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEditor;

[CustomEditor(typeof(GenerationScript))]
public class GenerationEditor : Editor {

	string tag;
	
	public override void OnInspectorGUI() {
		
		DrawDefaultInspector();
		
		GenerationScript generation_script = (GenerationScript)target;
		
		if(GUILayout.Button("New Level"))
		{
			ClearLog();
			generation_script.newLevel();
		}

		tag = EditorGUILayout.TagField("Tag:", tag);

		if(GUILayout.Button("Print all with Tag"))
		{
			ClearLog();
			generation_script.printObjects(tag);
		}

		if(GUILayout.Button("Generate"))
		{
			generation_script.Decide();
		}
	}
	
	public static void ClearLog()
	{
		var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
		var type = assembly.GetType("UnityEditorInternal.LogEntries");
		var method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}
	
}