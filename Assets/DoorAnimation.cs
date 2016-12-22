using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {

	public bool requireKey;
	public AudioClip doorSwitchClip;
	public AudioClip accessDeniedClip;

	private Animator anim;
	private GameObject player;
	private PlayerInventory playerInventory;
	private AudioSource audio;
	private int count;

	void Awake() {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInventory = player.GetComponent<PlayerInventory> ();
		audio = GetComponent<AudioSource> ();

	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			if (requireKey) {
				if (playerInventory.hasKey) {
					count++;
				} else {
					audio.clip = accessDeniedClip;
					audio.Play ();

				}
			} else {
				count++;
			}
		}

	}
	void onTriggerExit(Collider other) {
		if (other.gameObject == player) {
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
