using UnityEngine;
using System.Collections;

public class NavGraphScript : MonoBehaviour {
	NavNodeScript[] nodes;

	// Use this for initialization
	void Start () {
		nodes = GetComponentsInChildren<NavNodeScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public NavNodeScript Nearest(Vector3 position) {
		NavNodeScript closest = null;
		float distance = Mathf.Infinity;
		foreach (NavNodeScript node in nodes) {
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
