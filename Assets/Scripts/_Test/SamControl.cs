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
			myMissile.GetComponent<MissileNavigator>().target = target;

			myMissile.GetComponent<MovementModule>().airSpeed = myMissile.GetComponent<MovementModule>().GetOptimalSpeed()/2.0f;

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

		foreach(GameObject asset in SceneAssetsKeeper.sceneAssetsKeeper.playerAssets)
		{
			if(Vector3.Distance(asset.transform.position,transform.position) < trackingModule.trackRange)
			{
				trackingModule.target = asset;
				trackingModule.TrackTarget();
				break;
			}
		}

		if(trackingModule.locked)
		{
			FireMyMissile(trackingModule.target);
		}
	}
}
