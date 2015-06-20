using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//face mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Debug.Log ("MOUSE POS: " + mousePos);
		
		//Vector3 newVec = transform.position - mousePos;
		mousePos.z = 0;
//		transform.LookAt(mousePos);
		transform.LookAt(mousePos);



	}
}
