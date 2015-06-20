using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour
{
	
	public float speed;

	public Vector3 mousePos;
	public Vector3 direction;
	private Rigidbody2D rb;
	public shotType type;

	public enum shotType {
		MISSILE,
		ACTION
	}
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 0;
		direction = mousePos - transform.position;
		direction.Normalize ();

		if (type == shotType.ACTION)
			StartCoroutine (swipe ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (type == shotType.MISSILE) {
			transform.Translate (Vector3.down * Time.deltaTime * speed);
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Destroy (gameObject);
	}

	public IEnumerator swipe() {
		rb.AddRelativeForce (Vector3.down * speed, ForceMode2D.Impulse);
		yield return new WaitForSeconds (0.05f);
		Destroy (gameObject);
	}
}
