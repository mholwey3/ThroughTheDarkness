using UnityEngine;
using System.Collections;

public class DeathScreenGUI : MonoBehaviour 
{
	public void Update()
	{
		Screen.lockCursor = false;
	}

	public void Retry()
	{
		int i = 1;
		GameObject timer = GameObject.Find("Counter");
		if (timer != null) {
			TimerScript count = timer.GetComponent<TimerScript>();
			if (count != null) {
				i = count.level;
			}
		}
		Application.LoadLevel (i);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
