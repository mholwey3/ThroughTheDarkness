using UnityEngine;
using System.Collections;

public class UnitPathfinding : MonoBehaviour {

	//Example on how to access path's closest waypoint
	public GameObject path;

	public void GoToPath()
	{
		PathsScript path_script = path.GetComponent<PathsScript>();
		this.gameObject.transform.position = path_script.ClosestPoint(this.gameObject).transform.position;
	}
}
