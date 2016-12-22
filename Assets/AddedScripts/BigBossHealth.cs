using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;


public class BigBossHealth : MonoBehaviour {

	public float health = 200;

	public float resetAfterDeathTime = 10f;
	public AudioClip deathCLip;

	private Animator anim;
	private SceneFadeInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private ThirdPersonCharacter th;

	private float timer;
	public bool isDead = false;

	GameObject healthProgress;


	void Awake() {
		anim = GetComponent<Animator> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}

	void playerDying() {
		isDead = true;
		GetComponent<CapsuleCollider>().radius = 0.2f;
		GetComponent<CapsuleCollider>().direction = 0;
		anim.SetBool("Dead", isDead);
		AudioSource.PlayClipAtPoint (deathCLip, transform.position);
	}

	void PlayerDead() {
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		GetComponent<AudioSource> ().Stop ();

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (health <= 0f) {
			if (!isDead) {
				playerDying ();
			} else {
				PlayerDead ();
			}
		}
	}

	public void TakeDamage(float amount) {
		health -= amount;
	}
}
