using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MissionLoadoutMenu : MonoBehaviour {


	public GameObject[] playerPlanes;

	void OnEnable()
	{
		GameObject p = GameObject.Find("Planes");

		playerPlanes = new GameObject[p.transform.childCount];

		for (int i = 0; i < playerPlanes.Length; i++) {

			playerPlanes[i] = p.transform.GetChild(i).gameObject;

			LoadoutHolder.loadoutHolder.GetComponent<LoadoutHolder>().ChoosePlane(i);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
