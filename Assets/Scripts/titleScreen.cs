using UnityEngine;
using System.Collections;

public class titleScreen : MonoBehaviour {

	//public float speed = 10.0f;
	public float maxTime = 5f;

	private Color bgColor;
	private float t;


	public Color[] colours;
	private int newColourIndex;


	// Use this for initialization
	void Start () {
		//StartCoroutine (bgColorShifter ());
		newColourIndex = Random.Range(0, colours.Length);
	}
	
	// Update is called once per frame
	void Update () {

		//set colourChange background
//		float t = (Mathf.Sin(Time.time * speed) + 1f) / 2.0f;
//		Camera.main.backgroundColor = Color.Lerp( new Color(0.82f,0f,1f,1f), new Color(1f,0.83f,0f,1f), t);


		Color currentColor = Camera.main.backgroundColor;
		if (t < maxTime) {
			Camera.main.backgroundColor = Color.Lerp (currentColor, colours[newColourIndex], t / maxTime);
		} else {
			int nextColour = Random.Range(0, colours.Length);
			while (nextColour == newColourIndex)
				nextColour = Random.Range(0, colours.Length);
			t = 0f;
			newColourIndex = nextColour;
		}

		t += Time.deltaTime;
	}

	public void startGame(){ 
		Application.LoadLevel (1);
	}

	public void exit(){
		Application.Quit ();
	}
	
	IEnumerator bgColorShifter()
	{
		Color bgColor;
		while (true)
		{ 
			Debug.Log("IN TRUE");
			bgColor = new Color(Random.value, Random.value, Random.value, 1.0f);
			
			float t = 0f;
			Color currentColor = Camera.main.backgroundColor;
			
			while( t < 1.0 )
			{
				Debug.Log("IN WHILE - t: " + t);
				
				Camera.main.backgroundColor = Color.Lerp(currentColor, bgColor, t );
				t += Time.deltaTime;
				yield break;
			}
		}
	}
	
}
