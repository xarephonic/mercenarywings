using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstrumentControl : MonoBehaviour {

	public Text speedo;
	public Text altimeter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		speedo.text = "Speed: "+PlaneSelector.selectedPlane.airSpeed;
		altimeter.text = "Altitude: "+PlaneSelector.selectedPlane.altitude;

	}
}
