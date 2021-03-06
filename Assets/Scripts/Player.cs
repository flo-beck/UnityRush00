﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{

	public float speed;
	public Vector3 velocity;
	public Rigidbody2D rb;
	public Animator animator;
	public SpriteRenderer weaponImg;
	public Weapon weapon;
	public GameObject ammoLaunchPos;

	public AudioSource reloadSound;
	public AudioSource ejectSound;
	public AudioSource dieSound;

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
		mouse.z = 0;
		transform.rotation = Quaternion.LookRotation (Vector3.forward, transform.position - mouse);

		//move
		Vector3 vel = new Vector3 ();
		
		if (Input.GetKey (KeyCode.W)) {
			Vector3 velUp = new Vector3 ();
			// just use 1 to set the direction.
			velUp.y = 0.5f;
			vel += velUp;
			//rb.AddForce (transform.forward * Time.deltaTime);
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

		//pick up weapon
		if (Input.GetKeyDown (KeyCode.E)) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, 100, 1 << 15); //WEAPON LAYER
			if (!weapon && hit.collider != null){
				reloadSound.Play();
				weapon = hit.collider.gameObject.GetComponent<Weapon> ();
				SpriteRenderer weaponSprite = weapon.GetComponent<SpriteRenderer>();
				weaponSprite.enabled = false;
				weapon.gameObject.layer = gameObject.layer;
				weaponImg.sprite = weapon.heldImg;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "ammo") {
			playerDie();
		}
	}

	void fireWeapon(){
		if (weapon) {
			weapon.fire(ammoLaunchPos.transform.position, transform);
		}
	}

	void dropWeapon(){
		if (weapon) {
			ejectSound.Play();
			weapon.gameObject.layer = 15; 
			weaponImg.sprite = null;
			weapon.transform.position = transform.position;
			StartCoroutine(weapon.throwWeapon(mouse));
			weapon = null;
		}
	}

	public void playerDie(){
		AudioSource.PlayClipAtPoint(dieSound.clip, transform.position);;
		gameObject.SetActive (false);
	}
	
}
