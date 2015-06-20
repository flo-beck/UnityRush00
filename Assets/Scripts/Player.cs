using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public float speed = 8;
	public Vector3 velocity;
	public Rigidbody2D rb;
	public Animator animator;

	void Awake ()
	{

	}

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		//animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		//face mouse
		Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, transform.position - mouse);

		//move

		Vector3 vel = new Vector3 ();
		
		if (Input.GetKey (KeyCode.W)) {
			Vector3 velUp = new Vector3 ();
			// just use 1 to set the direction.
			velUp.y = 1;
			vel += velUp;
			rb.AddForce (transform.forward * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.S)) {
			Vector3 velDown = new Vector3 ();
			velDown.y = -1;
			vel += velDown;
		}

		// no else here. Combinations of up/down and left/right are fine.
		if (Input.GetKey (KeyCode.A)) {
			Vector3 velLeft = new Vector3 ();
			velLeft.x = -1;
			vel += velLeft;
		} else if (Input.GetKey (KeyCode.D)) {
			Vector3 velRight = new Vector3 ();
			velRight.x = 1;
			vel += velRight;
		}
		
		// check if player wants to move at all. Don't check exactly for 0 to avoid rounding errors
		// (magnitude will be 0, 1 or sqrt(2) here)
	
		if (vel.magnitude > 0.001) {
			Vector3.Normalize (vel);
			vel *= speed;
			rb.velocity = vel;
			animator.SetTrigger ("walk");
		} else { 
			animator.SetTrigger ("stopWalk");
		}
		
	}


}
