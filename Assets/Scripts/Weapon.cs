using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite mainImg;
	public Sprite heldImg;
	public weaponType type;
	public int ammo;
	public Ammunition a;


	private Vector3 mouse;

	public enum weaponType {
		GUN,
		BLADE
	}

	SpriteRenderer sr;

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

	public void fire(Vector3 playerPos, Vector3 launchPos){
		if (type == weaponType.GUN && ammo > 0){
			Debug.Log("LAUNCH MISSILES");
			
//			Vector3 newPos = mouse - playerPos;
//			newPos.Normalize();
//			newPos = playerPos + newPos * 1f;

			GameObject.Instantiate(a, launchPos, Quaternion.identity);
		}
	}

}
