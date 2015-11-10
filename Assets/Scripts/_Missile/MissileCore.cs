using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileCore : MonoBehaviour {

	public MovementModule movementModule;
	public TrackingModule trackingModule;
		
	public bool fireAndForget;				//if this is a fire and forget missile, it has tracking of its own and moves towards the target each turn regardless

	public void Fire(GameObject g)
	{

	}

	// Use this for initialization
	void Start () {
	
		movementModule = GetComponent<MovementModule>();
		trackingModule = GetComponent<TrackingModule>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
