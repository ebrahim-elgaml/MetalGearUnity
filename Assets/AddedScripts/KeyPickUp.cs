using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class KeyPickUp : MonoBehaviour {

	public AudioClip keyGrab;
	private GameObject player;
	private PlayerInventory playerInventory;
	public bool isYellow;
	public bool isBlue;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory =  player.GetComponent<PlayerInventory> ();
	}

	void Update () {
		transform.Rotate(Vector3.up *20* Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			AudioSource.PlayClipAtPoint (keyGrab, transform.position);
			if(isYellow)
				playerInventory.hasYellowKey = true;
			if(isBlue)
				playerInventory.hasBlueKey = true;
			
			Destroy (gameObject);

		}
	}



}
