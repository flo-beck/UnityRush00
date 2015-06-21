using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

	public Player player;
	public EnemyScript[] enemies;
	public GameObject endGamePanel;
	public Text winLoseText;
	public Text WeaponText;
	public Text AmmoText;
	string noWeapon = "NO WEAPON";

	// Use this for initialization
	void Start ()
	{
		enemies =  GetComponents<EnemyScript>();
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (player) {
			if (!player.gameObject.activeSelf)
				setGameLose ();
			else if (enemies.Length == 0)  //OR REACH TRIGGER END GAME POINT
				setGameWin ();


			if (player.weapon) {
				Weapon w = player.weapon;
				WeaponText.text = w.weaponName;
				if (w.type == Weapon.weaponType.GUN) {
					AmmoText.text = w.ammo.ToString ();
				} else {
					AmmoText.text = "-";
				} 
			} else {
				WeaponText.text = noWeapon;
				AmmoText.text = "-";
			}
		}
	}

	private void setGameLose ()
	{
		winLoseText.text = "GAME\nOVER";
		endGamePanel.SetActive (true);
		player.gameObject.SetActive (false);
	}

	private void setGameWin ()
	{
		winLoseText.text = "YOU\nWIN";
		endGamePanel.SetActive (true);
		Destroy (player.gameObject);
	}

	public void restartGame ()
	{ 
		Application.LoadLevel (1);
	}

	public void backToMenu ()
	{ 
		Application.LoadLevel (0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "player") {	
			setGameWin ();
		}
	}

}
