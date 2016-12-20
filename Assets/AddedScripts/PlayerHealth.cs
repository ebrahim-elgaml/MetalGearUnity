using UnityEngine;
using System.Collections;


public class PlayerHealth : MonoBehaviour {

	public float health = 100f;

	public float resetAfterDeathTime = 5f;
	public AudioClip deathCLip;

	private Animator anim;
	private SceneFadeInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private float timer;
	private bool playerDead;

	void Awake() {
		anim = GetComponent<Animator> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();


	}

	void playerDying() {
		playerDead = true;
		anim.SetBool("Dead", playerDead);
		AudioSource.PlayClipAtPoint (deathCLip, transform.position);
	}

	void PlayerDead() {
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
