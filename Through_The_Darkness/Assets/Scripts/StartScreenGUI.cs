using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreenGUI : MonoBehaviour 
{	
	void Start()
	{
		Cursor.visible = true;
		Screen.lockCursor = false;
	}
	
	public void StartGame()
	{
		Application.LoadLevel (1);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	public void Credits()
	{
		Application.LoadLevel (3);
	}
}
