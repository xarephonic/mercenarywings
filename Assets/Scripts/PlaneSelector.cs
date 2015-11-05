using UnityEngine;
using System.Collections;

public class PlaneSelector : MonoBehaviour {

	public static PlaneMoveControl selectedPlane;

	public GameObject _selectedPlane;

	public LoadoutHolder l;

	public enum PlayMode{

		commandMode,
		playMode
	}

	public static PlayMode currentMode;

	public void Commit()
	{
		selectedPlane.Commit();

		currentMode = PlayMode.playMode;
	}

	// Use this for initialization
	void Start () {

		l = LoadoutHolder.loadoutHolder.GetComponent<LoadoutHolder>();

		selectedPlane = l.mySpawnedPlanes[0];

		//Camera.main.transform.SetParent(selectedPlane.transform);

		currentMode = PlayMode.commandMode;
	}
	
	// Update is called once per frame
	void Update () {
	
		_selectedPlane = selectedPlane.gameObject;
	}
}
