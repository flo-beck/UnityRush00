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
	public bool enemy;

	public float coolDown;
	private float t;

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
	
		if (enemy)
			t += Time.deltaTime;
	}

	public IEnumerator throwWeapon(Vector3 mouse) {
		Rigidbody2D tmprb = this.gameObject.AddComponent<Rigidbody2D> (); // Add the rigidbody.
		sr.enabled = true;
		tmprb.drag = 5;
		Vector3 direction = mouse - transform.position;
		direction.Normalize ();
		tmprb.AddRelativeForce (direction * 12.0f, ForceMode2D.Impulse);
		yield return new WaitForSeconds (2f);
		Destroy (tmprb);
	}

	public void fire(Vector3 ammoLaunchPos, Transform shooter){
		a.gameObject.layer = gameObject.layer; // set ammo to same layer as me
		if (type == weaponType.GUN) {
			if (enemy == true)
				enemyFire(ammoLaunchPos, shooter);
			else
				StartCoroutine (launchMissiles (ammoLaunchPos, shooter));
		}
		else
			swipe (ammoLaunchPos, shooter);
	}

	public void enemyFire(Vector3 ammoLaunchPos, Transform shooter){
		if (t >= coolDown){
			fireSound.Play ();
			GameObject.Instantiate (a, ammoLaunchPos, shooter.rotation);
			t = 0;
		} 
	}

	public IEnumerator launchMissiles(Vector3 ammoLaunchPos, Transform shooter){
		for (int i = 0; i < ammoPerShot; i++){
			if (ammo > 0) {
				fireSound.Play ();
				GameObject.Instantiate (a, ammoLaunchPos, shooter.rotation);
				ammo--;
			} else 
				drySound.Play();
			yield return new WaitForSeconds (0.05f);
		}
	}

	public void swipe(Vector3 ammoLaunchPos, Transform shooter){
		fireSound.Play ();
		GameObject.Instantiate (a, ammoLaunchPos, shooter.rotation);
	}

}
