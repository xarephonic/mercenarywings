using UnityEngine;
using System.Collections;

public class MissileMovement : MonoBehaviour {

	public float airSpeed;
	public float turnRate;

	public float climbSpeedLoss;
	public float diveSpeedGain;

	public float remainingFuel;

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

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
}
