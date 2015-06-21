using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{

	public Player player;
	public Enemy[] enemies;
	public GameObject endGamePanel;
	public Text winLoseText;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (player && !player.gameObject.activeSelf) {
			setGameLose ();
		} else if (enemies.Length == 0) {  //OR REACH TRIGGER END GAME POINT
			//setGameWin ();
		}
	}

	private void setGameLose ()
	{
		winLoseText.text = "GAME\nOVER";
		endGamePanel.SetActive (true);
		Destroy (player.gameObject);
	}

	private void setGameWin ()
	{
		winLoseText.text = "YOU\nWIN";
		endGamePanel.SetActive (true);
		Destroy (player.gameObject);
	}

	public void restartGame(){ 
		Application.LoadLevel (1);
	}

	public void backToMenu(){ 
		Application.LoadLevel (0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "player") {	
			setGameWin ();
		}
	}

}
