using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollow : MonoBehaviour
{

	public Transform followTarget;
	private float initialZ;

	void Start()
	{
		initialZ = transform.position.z;
	}
	
	void Update () {
		FollowTarget();
	}

	void FollowTarget()
	{
		if (followTarget == null) return;

		transform.position = followTarget.position + (Vector3.forward * initialZ);
	}
}
