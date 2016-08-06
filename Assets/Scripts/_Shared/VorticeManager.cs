using UnityEngine;
using System.Collections;

public class VorticeManager : MonoBehaviour {

	public GameObject vorticeRoot;

	public Vector3 prevForw;
	public Vector3 currentForw;

	public float vorticeThreshold;

	public void SetCurrentForw(Vector3 forw){
		prevForw = currentForw;
		currentForw = forw;

		if(Vector3.Angle(prevForw,currentForw) > vorticeThreshold){
			SetActive(true);
		}else {
			SetActive(false);
		}
	}

	public void SetActive(bool b){
		if(vorticeRoot && b)
			vorticeRoot.SetActive(true);
		else if(vorticeRoot && !b)
			vorticeRoot.SetActive(false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
