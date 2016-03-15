using UnityEngine;
using System.Collections;

public class DemonAnimator : MonoBehaviour 
{
	Animator anim;
	FSM fsm;
	bool attacking = false;
	
	void Start ()
	{
		anim = GetComponent<Animator>();
		fsm = GetComponent<FSM>();
	}
	
	void Update ()
	{
		if(fsm.CurrentState == FSM.State.Patrol && fsm.agent.speed > 0)
		{
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
		}
		else if(fsm.CurrentState == FSM.State.Patrol && fsm.agent.speed == 0)
		{
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", true);
		}
		else if(fsm.CurrentState == FSM.State.PursuePlayer && attacking == false)
		{
			anim.SetBool ("isRunning", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttack", false);
		}
		else if(fsm.CurrentState == FSM.State.Investigate && fsm.agent.speed > 0)
		{
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
		}
		else if(fsm.CurrentState == FSM.State.Investigate && fsm.agent.speed == 0)
		{
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", true);
		}
		else if(fsm.CurrentState == FSM.State.ReturnToPatrol)
		{
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
		}
	}

	// enters the attacking state
	void OnCollisionStay(Collision collision) {
		if(fsm.CurrentState == FSM.State.PursuePlayer && collision.gameObject.tag == "Player")
		{
			attacking = true;
			StartCoroutine(Attacks());
			anim.SetBool ("isAttack", true);
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", false);
		}
	}
	
	IEnumerator Attacks() {
		yield return new WaitForSeconds(0.75f);
		attacking = false;
	}
}
