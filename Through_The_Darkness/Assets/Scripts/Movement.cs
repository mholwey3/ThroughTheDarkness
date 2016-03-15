using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{

	//animation variables
	protected Animator animator;

	// Lateral Movement Variables
	public float walkAcceleration = 5f;
	public float walkDeacceleration = 5f;
	public float airSpeed = 5f;

	// damage variable
	public bool damage = true;

	[HideInInspector]
	public float walkDeaccelerationVolx;
	[HideInInspector]
	public float walkDeaccelerationVolz;

	public float maxWalkSpeed;
	public float originalSpeed;
	public GameObject cameraObject;
	[HideInInspector]
	public Vector2 horizontalMovement;

	//public Vector3 input;

	// Jump Variables
	public bool grounded = false;
	public float jumpVelocity = 20f;
	public float maxSlope = 60f;

	public float originalLocalScaleY;


	void Awake()
	{
		originalSpeed = maxWalkSpeed;
		originalLocalScaleY = transform.localScale.y;
	}

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void LateUpdate () 
	{
		Move();
		Jump();
	}
	

	void Move()
	{
		horizontalMovement = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z);
		if (horizontalMovement.magnitude > maxWalkSpeed)
		{
			horizontalMovement = horizontalMovement.normalized;
			horizontalMovement *= maxWalkSpeed;
		}
		GetComponent<Rigidbody>().velocity = new Vector3(horizontalMovement.x, transform.GetComponent<Rigidbody>().velocity.y, horizontalMovement.y);
		
		if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && grounded)
		{
			// Stops the player abruptly
			GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
			//Animation bool
			animator.SetBool("isMoving", false );
			//Debug.Log("Stopped moving");
		}

		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && grounded)
		{
			// Animation bool
			animator.SetBool("isMoving", true );
		}
		
		if (grounded)
		{
			GetComponent<Rigidbody>().AddRelativeForce(Input.GetAxis("Horizontal") * walkAcceleration, 0, Input.GetAxis("Vertical") * walkAcceleration);
			// Animation bool
			//animator.SetBool("isMoving", true );
		}
		else if (!grounded)
			GetComponent<Rigidbody>().AddRelativeForce(Input.GetAxis("Horizontal") * airSpeed, 0, Input.GetAxis("Vertical") * airSpeed);
	}



	void Jump()
	{
		if (Input.GetButtonDown("Jump") && grounded)
			GetComponent<Rigidbody>().AddForce(0, jumpVelocity, 0);
	}

	// Checks to see if you are touching a jump-offable surface
	void OnCollisionStay(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
				grounded = true;
		}
	}

	// Checks to see if the player is in the air
	void OnCollisionExit()
	{
		grounded = false;
	}

	void OnCollisionEnter(Collision collision) {
		GameObject controller = GameObject.Find("GUI Manager");
		GameplayGUI gui = controller.GetComponent<GameplayGUI>();
		GameObject touch = collision.gameObject;
		if(!gui.paused) {
			// Checks to see which enemy is damaging the player
			if (damage == true) {
				if (touch.tag == "EasyEnemy") {
					gui.healthBar.value -= .33f;
					gui.playerLantern.intensity = gui.healthBar.value * gui.lightIntensity;
				}
				else if (touch.tag == "MediumEnemy") {
					gui.healthBar.value -= .6f;
					gui.playerLantern.intensity = gui.healthBar.value * gui.lightIntensity;
				}
				else if (touch.tag == "HardEnemy") {
					gui.healthBar.value -= 0.999f;
					gui.playerLantern.intensity = gui.healthBar.value * gui.lightIntensity;
				}
				damage = false;
				StartCoroutine(Damage());
			}
	
			if(gui.healthBar.value <= 0) {
				Application.LoadLevel(2);
			}
		}
	}

	// allows for damage to only be used every second
	IEnumerator Damage() {
		yield return new WaitForSeconds(1f);
		damage = true;
	}
}