using UnityEngine;
using System.Collections;

public class liftTrigger : MonoBehaviour {
	public float timeToDoorsClose = 2f;
	public float timeToLiftStart = 3f;
	public float timeToEndLevel = 6f;
	public float liftSpeed = 3f;

	private GameObject player;
	private Animator PlayerAnim;
	private SceneFadeInOut sceneFadeInOut;
	private LiftDoorsTracking liftDoorsTracking;
	private bool playerInLift;
	private float timer;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		PlayerAnim = player.GetComponent<Animator> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		liftDoorsTracking = GetComponent<LiftDoorsTracking> ();

	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			playerInLift = true;
			timer = 0f;
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			playerInLift = false;
			timer = 0f;
		}
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void LiftActivation(){
		timer += Time.deltaTime;
		if (timer >= timeToLiftStart) {
//			PlayerAnim.SetFloat (Hash128.speedFloat, 0f);

		}
	}

}
