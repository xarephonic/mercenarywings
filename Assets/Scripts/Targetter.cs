using UnityEngine;
using System.Collections;

public class Targetter : MonoBehaviour {

	public GameObject ucakCont;

	public Ray targettingRay;

	public Vector3 targetPoint;

	public float airSpeed;

	public float turnRate;

	public int playTime;

	public GameObject fakePlane;

	public GameObject cubePrefab;

	Vector3[] curvePoints;
	int pointIndex;

	// Use this for initialization
	void Start () {

//		curvePoints = new Vector3[playTime];
//
//		fakePlane.transform.position = ucakCont.transform.position;
//		fakePlane.transform.eulerAngles = ucakCont.transform.eulerAngles;
//
//		for (int i = 0; i < playTime; i++) {
//
//			Vector3 pos = fakePlane.transform.position + fakePlane.transform.forward * airSpeed * 0.05f;
//
//			fakePlane.transform.Rotate(Vector3.up,turnRate * 0.05f);
//
//			curvePoints[i] = pos;
//
//			Instantiate(cubePrefab,pos,Quaternion.identity);
//
//			fakePlane.transform.position = pos;
//
//		}

//		for (int i = playTime; i < playTime+100; i++) {
//			Vector3 pos = fakePlane.transform.position + fakePlane.transform.forward * airSpeed * 0.05f;
//			
//			fakePlane.transform.Rotate(Vector3.up,-turnRate * 0.05f);
//			
//			curvePoints[i] = pos;
//			
//			Instantiate(cubePrefab,pos,Quaternion.identity);
//			
//			fakePlane.transform.position = pos;
//		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

//		targettingRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//		RaycastHit hit = new RaycastHit();
//
//		if(Input.GetKeyUp(KeyCode.Mouse0))
//		{
//			if(Physics.Raycast(targettingRay,out hit))
//			{
//				targetPoint = new Vector3(hit.point.x,ucakCont.transform.position.y,hit.point.z);
//			}
//		}

//		ucakCont.transform.position += ucakCont.transform.forward * airSpeed * Time.deltaTime;
//
//		airSpeed = airSpeed * (1 - decelerationRate);
//
//		ucakCont.transform.Rotate(Vector3.up,turnRate * Time.deltaTime);

//		if(Vector3.Distance(ucakCont.transform.position,curvePoints[pointIndex]) > 0.1f)
//		{
//			ucakCont.transform.position = Vector3.MoveTowards(ucakCont.transform.position,curvePoints[pointIndex],airSpeed*Time.deltaTime);
//
//			ucakCont.transform.LookAt(curvePoints[pointIndex]);
//		}
//		else
//		{
//			pointIndex++;
//		}

	}
}
