using UnityEngine;
using System.Collections;

public class MissileNavigator : MonoBehaviour {

	public GameObject target;

	public MovementModule movementModule;

	public LineRenderer line;


	public void FindInterceptSolution()
	{
		float distanceToTarget = Vector3.Distance(target.transform.position,transform.position);

		float myCurrentAirSpeed = movementModule.airSpeed;

		float framesToReachTarget = distanceToTarget / (myCurrentAirSpeed * Constants.delta);

		Vector3 interceptionPoint = target.transform.position + target.GetComponent<MovementModule>().airSpeed * Constants.delta * framesToReachTarget * target.transform.forward;

		Vector3 relativeInterceptPoint = transform.InverseTransformPoint(interceptionPoint);

		Debug.Log("target distance: "+distanceToTarget);

		relativeInterceptPoint.Normalize();

		float angleBetweenMyHeadingAndTargetPoint = Vector3.Angle(transform.forward,relativeInterceptPoint);

		Debug.Log("intercept angle: "+angleBetweenMyHeadingAndTargetPoint);

		float speed = 100.0f;

		if(angleBetweenMyHeadingAndTargetPoint > (framesToReachTarget * movementModule.yawRate * Constants.delta))
		{
			Debug.Log("slowing down to turn");

			speed = -100.0f;
		}

		Debug.Log("relative intercept point: "+relativeInterceptPoint);
		Debug.Log("my heading: "+transform.forward);

		movementModule.SetCommandsForThisTurn(speed,Mathf.Clamp(relativeInterceptPoint.x * Constants.navigationConstant * 100,-100,100),Mathf.Clamp(relativeInterceptPoint.y * Constants.navigationConstant * -100,-100,100 ),0);

        movementModule.ExecuteMovement();
	}

	// Use this for initialization
	void Start () {

		movementModule = GetComponent<MovementModule>();
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
		FindInterceptSolution();
	}
}
