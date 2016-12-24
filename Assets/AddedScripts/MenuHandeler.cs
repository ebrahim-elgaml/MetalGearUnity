using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class MenuHandeler : MonoBehaviour {

	public GameObject canvas;
	bool isPaused;
	private GameObject player;
	private PlayerInventory playerInventory;

	public Button resume;
	public Button quit;
	public Button restart;
	public RawImage blueKey;
	public RawImage yellowKey;
	public RawImage weapon;
	public AudioClip clickSound;
	public Text keyStatus;


	// Use this for initialization
	void Start () {
		bool isMute = PlayerPrefs.GetInt ("mute") == 1;
		if (isMute) {
			AudioListener.volume = 0;
		}

		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory =  player.GetComponent<PlayerInventory> ();
		canvas.SetActive(false);
		if(blueKey)
			blueKey.color = Color.black;
		if(yellowKey)
			yellowKey.color = Color.black;
		if(weapon)
			weapon.color = Color.black;
		resume.onClick.AddListener(() => resumeGame());
		quit.onClick.AddListener(() => quitGame());
		restart.onClick.AddListener(() => restartGame());
		if (blueKey) {
			blueKey.GetComponent<Button> ().onClick.AddListener (() => onBlueClick ());
			yellowKey.GetComponent<Button> ().onClick.AddListener (() => onYellowCLick ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<ThirdPersonCharacter> ().isDead) {
			resume.gameObject.SetActive (false);
		}
		if(Input.GetKeyDown(KeyCode.Escape) && !isPaused){
			isPaused = true;
			canvas.SetActive (true);
			Time.timeScale = 0;
		}
		if (blueKey) {
			if (playerInventory.hasWeapon1)
				weapon.color = Color.white;
			if (playerInventory.hasYellowKey)
				yellowKey.color = Color.white;
			if (playerInventory.hasBlueKey)
				blueKey.color = Color.white;
		}

	
	}

	void resumeGame() {
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);
		isPaused = false;
		Time.timeScale = 1.0f;
		canvas.SetActive (false);
	}

	void quitGame() {
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);
		Application.LoadLevel (0);

	
	}

	void onBlueClick() {
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);
		if (playerInventory.hasBlueKey) {
			keyStatus.text = " Blue Key is selected";
			playerInventory.blueSelected = true;
			playerInventory.yellowSelected = false;
		}
	}

	void onYellowCLick() {
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);
		if (playerInventory.hasYellowKey) {
			keyStatus.text = " Yellow Key is selected";
			playerInventory.blueSelected = false;
			playerInventory.yellowSelected = true;
		}

	}


	void restartGame() {
		AudioSource.PlayClipAtPoint (clickSound, player.transform.position);

		Application.LoadLevel (1);
	}
		

	public void Pause() {
		isPaused = true;
		canvas.SetActive (true);
		Time.timeScale = 0f;
		resume.gameObject.SetActive (false);
	
	}


}
