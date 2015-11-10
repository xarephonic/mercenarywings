using UnityEngine;
using System.Collections;

public class SamControl : MonoBehaviour {

	public GameObject missilePrefab;
	public GameObject missileLaunchPoint;

	public GameObject myMissile;

	public TrackingModule trackingModule;

	public void ArmSamSite()
	{
		if(myMissile == null)
		{
			GameObject missile = Instantiate(missilePrefab,missileLaunchPoint.transform.position,Quaternion.identity) as GameObject;

			missile.transform.eulerAngles = missileLaunchPoint.transform.eulerAngles;

			myMissile = missile;
		}
	}

	public void FireMyMissile(GameObject target)
	{
		if(myMissile != null)
		{
			myMissile.transform.SetParent(null);

			myMissile = null;
		}
	}



	// Use this for initialization
	void Start () {
		ArmSamSite();

		trackingModule = GetComponent<TrackingModule>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
