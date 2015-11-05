using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KolControl : MonoBehaviour {

	public float roll;
	public float pitch;
	
	public float rollLimit;
	public float pitchLimit;

	public RectTransform myRect;
	public RectTransform kolRect;

	public void ChangeKolPosition()
	{
		kolRect.transform.position = Input.mousePosition;

		kolRect.anchoredPosition = new Vector2(Mathf.Clamp(kolRect.anchoredPosition.x,-1*rollLimit,rollLimit),Mathf.Clamp(kolRect.anchoredPosition.y,-1*pitchLimit,pitchLimit));
	}

	// Use this for initialization
	void Start () {

		myRect = GetComponent<RectTransform>();
		kolRect = transform.GetChild(0).GetComponent<RectTransform>();

		Vector2 sizeDifference = myRect.sizeDelta - kolRect.sizeDelta;

		rollLimit = sizeDifference.x/2;
		pitchLimit = sizeDifference.y/2;

	}
	
	// Update is called once per frame
	void Update () {

		roll = kolRect.anchoredPosition.x / rollLimit * 100;

		pitch = kolRect.anchoredPosition.y / pitchLimit * 100;

	}
}
