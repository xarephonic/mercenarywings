using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoystickControl : MonoBehaviour {

	public float roll;
	public float pitch;
	
	public float rollLimit;
	public float pitchLimit;

	public RectTransform myRect;
	public RectTransform stickRect;

	public void ChangeJoystickPosition()
	{
		stickRect.transform.position = Input.mousePosition;

		stickRect.anchoredPosition = new Vector2(Mathf.Clamp(stickRect.anchoredPosition.x,-1*rollLimit,rollLimit),Mathf.Clamp(stickRect.anchoredPosition.y,-1*pitchLimit,pitchLimit));
	}

	// Use this for initialization
	void Start () {

		myRect = GetComponent<RectTransform>();
		stickRect = transform.GetChild(0).GetComponent<RectTransform>();

		Vector2 sizeDifference = myRect.sizeDelta - stickRect.sizeDelta;

		rollLimit = sizeDifference.x/2;
		pitchLimit = sizeDifference.y/2;

	}
	
	// Update is called once per frame
	void Update () {

		roll = stickRect.anchoredPosition.x / rollLimit * 100;

		pitch = stickRect.anchoredPosition.y / pitchLimit * 100;

	}
}
