using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {


	public bool hasWeapon;
	public float maxDamage = 120f;
	public float minDamage = 40f;
	public AudioClip shotCLip;
	public float flashIntenstity = 3f;
	public float fadeSpeed = 10f;

	private Animator anim;
	private LineRenderer laserShotLine;
	private Light laserShotLight;
	private SphereCollider col;
	private bool shooting;
	private float scaledDamage;
	private BigBossHealth bigBossHealth;
	public GameObject target;
	public float rotateSpeed = 5;
	Vector3 offset;

	void Awake(){
		GameObject.FindGameObjectWithTag (Tags.bigBoss);
		bigBossHealth = GameObject.FindGameObjectWithTag (Tags.bigBoss).GetComponent<BigBossHealth> ();
		anim = GetComponent<Animator> ();
		laserShotLine = GetComponentInChildren<LineRenderer> ();
		laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
		col = GetComponent<SphereCollider> ();
		laserShotLine.enabled = false;
		laserShotLight.intensity = 0f;
		scaledDamage = maxDamage - minDamage;
	}
	// Use this for initialization
	void Start () {
		offset = target.transform.position - transform.position;
	}

	// Update is called once per frame
	void Update () {
		anim.SetBool ("HasWeapon", hasWeapon);
		anim.SetBool ("IsShooting", shooting);
//		float shot = anim.GetFloat ("Shot");
		if (Input.GetMouseButtonDown (0))
			Shoot ();
		else {
			shooting = false;
			laserShotLine.enabled = false;
		}
		laserShotLight.intensity = Mathf.Lerp (laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);

	}

	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);
	}

	void OnAnimatorIK(int layerIndex) {
		float aimW = anim.GetFloat ("AimWeight");
		anim.SetIKPosition (AvatarIKGoal.RightHand, target.transform.position + Vector3.up * 1.5f);
		anim.SetIKPositionWeight (AvatarIKGoal.RightHand, aimW);
	}

	void Shoot()
	{

		RaycastHit hitInfo = new RaycastHit();
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo) && hitInfo.transform.tag == Tags.bigBoss) {
			print ("It's working");
			shooting = true;
			float fracDistance = (col.radius - Vector3.Distance (transform.position, target.transform.position)) / col.radius;
			float damage = scaledDamage * fracDistance + minDamage;
			bigBossHealth.TakeDamage (damage);
			ShotEffect ();
		} else {
			shooting = false;
		}

	}

	void ShotEffect() 
	{
		laserShotLine.SetPosition (0, laserShotLine.transform.position);
		laserShotLine.SetPosition (1, target.transform.position + Vector3.up * 1.5f);
		laserShotLine.enabled = true;
		laserShotLight.intensity = flashIntenstity;
		AudioSource.PlayClipAtPoint (shotCLip, laserShotLight.transform.position);


	}

}
