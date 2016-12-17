using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {
	public float fadeSpeed = 1.5f;
	private bool sceneStarting = true;

	void Awake () {
		GetComponent<GUITexture> ().pixelInset = new Rect (0f, 0f, Screen.width, Screen.height); 
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (sceneStarting) {
			StartScene ();
		}
	}

	void FadeToClear ()
	{
		GetComponent<GUITexture> ().color = Color.Lerp (GetComponent<GUITexture> ().color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack ()
	{
		GetComponent<GUITexture> ().color = Color.Lerp (GetComponent<GUITexture> ().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	void StartScene() {
		FadeToClear ();
		if (GetComponent<GUITexture> ().color.a <= 0.05) {
			float g = GetComponent<GUITexture> ().color.g;
			float b = GetComponent<GUITexture> ().color.b;
			GetComponent<GUITexture> ().color = new Color (Color.clear.r, g, b);
			GetComponent<GUITexture> ().enabled = false;
			sceneStarting = false;
		}
	}

	public void EndScene() {
		GetComponent<GUITexture> ().enabled = true;
		FadeToBlack ();
		if (GetComponent<GUITexture> ().color.a >= 0.95f) {
			Application.LoadLevel (1);
		}
	}
}
