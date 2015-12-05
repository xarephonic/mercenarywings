using UnityEngine;
using System.Collections;

public class HitPointModule : MonoBehaviour {

	public MovementModule movementModule;

	public float hitPoints;

	public bool crashing;

	public void RecieveDamage(float dmgToRecieve)
	{
		hitPoints -= dmgToRecieve;

		if(hitPoints <= 0)
		{
			Crash();
		}
	}

	public void Crash()
	{
		crashing = true;

		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		movementModule = GetComponent<MovementModule>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
