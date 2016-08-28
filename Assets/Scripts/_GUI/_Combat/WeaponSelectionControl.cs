using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponSelectionControl : MonoBehaviour {

	public Sprite cannon;
	public Sprite missile;

	public Button[] weaponButtons;

	private void SetImageForButton(int ind, Sprite sp){
		weaponButtons[ind].transform.GetChild(0).GetComponent<Image>().sprite = sp;
	}

	private void ClearWeaponImages(){
		for (int i = 0; i < weaponButtons.Length; i++) {
			SetImageForButton(i, null);
		}

		SetImageForButton(0, cannon);
	}

	private void ActivateWeaponButtons(bool b){
		foreach(Button weaponBttn in weaponButtons){
			weaponBttn.interactable = b;
		}
		//cannon is always available
		weaponButtons[0].interactable = true;
	}

	private void DisplayWeapons(AircraftLoadout loadout) {
		ActivateWeaponButtons(false);
		ClearWeaponImages();

		for (int i = 0; i < loadout.loadedArmament.Length; i++) {
			//TODO add sprites for other weapons as well
			GameObject weapon = loadout.loadedArmament[i];
			if(weapon.GetComponent<MissileCore>() != null){
				
				SetImageForButton(i+1,missile);
				weaponButtons[i+1].interactable = true;
			} else {
				SetImageForButton(i+1, null);
			}
		}
	}

	// Use this for initialization
	void Start () {
		int count = transform.childCount;
		weaponButtons = new Button[count];
		for (int i = 0; i < count; i++) {
			weaponButtons[i] = transform.GetChild(i).GetComponent<Button>();
		}

		PlayerPlaneSelectionHandler.OnSelectedPlaneChanged += (GameObject newSelectedPlane) => DisplayWeapons(newSelectedPlane.GetComponent<AircraftLoadout>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
