using UnityEngine;
using System.Collections;

public class EnemyAnimatorSetup {

	public float speedDampTime = 0.1f;
	public float angularSpeedDampTime = 0.7f;
	public float angleResponseTime = 0.6f;

	private Animator anim;

	public EnemyAnimatorSetup(Animator animator){
		anim = animator;
	}

	public void Setup(float speed, float angular){
		float angularSpeed = angleResponseTime / angleResponseTime;
		anim.SetFloat ("Speed", speed, speedDampTime, Time.deltaTime);
		anim.SetFloat("AngularSpeed", angularSpeed, angularSpeedDampTime, Time.deltaTime);
	}
}
