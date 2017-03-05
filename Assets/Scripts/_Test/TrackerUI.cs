using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrackerUI : MonoBehaviour {

    public TrackingModule myTrackingModule;

    public Sprite trackingCrosshair;
	public Image myTrackingCrosshair;
	public LineRenderer myTargetingLine;

	// Use this for initialization
	void Start () {

        GameObject lockedCrosshairImage = new GameObject();
        Image crossHairImage = lockedCrosshairImage.AddComponent<Image>();

		crossHairImage.rectTransform.pivot = new Vector2(0.5f,0.5f);
		crossHairImage.rectTransform.anchorMax = Vector2.zero;
		crossHairImage.rectTransform.anchorMin = Vector2.zero;
		crossHairImage.rectTransform.sizeDelta = new Vector2(64,64);

		crossHairImage.raycastTarget = false;
		crossHairImage.sprite = trackingCrosshair;
		crossHairImage.color = Color.black;
		crossHairImage.type = Image.Type.Filled;
		crossHairImage.fillMethod = Image.FillMethod.Radial360;
		crossHairImage.fillAmount = 0;

		//TODO get reference to the correct canvas
		Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

		crossHairImage.transform.SetParent(canvas.transform);
		crossHairImage.transform.SetAsFirstSibling();
		crossHairImage.transform.localScale = Vector3.one;

		myTrackingCrosshair = crossHairImage;

		myTargetingLine = myTrackingModule.gameObject.AddComponent<LineRenderer>();

		myTargetingLine.useWorldSpace = true;
		myTargetingLine.SetVertexCount(2);
		myTargetingLine.SetPositions(new Vector3[]{Vector3.zero,Vector3.zero});
		myTargetingLine.SetWidth(0.25f,0.25f);
		myTargetingLine.SetColors(Color.red,Color.red);
		myTargetingLine.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (myTrackingModule.target)
        {
			Vector3 screenCoords = Camera.main.WorldToScreenPoint(myTrackingModule.target.transform.position);

			if(screenCoords.z < 0)
			{
				myTrackingCrosshair.enabled = false;
				return;
			}

			myTrackingCrosshair.enabled = true;

			myTrackingCrosshair.rectTransform.anchoredPosition = new Vector2( screenCoords.x / myTrackingCrosshair.transform.parent.localScale.x,screenCoords.y / myTrackingCrosshair.transform.parent.localScale.y);

			myTrackingCrosshair.GetComponent<Image>().fillAmount = myTrackingModule.trackProgressPercentage;


			if(myTrackingModule.locked)
				myTrackingCrosshair.color = Color.red;
			else{
				myTrackingCrosshair.color = Color.black;
			}


			myTargetingLine.enabled = true;

			myTargetingLine.SetPositions(new Vector3[]{myTrackingModule.transform.position,myTrackingModule.target.transform.position});
		}else{

			myTargetingLine.enabled = false;
			myTrackingCrosshair.enabled = false;
		}

	}
}
