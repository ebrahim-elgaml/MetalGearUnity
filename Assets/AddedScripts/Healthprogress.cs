using UnityEngine;
using System.Collections;

public class Healthprogress : MonoBehaviour {

	private PlayerHealth playerHealth;
	private float org;
	private float health = 100;
	// Use this for initialization
	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag (Tags.player).gameObject.GetComponent<PlayerHealth> ();
		org = transform.localScale.x;
		health = playerHealth.health;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth.health >= 0f)
			transform.localScale = new Vector3(org * playerHealth.health / health, transform.localScale.y, transform.localScale.y);
		else 
			transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.y);
	}
}
