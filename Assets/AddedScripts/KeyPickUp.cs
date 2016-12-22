using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class KeyPickUp : MonoBehaviour {

	public AudioClip keyGrab;
	private GameObject player;
	private PlayerInventory playerInventory;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInventory =  player.GetComponent<PlayerInventory> ();

	}

	void OnTriggerEnter(Collider other) {
		print ("ENter");
		print (other.gameObject);
		print (player);
		if (other.gameObject == player) {
			print ("ENter if ");
			AudioSource.PlayClipAtPoint (keyGrab, transform.position);
			playerInventory.hasKey = true;
			Destroy (gameObject);

		}
	}



}
