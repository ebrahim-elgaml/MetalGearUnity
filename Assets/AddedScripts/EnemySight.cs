using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemySight : MonoBehaviour {
	public float fieldOfVeiwAngle = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;

	private NavMeshAgent nav;
	private SphereCollider col;
	private Animator anim;
	private LastPlayerSighting lastPlayerSighting;
	private GameObject player;
	private Animator playerAnim;
	private PlayerHealth playerHealth;
	private Vector3 previousSighting;
	private ThirdPersonCharacter thirdPersonCharacter;
	public float lastSpeed = 0f;
	public float lastAngularSpeed = 0f;

	void Awake () 
	{
		nav = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
		anim = GetComponent<Animator> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerAnim = player.GetComponent<Animator> ();
		playerHealth = player.GetComponent<PlayerHealth> ();
		thirdPersonCharacter = player.GetComponent<ThirdPersonCharacter> ();
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(lastPlayerSighting.position != previousSighting){
			personalLastSighting = lastPlayerSighting.position;
		}
		 
		previousSighting = lastPlayerSighting.position;

		if ( playerHealth.health > 0f) {
			anim.SetBool ("PlayerInSight", playerInSight);
		} else {
			anim.SetBool ("PlayerInSight", false);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player) {
			playerInSight = false;
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);
			if (angle < fieldOfVeiwAngle * 0.5f) {
				RaycastHit hit;
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)){
					if(hit.collider.gameObject == player){
						playerInSight = true;
						if (anim.GetFloat ("Speed") > 0) {
							lastSpeed = anim.GetFloat ("Speed");
							lastAngularSpeed = anim.GetFloat ("AngularSpeed");
						}

						anim.SetFloat ("Speed", 0f);
						anim.SetFloat ("AngularSpeed", 0f);
						lastPlayerSighting.position = player.transform.position;
					}
				}
			}
				
			if (thirdPersonCharacter.isShouting) {
				if (CalculatePathLength (player.transform.position) <= col.radius) {
					personalLastSighting = player.transform.position;
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			playerInSight = false;
			anim.SetFloat ("Speed", lastSpeed);
			anim.SetFloat ("AngularSpeed", lastAngularSpeed);
		} 
	}
	float CalculatePathLength (Vector3 targetPosition)
	{
		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled)
			nav.CalculatePath(targetPosition, path);

		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3 [] allWayPoints = new Vector3[path.corners.Length + 2];

		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;

		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetPosition;

		// The points inbetween are the corners of the path.
		for(int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}

		// Create a float to store the path length that is by default 0.
		float pathLength = 0;

		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for(int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}

		return pathLength;
	}

}
