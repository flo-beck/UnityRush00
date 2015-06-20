using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public float speed = 5;
	public Vector3 velocity;
	public Rigidbody2D rb;
	public Animator animator;
	public SpriteRenderer weaponImg;
	public Weapon weapon;

	private Vector3 mouse;

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
		mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, transform.position - mouse);

		//move
		Vector3 vel = new Vector3 ();
		
		if (Input.GetKey (KeyCode.W)) {
			Vector3 velUp = new Vector3 ();
			// just use 1 to set the direction.
			velUp.y = 0.5f;
			vel += velUp;
			rb.AddForce (transform.forward * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.S)) {
			Vector3 velDown = new Vector3 ();
			velDown.y = -0.5f;
			vel += velDown;
		}

		// Combinations of up/down and left/right are fine.
		if (Input.GetKey (KeyCode.A)) {
			Vector3 velLeft = new Vector3 ();
			velLeft.x = -0.5f;
			vel += velLeft;
		} else if (Input.GetKey (KeyCode.D)) {
			Vector3 velRight = new Vector3 ();
			velRight.x = 0.5f;
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

		//fire
		if (Input.GetMouseButtonDown (0)) //LEFT CLICK
			fireWeapon ();
		else if (Input.GetMouseButtonDown (1))
			dropWeapon ();

	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.tag == "weapon" && Input.GetKeyDown (KeyCode.E)) {	
			weapon = other.gameObject.GetComponent<Weapon> ();
			weaponImg.sprite = weapon.heldImg;
		}
	}

	void fireWeapon(){
		if (weapon) {
		}
	}

	void dropWeapon(){
		if (weapon) {
			//throw weapon away
			Debug.Log("THROW WEAPON");
			weaponImg.sprite = null;
			weapon.transform.position = transform.position;
			weapon.getThrown();

		}

	}

}
