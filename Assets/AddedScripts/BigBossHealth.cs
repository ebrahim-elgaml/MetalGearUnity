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
	private MenuHandeler menu;



	void Awake() {
		menu = GameObject.FindGameObjectWithTag (Tags.menu).GetComponent<MenuHandeler> ();

		anim = GetComponent<Animator> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}

	void playerDying() {
		isDead = true;
//		GetComponent<CapsuleCollider>().radius = 0.2f;
//		GetComponent<CapsuleCollider>().direction = 0;
		anim.SetBool("Dead", isDead);
		AudioSource.PlayClipAtPoint (deathCLip, transform.position);
	}

	void LevelReset() {

		timer += Time.deltaTime;

		if (timer >= resetAfterDeathTime) {
			//			sceneFadeInOut.EndScene ();
			menu.Pause ();
		}
	}

	void PlayerDead() {
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		anim.SetBool("Dead", isDead);
		if (health <= 0f) {
			if (!isDead) {
				playerDying ();
			} else {
				PlayerDead ();
				LevelReset ();
			}


		}
		if (isDead) transform.position = new Vector3 (transform.position.x, -1, transform.position.z);
	}

	public void TakeDamage(float amount) {
		health -= amount;
	}
}
