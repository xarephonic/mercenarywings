using UnityEngine;
using System.Collections;

public class DamageModule : MonoBehaviour {

	public GameObject explosionParticleSystem;

	public float damageMax;
	public float damageMin;

	public void DealDamage(HitPointModule targetHpModule)
	{
		float dmgToDeal = Random.Range(damageMin,damageMax);

		targetHpModule.RecieveDamage(dmgToDeal);

		explosionParticleSystem.SetActive(true);

		Destroy(GetComponent<MovementModule>());
		Destroy(GetComponent<MissileNavigator>());

		foreach(Transform child in transform)
		{
			if(child.name != explosionParticleSystem.name)
			{
				Destroy(child.gameObject);
			}
		}

		StartCoroutine(WaitForParticleToFinish());
	}

	IEnumerator WaitForParticleToFinish()
	{
		yield return new WaitForSeconds(5.0f);

		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
