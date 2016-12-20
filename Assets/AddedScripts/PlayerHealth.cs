using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;


public class PlayerHealth : MonoBehaviour {

	public float health = 100f;

	public float resetAfterDeathTime = 10f;
	public AudioClip deathCLip;

	private Animator anim;
	private SceneFadeInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private ThirdPersonCharacter th;

	private float timer;
	private bool playerDead = false;

	void Awake() {
		anim = GetComponent<Animator> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		th = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<ThirdPersonCharacter> ();



	}

	void playerDying() {
		playerDead = true;
		GetComponent<CapsuleCollider>().radius = 0.2f;
		GetComponent<CapsuleCollider>().direction = 0;
		th.isDead = true;
		anim.SetBool("Dead", playerDead);
		AudioSource.PlayClipAtPoint (deathCLip, transform.position);
	}

	void PlayerDead() {
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		GetComponent<AudioSource> ().Stop ();

	}

	void LevelReset() {
		timer += Time.deltaTime;

		if (timer >= resetAfterDeathTime) {
			sceneFadeInOut.EndScene ();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0f) {
			if (!playerDead) {
				playerDying ();
			} else {
				PlayerDead ();
				LevelReset ();
			}
		}
	}

	public void TakeDamage(float amount) {
		health -= amount;
	}
}
