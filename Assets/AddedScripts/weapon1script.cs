using UnityEngine;
using System.Collections;

public class weapon1script : MonoBehaviour {
	public AudioClip keyGrab;
	private GameObject player;
	private PlayerInventory playerInventory;

	// Use this for initialization
	void Start () {

	}
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory =  player.GetComponent<PlayerInventory> ();

	}
	void OnTriggerEnter(Collider other) {

		if (other.gameObject == player) {
			AudioSource.PlayClipAtPoint (keyGrab, transform.position);
			playerInventory.hasWeapon1 = true;
			Destroy (gameObject);

		}
	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up *20* Time.deltaTime, Space.World);

	}
}
