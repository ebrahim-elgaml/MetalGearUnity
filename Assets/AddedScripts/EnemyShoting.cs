using UnityEngine;
using System.Collections;

public class EnemyShoting : MonoBehaviour {

	public float maxDamage = 120f;
	public float minDamage = 40f;
	public AudioClip shotCLip;
	public float flashIntenstity = 3f;
	public float fadeSpeed = 10f;

	private Animator anim;
	private LineRenderer laserShotLine;
	private Light laserShotLight;
	private SphereCollider col;
	private Transform player;
	private PlayerHealth playerHealth;
	private bool shooting;
	private float scaledDamage;
	private BigBossHealth bigBossHealth;

	void Awake(){
		bigBossHealth = GetComponent<BigBossHealth> ();
		anim = GetComponent<Animator> ();
		laserShotLine = GetComponentInChildren<LineRenderer> ();
		laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
		col = GetComponent<SphereCollider> ();
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		playerHealth = player.gameObject.GetComponent<PlayerHealth> ();

		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;
		scaledDamage = maxDamage - minDamage;

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float shot = anim.GetFloat ("Shot");
		if (bigBossHealth.health <= 0f)
			return;
		if (shot > 0.5f && !shooting) {
			Shoot ();
		}
		if (shot < 0.5f) {
			shooting = false;
			laserShotLine.enabled = false;
		}
		laserShotLight.intensity = Mathf.Lerp (laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);

	}
	void OnAnimatorIK(int layerIndex) {
		float aimW = anim.GetFloat ("AimWeight");
		anim.SetIKPosition (AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
		anim.SetIKPositionWeight (AvatarIKGoal.RightHand, aimW);
	}

	void Shoot()
	{
		shooting = true;
		float fracDistance = (col.radius - Vector3.Distance (transform.position, player.position)) / col.radius;
		float damage = scaledDamage * fracDistance + minDamage;
		playerHealth.TakeDamage (damage);
		ShotEffect ();
	}

	void ShotEffect() 
	{
		laserShotLine.SetPosition (0, laserShotLine.transform.position);
		laserShotLine.SetPosition (1, player.position + Vector3.up * 1.5f);
		laserShotLine.enabled = true;
		laserShotLight.intensity = flashIntenstity;
		AudioSource.PlayClipAtPoint (shotCLip, laserShotLight.transform.position);


	}
}
