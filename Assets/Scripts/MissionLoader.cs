using UnityEngine;
using System.Collections;

public class MissionLoader : MonoBehaviour {

	public void LoadHangar()
	{
		Application.LoadLevel("Hangar");
	}

	public void LoadTestFlightMission()
	{
		Application.LoadLevel("TestFlight");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
