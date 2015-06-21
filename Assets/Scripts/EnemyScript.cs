using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody))]

public class EnemyScript : MonoBehaviour
{
	public enum State
	{
		IDLE,
		FIGHT,
		PATROL,
		MOVE
	}

	public enum Layers
	{
		WALLS = 8,
		PLAYER = 9,
		GROUND = 11,
		CHECKPOINT = 12,
		SOUND = 10
	}

	public float speed = 30f;
	public float searchTimeout = 5f;
	public GameObject target;
	public State state;
	public NavGraphScript navMesh;
	public NavNodeScript currentNode;

	Vector2 dir;
	Vector3 destination;

	float timeout;
	bool willTimeout;

	public SpriteRenderer weaponImg;
	public Weapon weapon;
	public GameObject ammoLaunchPos;
	
	// Use this for initialization
	void Start ()
	{
		if (weapon) {
			weaponImg.sprite = weapon.heldImg;
			weapon.gameObject.SetActive(true);
			weapon.gameObject.layer = gameObject.layer;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (state == State.FIGHT) {
			if (target && SeeObject (target.transform.position, 1 << 14) == target) {
				FightStep();
			} else {
				state = State.MOVE;
				SearchStart (true);
			}
		} else if (state == State.MOVE)
			SearchStep ();
		else if (state == State.PATROL) {
			if (target)
				SearchStep ();
			else
				Idle ();
		}
	}

	void SearchStart(bool to) {
		if (!target) {
			Idle ();
			return ;
		}
		//state = State.MOVE;
		timeout = searchTimeout;
		willTimeout = to;
		//SearchNextDoor ();
		GetComponentInChildren<Animator> ().SetTrigger ("walk");

		currentNode = navMesh.Nearest(transform.position);
	}

	void GoToNextNode() {
		/*GameObject door = ClosestDoor (target.transform.position);
		if (door) {
			SetDestination (door.transform.position);*/
		if (currentNode)
			currentNode = currentNode.Nearest(target.transform.position);
		if (currentNode) {
			SetDestination(currentNode.transform.position);
		} else {
			Idle();
		}
	}

	void SearchStep () {
		if (target == SeeObject (target.transform.position, (1 << 9))
		    || target == SeeObject (target.transform.position, (1 << 12))) {
			destination = target.transform.position;
			Step ();
			return ;
		}
		dir = (Vector2)destination - (Vector2)transform.position;
		if (dir.magnitude < 0.2f)
			GoToNextNode ();
		else if (willTimeout) {
			if (timeout < Time.deltaTime)
				Idle ();
			else {
				Step ();
				timeout -= Time.deltaTime;
			}
		}
	 }

	void FightStep() {
		SetDestination (target.transform.position);
		Step ();
		weapon.fire ( ammoLaunchPos.transform.position, transform);

	}

	void Step ()
	{
		dir = (Vector2)destination - (Vector2)transform.position;
		dir.Normalize ();

		SetRotation (-dir);
		GetComponent<Rigidbody2D> ().AddForce (dir * speed, ForceMode2D.Force);
	}

	void SetDestination (Vector2 dst)
	{
		GetComponentInChildren<Animator> ().SetTrigger ("walk");
		destination = dst;
		dir = dst - (Vector2)transform.position;
	}

	void SetRotation (Vector2 dir)
	{
		transform.rotation = Quaternion.LookRotation (Vector3.forward, dir);
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.layer != 14)
			return ;
		GameObject p = SeeObject (other.gameObject.transform.position, 1 << 14);
		if (p) {
			target = p;
			state = State.FIGHT;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 10) {
			Player player = other.gameObject.GetComponentInParent<Player>();
			if (player) {
				target = player.gameObject;
				state = State.FIGHT;
			}
		}
	}

	GameObject SeeObject (Vector3 position, int layer)
	{
		RaycastHit2D hit = Physics2D.Linecast (transform.position, position, (1 << 8) + layer);
		if (hit != null && hit.collider != null) {
			if (((1 << hit.collider.gameObject.layer) & layer) != 0)
			    return hit.collider.gameObject;
			else
				return null;
		}
		return null;
	}

	/*GameObject ClosestDoor (Vector3 position)
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, 100, 1 << 11);
		if (hit != null && hit.collider != null && target != hit.collider.gameObject) {
			List<GameObject> doors = hit.collider.GetComponent<roomScript> ().doors;

			GameObject closest = null;
			float distance = Mathf.Infinity;
			foreach (GameObject door in doors) {
				Vector2 diff = (Vector2)door.transform.position - (Vector2)position;
				float dist = diff.sqrMagnitude;
				if (distance > dist) {
					distance = dist;
					closest = door;
				}
			}
			return closest;
		}
		return null;
	}*/

	public void Idle() {
		target = null;
		GetComponentInChildren<Animator> ().SetTrigger ("stopWalk");
		state = State.IDLE;
	}

	public void Move(GameObject g) {
		state = State.MOVE;
		target = g;
		SetDestination (g.transform.position); // try to remove
		SearchStart (false);
	}

	public void Patrol(GameObject g) {
		if (state == State.PATROL || state == State.IDLE) {
			target = g;
			SearchStart(false);
			state = State.PATROL; // try to remove
		}
	}
}
