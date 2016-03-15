using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			switch(Application.loadedLevel) {
				case 1:
					Application.LoadLevel (4);
					break;
				case 4: 
					Application.LoadLevel(5);
					break;
				case 5: 
					Application.LoadLevel (6);
					break;
				case 6: 
					Application.LoadLevel (7);
					break;
				case 7: 
					Application.LoadLevel(0);
					break;
				default:
					break;
			}

		}
	}
}
