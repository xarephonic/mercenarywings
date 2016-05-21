using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrackerUI : MonoBehaviour {

    public TrackingModule myTrackingModule;

    public Sprite lockedCrosshair;
    public Sprite trackingCrosshair;

	// Use this for initialization
	void Start () {

        GameObject lockedCrosshairImage = new GameObject();
        lockedCrosshairImage.AddComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {

        if (myTrackingModule.target)
        {

        }

	}
}
