using UnityEngine;
using System.Collections;

public class weapon2script : MonoBehaviour {
	public AudioClip keyGrab;
	private GameObject player;
	private PlayerInventory playerInventory;

	// Use this for initialization
	void Start () {
	
	}
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
			playerInventory.hasWeapon2 = true;
			Destroy (gameObject);

		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up *20* Time.deltaTime, Space.World);
	
	}
}
