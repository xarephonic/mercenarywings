using UnityEngine;
using System.Collections;

public class MissileMovement : MonoBehaviour {

	public GameObject target;

	public bool fireAndForget;				//if this is a fire and forget missile, it has tracking of its own and moves towards the target each turn regardless

	public Vector3 lastTrackedPosition;		//this is the last known position of target. if target is lost, missile keeps moving forward from this position onwards until it reacquires the target lock

	public float airSpeed;
	public float turnRate;

	public float climbSpeedLoss;
	public float diveSpeedGain;

	public float remainingFuel;

	public float trackAngle;
	public float trackRange;

	public float delta;

	public void Fire(GameObject g)
	{
		target = g;
		lastTrackedPosition = g.transform.position;
	}

	public void TrackTarget()
	{
		Vector2 myFootPrint = new Vector2(transform.position.x,transform.position.z);
		Vector2 targetFootPrint = new Vector2(target.transform.position.x,target.transform.position.z);

		Vector2 direction = targetFootPrint - myFootPrint;

		float angle = Vector2.Angle(direction,new Vector2(transform.forward.x,transform.forward.z));

		if(angle < trackAngle/2.0f)
		{
			lastTrackedPosition = target.transform.position;

			Debug.Log("TRACKING: "+lastTrackedPosition);
		}
	}

	public void MoveToTarget()
	{
		//we project a ray from the front of our missile to trackingRange

		Ray targetingRay = new Ray(transform.position,transform.forward);

		//we get the birds flight distance between us and the target

		float distance = Vector2.Distance(new Vector2(transform.position.x,transform.position.z),new Vector2(target.transform.position.x,target.transform.position.z));

		//we get the point on the ray that is at the same distance as the target is to the missile

		Vector3 point = targetingRay.GetPoint(distance);

		//we compare the point we got on the ray and the last tracked position of our target

		if(Vector3.Distance(point,lastTrackedPosition) > 0.1f)
		{
			Vector3 turnVector = Vector3.zero;

			Vector3 targetLocalPos = transform.InverseTransformPoint(target.transform.position);

			float x = 0;
			float y = 0;
			float z = 0;

			if(targetLocalPos.x > 0)
			{
				//turn right
				y = turnRate*delta;
			}
			else if(targetLocalPos.x < 0)
			{
				//turn left
				y = -turnRate*delta;
			}

			if(lastTrackedPosition.y > point.y)
			{
				x = -turnRate*delta;
			}
			else if(lastTrackedPosition.y < point.y)
			{
				x = turnRate*delta;
			}

			turnVector = new Vector3(x,y,z);

			transform.Rotate(turnVector);
		}

		transform.position = transform.position + transform.forward * delta * airSpeed;
	}

	public void RecieveTrackingInformation(Vector3 pos)
	{
		lastTrackedPosition = pos;
	}

	// Use this for initialization
	void Start () {

		delta = 0.02f;
	}
	
	// Update is called once per frame
	void Update () {

		if(PlaneSelector.currentMode == PlaneSelector.PlayMode.playMode)
		{
			if(target != null)
			{
				if(fireAndForget)
					TrackTarget();

				MoveToTarget();
			}
		}
		else if(PlaneSelector.currentMode == PlaneSelector.PlayMode.commandMode)
		{

		}
	}
}
