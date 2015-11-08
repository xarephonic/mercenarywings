using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class planeswapper : MonoBehaviour {

	public GameObject[] planesArray;

	public int planesArrayIndex;

	public Transform lookatTarget;

	public float rotationSpeed;

	public Text planeName;

	public Image[] planeStatBars;

	public Image[] pilotStatBars;

	public Text pilotName;

	public Image pilotPic;

	public AircraftCore currentPlane;

	public bool stopAutoMove;

	public void Forward()
	{
		planesArrayIndex++;

		if(planesArrayIndex == planesArray.Length)
			planesArrayIndex = 0;

		for (int i = 0; i < planesArray.Length; i++) {
			planesArray[i].SetActive(false);
		}

		planesArray[planesArrayIndex].SetActive(true);

		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
	}

	public void Back()
	{
		planesArrayIndex--;

		if(planesArrayIndex == -1)
			planesArrayIndex = planesArray.Length-1;

		for (int i = 0; i < planesArray.Length; i++) {
			planesArray[i].SetActive(false);
		}
		
		planesArray[planesArrayIndex].SetActive(true);

		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
	}

	public void GoToIndex(int i)
	{
		planesArrayIndex = Mathf.Clamp(i,0,planesArray.Length-1);

		for (int j = 0; j < planesArray.Length; j++) {
			planesArray[j].SetActive(false);
		}

		planesArray[planesArrayIndex].SetActive(true);
		
		currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();
	}

	// Use this for initialization
	void Start () {

        currentPlane = planesArray[planesArrayIndex].GetComponent<AircraftCore>();

		planeName.text = currentPlane.name;

	}
	
	// Update is called once per frame

    void Update () {

		//plane camera control
		if(!stopAutoMove)
		{
			//Camera.main.transform.RotateAround(lookatTarget.position,Vector3.up,rotationSpeed*Time.deltaTime);
		}
		Camera.main.transform.LookAt(lookatTarget.position);

		for (int i = 0; i < Input.touchCount; i++) {
			if(Input.touches[i].phase == TouchPhase.Moved)
			{
				stopAutoMove = true;

				Camera.main.transform.RotateAround(lookatTarget.position,Vector3.up,Input.touches[i].deltaPosition.x);
			}
			else if(Input.touches[i].phase == TouchPhase.Ended)
			{
				stopAutoMove = false;
			}
		}

		//plane bars
        /*
		planeName.text = currentPlane.name;

		Image airAttBar = planeStatBars[0];

		float airAttBarTargetScale = currentPlane.airAttack*0.5f/10+currentPlane.driver.airAttackSkill*0.5f/10;

		airAttBar.fillAmount = Mathf.Lerp(airAttBar.fillAmount,airAttBarTargetScale,0.3f);

		Image airDefBar = planeStatBars[1];
		
		float airDefBarTargetScale = currentPlane.airDefense*0.5f/10+currentPlane.driver.airDefenseSkill*0.5f/10;

		airDefBar.fillAmount = Mathf.Lerp(airDefBar.fillAmount,airDefBarTargetScale,0.3f);

		Image groundAttBar = planeStatBars[2];
		
		float groundAttBarTargetScale = currentPlane.groundAttack*0.5f/10+currentPlane.driver.groundAttackSkill*0.5f/10;
		
		groundAttBar.fillAmount = Mathf.Lerp(groundAttBar.fillAmount,groundAttBarTargetScale,0.3f);

		Image groundDefBar = planeStatBars[3];
		
		float groundDefBarTargetScale = currentPlane.groundDefense*0.5f/10+currentPlane.driver.groundDefenseSkill*0.5f/10;
		
		groundDefBar.fillAmount = Mathf.Lerp(groundDefBar.fillAmount,groundDefBarTargetScale,0.3f);

		pilotName.text = currentPlane.driver.name;

		pilotPic.sprite = currentPlane.driver.picture;
        */

	}
}
