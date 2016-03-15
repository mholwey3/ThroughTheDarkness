using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM : MonoBehaviour 
{
#region variables
	//private
	bool patrolling = false;
	bool investigating = false;
	bool pursuing = false;
	int waypoint = 1;
	float distanceFromPlayer;
	Transform _transform;
	PathsScript path_script;
	GameObject playerLantern;

	//public
	public Transform eyes;
	public GameObject path;
	public Transform player;
	[HideInInspector]
	public NavMeshAgent agent;
	public float walkVelocity = 1.0f, runVelocity = 2.5f;
	public enum State 
	{
		Patrol,
		PursuePlayer,
		Investigate,
		ReturnToPatrol,
		Default
	};

	State _state;
#endregion

#region getter/setter
	public State CurrentState
	{
		get
		{
			return _state;
		}

		set
		{
			_state = value;
		}
	}
#endregion

	void Awake () 
	{
		path_script = path.GetComponent<PathsScript>();
		_transform = GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
		CurrentState = State.Patrol;
		distanceFromPlayer = Vector3.Distance (_transform.position, player.position);
		playerLantern = GameObject.FindGameObjectWithTag ("Lantern");
	}
	
	void Start()
	{
		StartCoroutine(ManageStates());
	}

	void Update () 
	{
		distanceFromPlayer = Vector3.Distance(_transform.position, player.position);

		//Enemy will switch to pursueplayer state when it has direct vision of the player
		//OR when the enemy notices the light being emitted from the player (even if turned away)
		if((distanceFromPlayer < (playerLantern.GetComponent<Light>().range + 1) && !Physics.Linecast (eyes.position, playerLantern.transform.position) || distanceFromPlayer < playerLantern.GetComponent<Light>().range - 1) && !pursuing)
		{
			CurrentState = State.PursuePlayer;
		}
	}
	
	IEnumerator ManageStates()
	{
		//The FSM is always running in a state so we need an infinite loop
		while(true)
		{
			switch(_state)
			{
			case State.Patrol:
				if(!patrolling)
				{
					agent.speed = walkVelocity;
					StartCoroutine ("Patrol");
					patrolling = true;
				}
				break;

			case State.PursuePlayer:
				if(!pursuing)
				{
					StopCoroutine("Patrol");
					StopCoroutine ("Investigate");
					patrolling = investigating = false;
					pursuing = true;
					agent.speed = runVelocity;
				}

				agent.SetDestination(player.position);
				if(Vector3.Distance (transform.position, player.transform.position) >= playerLantern.GetComponent<Light>().range + 2f)
				{
					agent.Stop(true);
					agent.speed = 0f;
					CurrentState = State.Investigate;
				}
				break;
			
			case State.Investigate:
				if(!investigating)
				{
					pursuing = false;
					investigating = true;
					StartCoroutine ("Investigate");
				}
				break;
			case State.ReturnToPatrol:
				StopCoroutine ("Investigate");
				investigating = false;
				agent.speed = walkVelocity;
				if(Vector3.Distance (_transform.position, ClosestPoint().transform.position) > 0.5f)
					agent.SetDestination (ClosestPoint().transform.position);
				else
				{
					waypoint = path_script.waypoints.IndexOf(ClosestPoint ());
					CurrentState = State.Patrol;
				}
				break;
			case State.Default:
				break;
			}
			yield return null;
		}
	}

	//Cycle through the patrol points, stopping briefly at each one
	public IEnumerator Patrol()
	{
		while(true)
		{
			if(Vector3.Distance (_transform.position, path_script.waypoints[waypoint].transform.position) > 0.5f)
			{
				agent.SetDestination(path_script.waypoints[waypoint].transform.position);
				yield return null;
			}
			else
			{
				agent.Stop(true);
				agent.speed = 0f;
				waypoint = (waypoint + 1) % path_script.waypoints.Count;
				yield return new WaitForSeconds(2.0f);
				agent.speed = walkVelocity;
			}
		}
	}

	//After the agent has lost sight of the player, it will look in the area the player was last spotted 
	//and return to its patrol if the player is still not found
	public IEnumerator Investigate()
	{
		yield return new WaitForSeconds(1f);
		Vector3 playerPos = player.position;

		agent.speed = walkVelocity;
		while(true)
		{
			if(Vector3.Distance (transform.position, playerPos) > 1f)
			{
				agent.SetDestination(playerPos);
			}
			else
			{
				agent.Stop (true);
				agent.speed = 0f;
				yield return new WaitForSeconds(1.0f);
				CurrentState = State.ReturnToPatrol;
			}

			yield return null;
		}
	}

	//Finds the waypoint that is closest to the agent so it can return to its patrol
	public GameObject ClosestPoint()
	{
		GameObject closest = path_script.waypoints[0];
		float shortest_distance = Vector3.Distance(this.gameObject.transform.position, path_script.waypoints[0].transform.position);
		if(path_script.waypoints.Count > 1)
		{
			foreach(GameObject point in path_script.waypoints)
			{
				if(Vector3.Distance(this.gameObject.transform.position, point.transform.position) < shortest_distance)
				{
					closest = point;
					shortest_distance = Vector3.Distance(this.gameObject.transform.position, point.transform.position);
				}
			}
		}
		return closest;
	}
}
