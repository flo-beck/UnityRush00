using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite mainImg;
	public Sprite heldImg;
	SpriteRenderer sr;
	//Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>(); 
		//rb = gameObject.GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update () {
		//rb.AddRelativeForce (Vector3.down * 2.0f);
	
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
		tmprb.AddRelativeForce (direction * 3.0f, ForceMode2D.Impulse);
		yield return new WaitForSeconds (2f);
		Destroy (tmprb);
	}
}
