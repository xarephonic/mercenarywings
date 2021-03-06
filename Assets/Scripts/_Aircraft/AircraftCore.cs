﻿using UnityEngine;
using System.Collections;

public class AircraftCore : MonoBehaviour {

	public int aircraftId;
	public string aircraftName;

	public MovementModule movementModule;
	public TrackingModule trackingModule;
	public AircraftLoadout loadoutModule;
	public AircraftFireControl fireControlModule;

    public Sprite aircraftPicture;

	// Use this for initialization
	void Start () {
	
		movementModule = GetComponent<MovementModule>();
		trackingModule = GetComponent<TrackingModule>();
		loadoutModule = GetComponent<AircraftLoadout>();
		fireControlModule = GetComponent<AircraftFireControl>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
