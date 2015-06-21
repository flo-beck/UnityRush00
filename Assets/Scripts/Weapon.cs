using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite mainImg;
	public Sprite heldImg;
	public weaponType type;
	public int ammo;
	public Ammunition a;
	public int ammoPerShot;
	public AudioSource fireSound;
	public AudioSource drySound;
	public string weaponName;

	public enum weaponType {
		GUN,
		BLADE
	}

	SpriteRenderer sr;
	private Vector3 mouse;
	
	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>(); 

		mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mouse.z = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "player" && Input.GetKeyDown (KeyCode.E)) {
			sr.enabled = false;
		}
	}

	public IEnumerator throwWeapon(Vector3 mouse) {
		Rigidbody2D tmprb = this.gameObject.AddComponent<Rigidbody2D> (); // Add the rigidbody.
		sr.enabled = true;
		tmprb.drag = 5;
		tmprb.AddTorque (5);
		Vector3 direction = mouse - transform.position;
		direction.Normalize ();
		tmprb.AddRelativeForce (direction * 12.0f, ForceMode2D.Impulse);
		yield return new WaitForSeconds (2f);
		Destroy (tmprb);
	}

	public void fire(Player p){
		a.gameObject.layer = gameObject.layer; // set ammo to same layer as me
		if (type == weaponType.GUN) {
			StartCoroutine (launchMissiles (p));
		}
		else
			swipe (p);
	}

	public IEnumerator launchMissiles(Player p){
		for (int i = 0; i < ammoPerShot; i++){
			if (type == weaponType.GUN && ammo > 0) {
				fireSound.Play ();
				GameObject.Instantiate (a, p.ammoLaunchPos.transform.position, p.transform.rotation);
				ammo--;
			} else 
				drySound.Play();

			yield return new WaitForSeconds (0.05f);
		}
	}

	public void swipe(Player p){
		fireSound.Play ();
		GameObject.Instantiate (a, p.ammoLaunchPos.transform.position, p.transform.rotation);
	}

}
