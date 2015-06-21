using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointScript : MonoBehaviour {
	public CheckpointScript next;
	public List<EnemyScript> startingPatrols = new List<EnemyScript>();

	// Use this for initialization
	void Start () {
		foreach (EnemyScript en in startingPatrols) {
			en.Patrol(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		EnemyScript e = other.gameObject.GetComponent<EnemyScript> ();
		if (e) {
			if (next) {
				e.Patrol(next.gameObject);
			}
			else {
				e.Idle();
			}
		}
	}
}
