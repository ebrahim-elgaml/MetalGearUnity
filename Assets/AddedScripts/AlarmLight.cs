using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

	public float fadeSpeed = 2f;
	public float highIntensity = 2f;
	public float lowIntensity = 0.5f;
	public float changeMargin = 0.2f;
	public bool alarmOn;

	private float targetIntensity;

//	Light light;

	void Awake () {
		GetComponent<Light> ().intensity = 0f;
		targetIntensity = highIntensity;
	}
	// Use this for initialization
//	void Start () {
//		light = GetComponent<Light> ();
//	}
	
	// Update is called once per frame
	void Update () {
		float f = fadeSpeed * Time.deltaTime;
		if (alarmOn) {
			GetComponent<Light> ().intensity = Mathf.Lerp (GetComponent<Light> ().intensity, targetIntensity, f);
		} else {
			GetComponent<Light> ().intensity = Mathf.Lerp (GetComponent<Light> ().intensity, 0f, f);

		}
	}

	void checkTargetIntenstity() {
		if (Mathf.Abs (targetIntensity - GetComponent<Light> ().intensity) < changeMargin) {
			if (targetIntensity == highIntensity) {
				targetIntensity = lowIntensity;
			} else {
				targetIntensity = highIntensity;
			}

		}
	}
}
