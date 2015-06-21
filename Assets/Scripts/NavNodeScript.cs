using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavNodeScript : MonoBehaviour {
	public List<NavNodeScript> neighbors = new List<NavNodeScript>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public NavNodeScript Nearest(Vector3 position) {
		NavNodeScript closest = null;
		float distance = Mathf.Infinity;
		foreach (NavNodeScript node in neighbors) {
			Vector2 diff = (Vector2)node.transform.position - (Vector2)position;
			float dist = diff.sqrMagnitude;
			if (distance > dist) {
				distance = dist;
				closest = node;
			}
		}
		return closest;
	}
}
