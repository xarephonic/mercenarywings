using UnityEngine;
using System.Collections;

public class MissileNavigator : MonoBehaviour {

	public GameObject target;

	public MovementModule movementModule;

	public void FindInterceptSolution()
	{

		Vector3 directionToTarget = target.transform.position - transform.position;

		Vector3 relativeDirectionToTarget = transform.InverseTransformDirection(directionToTarget);

		relativeDirectionToTarget.Normalize();

		Debug.Log(Vector3.Distance(target.transform.position,transform.position));

		movementModule.SetCommandsForThisTurn(100,(relativeDirectionToTarget.x < 0) ? -100:100 , (relativeDirectionToTarget.y < 0) ? 100:-100 , 0);

		movementModule.ExecuteMovement();

	}

	// Use this for initialization
	void Start () {

		movementModule = GetComponent<MovementModule>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!target)
		{
			if(movementModule.enabled)
				movementModule.enabled = false;

			return;
		}
		else{
			movementModule.enabled = true;
		}


		if(SceneStateManager.currentState == SceneStateManager.CombatSceneState.MOVEMENT){
			FindInterceptSolution();
		}
	}
}
