using UnityEngine;
using System.Collections;

public class TestMissileMovement : MonoBehaviour {

	public GameObject target;

    public float losAngle;

	public LineRenderer headingRenderer;
	public LineRenderer losRenderer;

	public MovementModule movementModule;


	public void GetAngleDifference()
	{
        Vector3 losToTarget = target.transform.position - transform.position;

        float newLosAngle = Vector3.Angle(losToTarget, transform.forward);

        if(losAngle == 0)
        {
            losAngle = newLosAngle;
        }

        float deltaLosAngle = losAngle - newLosAngle;

        Debug.Log(deltaLosAngle);

        movementModule.SetCommandsForThisTurn(100, deltaLosAngle * 100, 0, 0);

        losAngle = newLosAngle;

        movementModule.ExecuteMovement();

		losRenderer.SetPosition(0,transform.position);
		losRenderer.SetPosition(1,transform.position + losToTarget);

		headingRenderer.SetPosition(0,transform.position);
		headingRenderer.SetPosition(1,transform.position+transform.forward*100);
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
