using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerHide : MonoBehaviour {
	private ThirdPersonCharacter th;
	private bool isHiding = false;
	// Use this for initialization
	void Start () {
		th = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<ThirdPersonCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
		float x = GameObject.FindGameObjectWithTag (Tags.player).transform.position.x;
		float z =  GameObject.FindGameObjectWithTag (Tags.player).transform.position.z;
		if(Input.GetKeyDown(KeyCode.H)) {
			if(isHiding){
				th.isHiding = false;
				isHiding = false;
				transform.position = new Vector3(x, 20, z);
			} else {
				th.isHiding = true;
				isHiding = true;
				transform.position = new Vector3(x, 0, z);
			}
		}

		if(isHiding){
			transform.position = new Vector3(x, 0, z);
		} else {
			transform.position = new Vector3(x, 4, z);
		}
	}
}
