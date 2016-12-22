using UnityEngine;
using System.Collections;

public class BigBossHealthBar : MonoBehaviour {

	private BigBossHealth bigBossHealth;
	private float org;
	private float health = 200;
	// Use this for initialization
	void Start () {
		bigBossHealth = GetComponent<BigBossHealth> ();
		health = bigBossHealth.health;
		org = transform.localScale.x;
	}

	// Update is called once per frame
	void Update () {
		if (bigBossHealth.health >= 0f)
			transform.localScale = new Vector3(org * bigBossHealth.health / health, transform.localScale.y, transform.localScale.y);
		else 
			transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.y);
	}
}
