using UnityEngine;
using System.Collections;

public class TestMissileMovement : MonoBehaviour {

	public GameObject target;

    public float losAngle;

	public LineRenderer headingRenderer;
	public LineRenderer losRenderer;

	public MovementModule movementModule;

	public float fuel;

	public void GetAngleDifference()
	{
		if(fuel <= 0)
			return;

		float distanceToTarget = Vector3.Distance(target.transform.position,transform.position);

		float myCurrentAirSpeed = movementModule.airSpeed;

		float framesToReachTarget = distanceToTarget / (myCurrentAirSpeed * Constants.delta);

		Vector3 interceptionPoint = target.transform.position + target.GetComponent<MovementModule>().airSpeed * Constants.delta * framesToReachTarget * target.transform.forward;

		Vector3 relativeInterceptPoint = transform.InverseTransformPoint(interceptionPoint);

		relativeInterceptPoint.Normalize();

		movementModule.SetCommandsForThisTurn(100,Mathf.Clamp(relativeInterceptPoint.x * Constants.navigationConstant * 100,-100,100),Mathf.Clamp(relativeInterceptPoint.y * Constants.navigationConstant * -100,-100,100),0);

        movementModule.ExecuteMovement();

		losRenderer.SetPosition(0,transform.position);
		losRenderer.SetPosition(1,target.transform.position);

		headingRenderer.SetPosition(0,transform.position+transform.forward*10);
		headingRenderer.SetPosition(1,transform.position+transform.forward*100);

		fuel -= Constants.delta;
	}

	// Use this for initialization
	void Start () {

		movementModule = GetComponent<MovementModule>();
	}
	
	// Update is called once per frame
	void Update () {
		GetAngleDifference();
	}
}
