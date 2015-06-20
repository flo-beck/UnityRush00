using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour
{

	public float speed = 5f;

	public Vector3 mousePos;
	public Vector3 direction;

	private Rigidbody2D rb;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		Debug.Log ("Mouse Pos: " + mousePos);
		direction = mousePos - transform.position;
		Debug.Log ("DIRECTION: " + direction);
		
		direction.Normalize ();
		Debug.Log ("Normalized: " + direction);
		
		//transform.rotation = Quaternion.LookRotation (Vector3.forward, transform.position - mousePos);
	}
	
	// Update is called once per frame
	void Update ()
	{
//		rb.AddRelativeForce (direction * 20.0f, ForceMode2D.Impulse);


		transform.Translate (direction * Time.deltaTime * speed);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Destroy (gameObject);
	}
}
