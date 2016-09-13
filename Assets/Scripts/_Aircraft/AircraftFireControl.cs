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
		coneManager.Cone(500,0.1f,170,30);
		coneManager.transform.eulerAngles = gameObject.transform.eulerAngles;
		coneManager.transform.position = gameObject.transform.position;

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
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneStateManager.currentState == SceneStateManager.CombatSceneState.MOVEMENT){
			if(target != null){
				if(activeWeaponInd == -1){
					Vector3 direction = target.transform.position - transform.position;

					float angle = Vector3.Angle(direction,transform.forward);

					if(Vector3.Distance(transform.position,target.transform.position) < 500 && angle < 10/2.0f)
					{
						if(cannonParticles)
							cannonParticles.gameObject.SetActive(true);
						if(cannonAud)
							cannonAud.gameObject.SetActive(true);
					}
				}
			}
		}else {
			if(cannonParticles)
				cannonParticles.gameObject.SetActive(false);
			if(cannonAud)
				cannonAud.gameObject.SetActive(false);
		}
	}
}
