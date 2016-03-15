using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour 
{
	[HideInInspector]
	public float timer;
	[HideInInspector]
	public int level;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update () 
	{
		GameObject gui = GameObject.Find("GUI Manager"); 
		if (gui != null) {
			GameplayGUI count = gui.GetComponent<GameplayGUI>();
			if(count != null) {
				timer = count.timer;
			}
		}
		if (Application.loadedLevel == 0) {
			timer = 0;
		}
		if (Application.loadedLevel != 2) {
			level = Application.loadedLevel;
		}
	}
}
