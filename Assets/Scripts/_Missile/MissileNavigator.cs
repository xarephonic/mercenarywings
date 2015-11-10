using UnityEngine;
using System.Collections;

public class MissileNavigator : MonoBehaviour {

	public GameObject target;

	public MovementModule movementModule;


	public void FindInterceptSolution()
	{
		float distanceToTarget = Vector3.Distance(target.transform.position,transform.position);

		float myCurrentAirSpeed = movementModule.airSpeed;

		float framesToReachTarget = distanceToTarget / (myCurrentAirSpeed * Constants.delta);

		Vector3 interceptionPoint = target.transform.position + target.GetComponent<MovementModule>().airSpeed * Constants.delta * framesToReachTarget * target.transform.forward;

		Vector3 relativeInterceptPoint = transform.InverseTransformPoint(interceptionPoint);

		relativeInterceptPoint.Normalize();

		movementModule.SetCommandsForThisTurn(100,Mathf.Clamp(relativeInterceptPoint.x * Constants.navigationConstant * 100,-100,100),Mathf.Clamp(relativeInterceptPoint.y * Constants.navigationConstant * -100,-100,100),0);

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
