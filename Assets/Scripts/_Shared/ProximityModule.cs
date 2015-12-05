using UnityEngine;
using System.Collections;

public class ProximityModule : MonoBehaviour {

	public MissileNavigator missileNavigator;
	public DamageModule damageModule;

	public bool armed;

	public float explosionProximity;
	public float explosionRadius;

	void Explode()
	{
		damageModule.DealDamage(missileNavigator.target.GetComponent<HitPointModule>());
	}

	// Use this for initialization
	void Start () {

		damageModule = GetComponent<DamageModule>();
		missileNavigator = GetComponent<MissileNavigator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(armed && missileNavigator.target != null)
		{
			float distance = Vector3.Distance(missileNavigator.target.transform.position,transform.position);

			if(distance <= explosionProximity)
			{
				Explode();
			}
		}

	}
}
