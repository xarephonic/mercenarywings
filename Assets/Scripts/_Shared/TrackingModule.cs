using UnityEngine;
using System.Collections;

public class TrackingModule : MonoBehaviour {

	public enum TrackingType
	{
		radar,
		infrared
	}

	public GameObject target;

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget == target)
            return;

		ResetLock();
        target = newTarget;
    }

	public void ResetLock(){
		trackProgress = 0;
		trackProgressPercentage = 0;
		locked = false;
	}

	public Vector3 lastTrackedPosition;

	public TrackingType trackType;

	public float trackAngle;
	public float trackRange;

	public bool locked;
	public float trackProgress;
	public float trackProgressPercentage;

	public float sensorStrength;

	[SerializeField]
	private float baseLockTimeConstant;

	public void AcquireTarget()
	{
		EcmModule targetEcmModule = target.GetComponent<EcmModule>();

		float targetEcmStrength = (trackType == TrackingType.radar) ? targetEcmModule.radarEcmStrength : targetEcmModule.infraredEcmStrength;

		float lockTime = (baseLockTimeConstant + ((targetEcmStrength - sensorStrength)*0.05f));

		trackProgress += Constants.delta;
        trackProgress = Mathf.Clamp(trackProgress, 0, lockTime);

		trackProgressPercentage = trackProgress / lockTime;

		locked = (trackProgress >= lockTime) ? true : false;
	}

	//Checks to see if I can see the target. If I can see the target, I will keep tracking and try to acquire a target lock. If I lose my track, I have to restart my track progress
	public void TrackTarget()
	{
		/*
		Vector2 myFootPrint = new Vector2(transform.position.x,transform.position.z);
		Vector2 targetFootPrint = new Vector2(target.transform.position.x,target.transform.position.z);
		
		Vector2 direction = targetFootPrint - myFootPrint;
		*/

		Vector3 direction = target.transform.position - transform.position;
		
		//float angle = Vector2.Angle(direction,new Vector2(transform.forward.x,transform.forward.z));

		float angle = Vector3.Angle(direction,transform.forward);

		if(Vector3.Distance(transform.position,target.transform.position) < trackRange && angle < trackAngle/2.0f)
		{
			lastTrackedPosition = target.transform.position;

			AcquireTarget();
		}
		else
		{
			ResetLock();
		}
	}

	public void SetTrackingInfo(GameObject target,Vector3 targetPos)
	{
		this.target = target;
		this.lastTrackedPosition = targetPos;

		locked = true;
	}

	// Use this for initialization
	void Start () {
		SceneAssetsKeeper.OnAssetDestroyed += (GameObject asset) => {
			if(asset == target){
				SetTarget(null);
			}
		};
	}
	
	// Update is called once per frame
	void Update () {

		//TODO make this work only when the game is in play state (stop tracking in planning state)
		if (target && SceneStateManager.currentState == SceneStateManager.CombatSceneState.MOVEMENT)
        {
            TrackTarget();
        }
	}
}
