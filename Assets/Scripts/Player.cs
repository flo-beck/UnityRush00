using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float thrust;
	public Rigidbody rb;

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
		//face mouse
		Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - mouse);

		//move
		if (Input.GetKey(KeyCode.W)){
			rb.AddForce(transform.forward * Time.deltaTime);
		}

	}


}
