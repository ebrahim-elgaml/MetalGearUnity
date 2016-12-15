using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class script : MonoBehaviour {
	public Button game;
	public Button options;
	public Button mute;
	public Text description;
	public Text creds;
	bool isMute = false;
	bool isOptions = false;
	// Use this for initialization

	public AudioClip gameSound;
	public AudioClip clickSound;


	private AudioSource source;
	void Start () {
		mute.gameObject.SetActive (false);
		description.gameObject.SetActive (false);
		creds.gameObject.SetActive (false);
		game.onClick.AddListener(() => gameClick());
		options.onClick.AddListener(() => optionsClick());
		mute.onClick.AddListener(() => muteClick());
		playSound (gameSound);
	}
	void Awake () {
		source = GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update () {
	
	}

	void gameClick() {
		source.Stop ();
		playSound (clickSound);
		PlayerPrefs.SetInt ("mute", isMute? 1 : 0);
		SceneManager.LoadScene ("FirstLevel", LoadSceneMode.Single);
	}

	void optionsClick() {
		playSound (clickSound);
		isOptions = !isOptions;
		mute.gameObject.SetActive (isOptions);
		description.gameObject.SetActive (isOptions);
		creds.gameObject.SetActive (isOptions);
	}

	void muteClick() {
		playSound (clickSound);
		if (isMute) {
			isMute = false;
			mute.GetComponentInChildren<Text> ().text = "Mute";

		} else {
			
			isMute = true;
			mute.GetComponentInChildren<Text> ().text = "unMute";
		}
		source.mute = isMute;
	}
	void playSound(AudioClip c) {
		if(!isMute)
			source.PlayOneShot (c);
	}
}
