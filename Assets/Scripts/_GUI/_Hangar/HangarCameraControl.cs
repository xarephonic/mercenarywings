using UnityEngine;
using System.Collections;

public class HangarCameraControl : MonoBehaviour {

	public Transform lookatTarget;

	public float rotationSpeed;

	public bool stopAutoMove;

	public float autoRotateRelief;

	IEnumerator AutoRotateReliefTimer()
	{
		yield return new WaitForSeconds(autoRotateRelief);

		stopAutoMove = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//plane camera control
		if(!stopAutoMove)
		{
			Camera.main.transform.RotateAround(lookatTarget.position,Vector3.up,rotationSpeed*Time.deltaTime);
		}
		Camera.main.transform.LookAt(lookatTarget.position);
		
		for (int i = 0; i < Input.touchCount; i++) {
			if(Input.touches[i].phase == TouchPhase.Moved)
			{
				StopCoroutine(AutoRotateReliefTimer());

				stopAutoMove = true;
				
				Camera.main.transform.RotateAround(lookatTarget.position,Vector3.up,Input.touches[i].deltaPosition.x);
			}
			else if(Input.touches[i].phase == TouchPhase.Ended)
			{
				StopCoroutine(AutoRotateReliefTimer());

				StartCoroutine(AutoRotateReliefTimer());
			}
		}

	}
}
