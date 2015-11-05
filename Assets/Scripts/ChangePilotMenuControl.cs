using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangePilotMenuControl : MonoBehaviour {

	public planeswapper ps;

	public pilot currentPilot;

	public pilot noPilot;

	public pilot[] pilots;
	public int pilotIndex;

	public Image[] planeStatBars;

	public Image[] pilotStatBars;

	public Image[] totalStatBars;

	public Image totalPilotImage;
	public Image totalPlaneImage;

	public Text planeName;
	public Text pilotName;

	public Image planePic;
	public Image pilotPic;

	public void Forward()
	{
		pilotIndex++;

		if(pilotIndex == pilots.Length)
		{
			pilotIndex = 0;
		}

		currentPilot = pilots[pilotIndex];
	}

	public void Back()
	{
		pilotIndex--;
		
		if(pilotIndex == -1)
		{
			pilotIndex = pilots.Length-1;
		}
		
		currentPilot = pilots[pilotIndex];
	}

	public void ChangePilot()
	{
		if(ps.currentPlane.driver == currentPilot)
		{
			//do nothing
		}
		else
		{
			foreach(GameObject p in ps.planesArray)
			{
				if(p.GetComponent<plane>().driver == currentPilot)
				{
					p.GetComponent<plane>().driver = noPilot.GetComponent<pilot>();
					break;
				}
			}

			ps.currentPlane.driver = currentPilot;
		}

		gameObject.SetActive(false);

	}

	void OnEnable()
	{
		currentPilot = ps.currentPlane.driver;

		for (int i = 0; i < pilots.Length; i++) {
			if(pilots[i] == currentPilot)
			{
				pilotIndex = i;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//plane stats

		planeName.text = ps.currentPlane.name;

		planePic.sprite = ps.currentPlane.planePicture;

		Image airAttBar = planeStatBars[0];
		
		float airAttBarTargetScale = ps.currentPlane.airAttack/10;
		
		airAttBar.fillAmount = Mathf.Lerp(airAttBar.fillAmount,airAttBarTargetScale,0.3f);
		
		Image airDefBar = planeStatBars[1];
		
		float airDefBarTargetScale = ps.currentPlane.airDefense/10;
		
		airDefBar.fillAmount = Mathf.Lerp(airDefBar.fillAmount,airDefBarTargetScale,0.3f);
		
		Image groundAttBar = planeStatBars[2];
		
		float groundAttBarTargetScale = ps.currentPlane.groundAttack/10;
		
		groundAttBar.fillAmount = Mathf.Lerp(groundAttBar.fillAmount,groundAttBarTargetScale,0.3f);
		
		Image groundDefBar = planeStatBars[3];
		
		float groundDefBarTargetScale = ps.currentPlane.groundDefense/10;
		
		groundDefBar.fillAmount = Mathf.Lerp(groundDefBar.fillAmount,groundDefBarTargetScale,0.3f);

		//pilot stats

		string pilotsAssignedPlaneName = "";

		foreach(GameObject p in ps.planesArray)
		{
			if(p.GetComponent<plane>().driver == currentPilot)
			{
				pilotsAssignedPlaneName = p.GetComponent<plane>().name;
				break;
			}
			else
			{
				pilotsAssignedPlaneName = "Not Assigned";
			}
		}

		pilotName.text = currentPilot.name+"("+pilotsAssignedPlaneName+")";

		pilotPic.sprite = currentPilot.picture;

		Image pilotAirAttBar = pilotStatBars[0];
		
		float pilotAirAttBarTargetScale = currentPilot.airAttackSkill/10;
		
		pilotAirAttBar.fillAmount = Mathf.Lerp(pilotAirAttBar.fillAmount,pilotAirAttBarTargetScale,0.3f);
		
		Image pilotAirDefBar = pilotStatBars[1];
		
		float pilotAirDefBarTargetScale = currentPilot.airDefenseSkill/10;
		
		pilotAirDefBar.fillAmount = Mathf.Lerp(pilotAirDefBar.fillAmount,pilotAirDefBarTargetScale,0.3f);
		
		Image pilotGroundAttBar = pilotStatBars[2];
		
		float pilotGroundAttBarTargetScale = currentPilot.groundAttackSkill/10;
		
		pilotGroundAttBar.fillAmount = Mathf.Lerp(pilotGroundAttBar.fillAmount,pilotGroundAttBarTargetScale,0.3f);
		
		Image pilotGroundDefBar = pilotStatBars[3];
		
		float pilotGroundDefBarTargetScale = currentPilot.groundDefenseSkill/10;
		
		pilotGroundDefBar.fillAmount = Mathf.Lerp(pilotGroundDefBar.fillAmount,pilotGroundDefBarTargetScale,0.3f);

		//total stats

		totalPlaneImage.sprite = ps.currentPlane.planePicture;

		totalPilotImage.sprite = currentPilot.picture;

		float totalAirAttTargetScale = ps.currentPlane.airAttack*0.5f/10+currentPilot.airAttackSkill*0.5f/10;

		float totalAirDefenseTargetScale = ps.currentPlane.airDefense*0.5f/10+currentPilot.airDefenseSkill*0.5f/10;

		float totalGroundAttTargetScale = ps.currentPlane.groundAttack*0.5f/10+currentPilot.groundAttackSkill*0.5f/10;

		float totalGroundDefTargetScale = ps.currentPlane.groundDefense*0.5f/10+currentPilot.groundDefenseSkill*0.5f/10;

		float[] targetValues = new float[]{totalAirAttTargetScale,totalAirDefenseTargetScale,totalGroundAttTargetScale,totalGroundDefTargetScale};

		for (int i = 0; i < totalStatBars.Length; i++) {
			totalStatBars[i].fillAmount = Mathf.Lerp(totalStatBars[i].fillAmount,targetValues[i],0.3f);
		}

	}
}
