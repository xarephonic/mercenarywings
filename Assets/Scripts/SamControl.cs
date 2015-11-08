using UnityEngine;
using System.Collections;

public class SamControl : MonoBehaviour {

	public GameObject missilePrefab;
	public GameObject missileLaunchPoint;

	public GameObject myMissile;

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

			//myMissile.GetComponent<MissileMovement>().Fire(target);

			myMissile = null;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			FireMyMissile(other.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		ArmSamSite();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
