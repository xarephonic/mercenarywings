using UnityEngine;
using System.Collections;

public class TrackingModule : MonoBehaviour {

	public enum TrackingType
	{
		radar,
		infrared
	}

	public GameObject target;

	public Vector3 lastTrackedPosition;

	public TrackingType trackType;

	public float trackAngle;
	public float trackRange;

	public bool locked;
	public float trackProgress;

	public float sensorStrength;

	[SerializeField]
	private float baseLockTimeConstant;

	public void AcquireTarget()
	{
		EcmModule targetEcmModule = target.GetComponent<EcmModule>();

		float targetEcmStrength = (trackType == TrackingType.radar) ? targetEcmModule.radarEcmStrength : targetEcmModule.infraredEcmStrength;

		float lockTime = (baseLockTimeConstant + ((sensorStrength - targetEcmStrength)*0.05f));

		trackProgress += Constants.delta;

		locked = (trackProgress >= lockTime) ? true : false;
	}

	//Checks to see if I can see the target. If I can see the target, I will keep tracking and try to acquire a target lock. If I lose my track, I have to restart my track progress
	public void TrackTarget()
	{
		Vector2 myFootPrint = new Vector2(transform.position.x,transform.position.z);
		Vector2 targetFootPrint = new Vector2(target.transform.position.x,target.transform.position.z);
		
		Vector2 direction = targetFootPrint - myFootPrint;
		
		float angle = Vector2.Angle(direction,new Vector2(transform.forward.x,transform.forward.z));

		if(Vector3.Distance(transform.position,target.transform.position) < trackRange && angle < trackAngle/2.0f)
		{
			lastTrackedPosition = target.transform.position;

			AcquireTarget();
		}
		else
		{
			locked = false;
			trackProgress = 0;
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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
