using UnityEngine;
using System.Collections;

public class TestMissileMovement : MonoBehaviour {

	public GameObject target;

	public Vector3 targetHeading;
	public float targetSpeed;

	public float angleToTarget;

	public float angleDifferenceBetweenHeadings;
	public float delta;

	public LineRenderer headingRenderer;
	public LineRenderer losRenderer;

	public MovementModule movementModule;

	public void GetTargetHeading()
	{
		targetHeading = target.transform.forward;
	}

	public void GetTargetSpeed()
	{
		targetSpeed = target.GetComponent<MovementModule>().airSpeed;
	}

	public void GetAngleToTarget()
	{
		angleToTarget = Vector3.Angle(transform.forward,targetHeading);
	}

	public void GetAngleDifference()
	{
		float newAngleDiff = 0;

		newAngleDiff = Vector3.Angle(transform.forward,(target.transform.position - transform.position));

		delta = newAngleDiff - angleDifferenceBetweenHeadings;

		angleDifferenceBetweenHeadings = newAngleDiff;

		Vector3 targetLocalPos = transform.InverseTransformPoint(target.transform.position);

		Debug.Log("Target "+targetLocalPos);

		if(delta > 1)
		{
			Debug.Log("turning into target");

			if(targetLocalPos.x > 0)
			{
				Debug.Log("target to my right");

				movementModule.SetCommandsForThisTurn(100,100,0,0);
			}
			else if(targetLocalPos.x < 0)
			{
				Debug.Log("target to my left");

				movementModule.SetCommandsForThisTurn(100,-100,0,0);
			}

		}
		else if(delta < -1)
		{
			Debug.Log("turning away from target");

			if(targetLocalPos.x > 0)
			{
				Debug.Log("target to my right");

				movementModule.SetCommandsForThisTurn(100,-100,0,0);
			}
			else if(targetLocalPos.x < 0)
			{
				Debug.Log("target to my left");

				movementModule.SetCommandsForThisTurn(100,100,0,0);
			}
		}
		else
		{
			movementModule.SetCommandsForThisTurn(100,0,0,0);
		}

		movementModule.ExecuteMovement();

		losRenderer.SetPosition(0,transform.position);
		losRenderer.SetPosition(1,target.transform.position);

		headingRenderer.SetPosition(0,transform.position);
		headingRenderer.SetPosition(1,transform.position+transform.forward*100);
	}

	// Use this for initialization
	void Start () {

		movementModule = GetComponent<MovementModule>();
	}
	
	// Update is called once per frame
	void Update () {
	
		GetTargetSpeed();
		GetTargetHeading();
		GetAngleToTarget();
		GetAngleDifference();
	}
}
