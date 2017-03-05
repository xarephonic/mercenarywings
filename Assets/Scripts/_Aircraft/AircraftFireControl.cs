using UnityEngine;
using System.Collections;

public class AircraftFireControl : MonoBehaviour {

	public AircraftLoadout loadout;
	public GenerateCone coneManager;

	public GameObject target;

	public void SetTarget(GameObject newTarget){
		target = newTarget;
	}

	public delegate void WeaponSelectionAction();
	public static event WeaponSelectionAction OnSelectedWeaponChanged;

	public int activeWeaponInd;			//if this is -1, active weapon is cannon
	public GameObject activeWeapon;

	public ParticleSystem cannonParticles;
	public AudioSource cannonAud;

	public float cannonRange;
	public float cannonFireRate;
	public float cannonAccuracy;
	public float cannonDmg;
	public float roundsPerFrame;
	public float cannonCoolDownTime;		//cannon will keep firing for this amount of time after the target is gone, this is to stop abrupt stopping of cannon when target dies
	public float remainingCannonCoolDownTime;

	public void ChangeSelectedWeapon(int newInd){
		Debug.Log("Changing selected weapon to "+newInd);
		activeWeaponInd = newInd;
		activeWeapon = null;
		if(newInd == -1){
			//OnSelectedWeaponChanged();
			GenerateConeForWeapon();
			return;
		}

		activeWeapon = loadout.loadedArmament[activeWeaponInd];

		//OnSelectedWeaponChanged();
		GenerateConeForWeapon();
	}

	void GenerateConeForWeapon(){
		Debug.Log("Generating Cone");

		coneManager.transform.SetParent(null);

		coneManager.Cone(500*Constants.scaleFactor,0.1f,170,30);
		coneManager.transform.eulerAngles = gameObject.transform.eulerAngles;
		coneManager.transform.position = gameObject.transform.position;

		coneManager.transform.SetParent(gameObject.transform);

	}

	// Use this for initialization
	void Awake () {
		loadout = gameObject.GetComponent<AircraftLoadout>();
		coneManager = GameObject.Find("ConeManager").GetComponent<GenerateCone>();

		PlayerPlaneSelectionHandler.OnSelectedPlaneChanged += delegate(GameObject newSelectedPlane) {
			if(newSelectedPlane == this.gameObject){
				ChangeSelectedWeapon(-1);
			}
		};

		remainingCannonCoolDownTime = cannonCoolDownTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneStateManager.currentState == SceneStateManager.CombatSceneState.MOVEMENT){
			if(target != null && target.activeInHierarchy){
				if(activeWeaponInd == -1){
					Vector3 direction = target.transform.position - transform.position;

					float angle = Vector3.Angle(direction,transform.forward);

					if(Vector3.Distance(transform.position,target.transform.position) < 500*Constants.scaleFactor && angle < 10/2.0f)
					{
						float rand = Random.Range(0,1);
						if(rand < cannonAccuracy){
							target.GetComponent<HitPointModule>().RecieveDamage(cannonDmg);
						}

						if(cannonParticles){
							cannonParticles.gameObject.SetActive(true);
							cannonParticles.Play();
						}
						if(cannonAud)
							cannonAud.gameObject.SetActive(true);
					}
				}
			}
			else {

				remainingCannonCoolDownTime -= 1*Time.deltaTime;

				if(remainingCannonCoolDownTime <= 0) {
					if(cannonParticles)
						cannonParticles.gameObject.SetActive(false);
					if(cannonAud)
						cannonAud.gameObject.SetActive(false);

					remainingCannonCoolDownTime = cannonCoolDownTime;
					//cannonAud.volume = 1.0f;
				} else {
					cannonAud.volume = Mathf.Lerp(cannonAud.volume,0,0.3f);
				}
			}
		}else {
			remainingCannonCoolDownTime = cannonCoolDownTime;
			//cannonAud.volume = 1.0f;

			if(cannonParticles)
				//cannonParticles.gameObject.SetActive(false);
				cannonParticles.Pause();
			if(cannonAud)
				cannonAud.gameObject.SetActive(false);
		}
	}
}
