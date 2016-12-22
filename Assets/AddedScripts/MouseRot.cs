using UnityEngine;
using System.Collections;

public class MouseRot : MonoBehaviour {

	public GameObject target;
	public float rotateSpeed = 5;
	Vector3 offset;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		offset = target.transform.position - transform.position;
	}
	
	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
//		target.transform.Rotate(0, horizontal, 0);
		target.transform.Rotate(0, horizontal, 0);


	}
}
