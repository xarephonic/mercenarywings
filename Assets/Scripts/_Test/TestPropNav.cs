using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPropNav : MonoBehaviour {

	public GameObject target;
	public Vector3 targetPos;
	public Vector3 prevTargetPos;
	public Vector3 directionToTarget;
	public Vector3 prevDirectionToTarget;
	public Vector3 losRate;
	public List<Vector3> losRates;
	public float rangeToTarget;
	public Vector3 targetVelocity;
	public Vector3 relativeVelocity;

	public Vector3 rotationVector;
	public Vector3 an;

	public LineRenderer lr;

	public float speed;
	public float accel;

	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer>();

		directionToTarget = target.transform.position - transform.position;
		prevDirectionToTarget = directionToTarget;
		losRate = directionToTarget - prevDirectionToTarget;
		rangeToTarget = Vector3.Distance(target.transform.position,transform.position);

		targetPos = target.transform.position;
		prevTargetPos = targetPos;

		losRates.Add(losRate);
	}
	
	// Update is called once per frame
	void Update () {
		prevDirectionToTarget = directionToTarget;
		directionToTarget = target.transform.position - transform.position;

		Vector3 losDelta = directionToTarget - prevDirectionToTarget;

		losDelta = losDelta - Vector3.Project(losDelta,directionToTarget);

		Vector3 desiredRotation = (Constants.delta * directionToTarget) + (losDelta * 5);
		Quaternion desRot = Quaternion.LookRotation(desiredRotation,transform.up);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, desRot, Constants.delta * 90);

		//rangeToTarget = Vector3.Distance(target.transform.position,transform.position);
		/*

		targetPos = target.transform.position;

		targetVelocity = targetPos - prevTargetPos;

		relativeVelocity = targetVelocity - (transform.forward*speed/3.6f*Constants.delta);

		prevTargetPos = targetPos;

		losRate = prevDirectionToTarget - directionToTarget;

		losRates.Add(losRate);

		prevDirectionToTarget = directionToTarget;


		rotationVector = Vector3.Cross(directionToTarget,relativeVelocity) / Vector3.Dot(directionToTarget,directionToTarget);

		an = Vector3.Cross(4*relativeVelocity,rotationVector);
		*/

		lr.SetPosition(0,transform.position);
		lr.SetPosition(1,target.transform.position);

		speed += accel*3.6f*Constants.delta;

		transform.position += transform.forward*speed/3.6f*Constants.delta;
	}

	public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
	{
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);

		if (dir > 0.0f) {
			return 1.0f;
		} else if (dir < 0.0f) {
			return -1.0f;
		} else {
			return 0.0f;
		}
	} 
}
