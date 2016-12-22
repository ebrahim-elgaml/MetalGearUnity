﻿using UnityEngine;
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
					f = playerInventory.hasYellowKey && playerInventory.openBlue;
				if (isBlue)
					f = playerInventory.hasBlueKey;
				if (f) {
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
