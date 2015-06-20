using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public Sprite mainImg;
	public Sprite heldImg;
	SpriteRenderer sr;
	Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>(); 
		rb = gameObject.GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "player" && Input.GetKeyDown (KeyCode.E)) {
			sr.enabled = false;
		}
	}

	public void getThrown(){

		sr.enabled = true;
		//get direction to throw
		rb.AddRelativeForce(Vector3.forward * 100);
	}

}
