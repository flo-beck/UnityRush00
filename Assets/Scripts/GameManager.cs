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

		if (!player.gameObject.activeSelf) {
			setGameLose ();
		} else if (enemies.Length == 0) {  //OR REACH TRIGGER END GAME POINT
			//setGameWin ();
		}

		//set flashing background

	}

	private void setGameLose ()
	{
		winLoseText.text = "GAME\nOVER";
		endGamePanel.SetActive (true);
	}

	private void setGameWin ()
	{
		winLoseText.text = "YOU\nWIN";
		endGamePanel.SetActive (true);
	}

	public void restartGame(){ 
		Application.LoadLevel (1);
	}

	public void backToMenu(){ 
		Application.LoadLevel (0);
	}
	
}
