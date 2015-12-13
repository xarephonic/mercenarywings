using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoystickControl : MonoBehaviour {

	public bool interactable;

	public float roll;
	public float pitch;
	
	public float rollLimit;
	public float pitchLimit;

	public RectTransform myRect;
	public RectTransform stickRect;

	public void ChangeJoystickPosition()
	{
		if(!interactable)
			return;

		stickRect.transform.position = Input.mousePosition;

		stickRect.anchoredPosition = new Vector2(Mathf.Clamp(stickRect.anchoredPosition.x,-1*rollLimit,rollLimit),Mathf.Clamp(stickRect.anchoredPosition.y,-1*pitchLimit,pitchLimit));
	}

	public void SetJoystickPositionAccordingToValues(float p, float r){

		stickRect.anchoredPosition = new Vector2(r / 100 * rollLimit, p / 100 * pitchLimit);
	}

	// Use this for initialization
	void Start () {

		interactable = true;

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
