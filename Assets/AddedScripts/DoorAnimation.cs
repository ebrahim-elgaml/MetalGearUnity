using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorAnimation : MonoBehaviour {

	public bool requireKey;
	public AudioClip doorSwitchClip;
	public AudioClip accessDeniedClip;

	private Animator anim;
	private GameObject player;
	private PlayerInventory playerInventory;
	private AudioSource audio;
	private int count;
	public Text keyStatus;

	public bool isYellow;
	public bool isBlue;
	void Awake() {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory = player.GetComponent<PlayerInventory> ();
		audio = GetComponent<AudioSource> ();

	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			if (requireKey) {
				bool f = false;
				if (isYellow)
					f = playerInventory.hasYellowKey && playerInventory.openBlue && playerInventory.yellowSelected;
				if (isBlue)
					f = playerInventory.hasBlueKey && playerInventory.blueSelected;
				if (f) {
					if( isYellow) SceneManager.LoadScene ("LastLevel", LoadSceneMode.Single);
					if (isBlue) {
						keyStatus.text = "No key selected"; 
						playerInventory.hasBlueKey = false;
						playerInventory.blueSelected = false;
						playerInventory.openBlue = true;
					}
						
					count++;
				} else {
					audio.clip = accessDeniedClip;
					audio.Play ();

				}
			} else {
				count++;
			}
		} else {
			if (other.gameObject.tag == Tags.enemy) {
				if (other is CapsuleCollider) {
					count++;
				}
			}
		}

	}


	void onTriggerExit(Collider other) {
		if (other.gameObject == player || (other.gameObject.tag == Tags.enemy || other is CapsuleCollider)) {
			count = Mathf.Max (0, count - 1);

		}
	}
	void Update(){
		anim.SetBool ("Open", count > 0);
		if (anim.IsInTransition (0) && !audio.isPlaying) {
			audio.clip = doorSwitchClip;
			audio.Play ();
		}
	}
}
