using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour {

	public Ray targetingRay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		targetingRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit = new RaycastHit();

	}
}
