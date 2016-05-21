using UnityEngine;
using System.Collections;

public class testlockclick : MonoBehaviour {

    public TrackingModule lockerTrackingModule;

    void OnMouseDown()
    {
        lockerTrackingModule.SetTarget(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
