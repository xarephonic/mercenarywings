using UnityEngine;
using System.Collections;

public class HangarMenuHandler : MonoBehaviour {

	public GameObject menuBg;

	public void ActivateMenuItem(GameObject item)
	{
		item.transform.SetParent(menuBg.transform,false);

		item.SetActive(true);

		menuBg.SetActive(true);
	}

	public void DeactivateMenuItem(GameObject item)
	{
		item.transform.SetParent(null,false);

		item.SetActive(false);

		menuBg.SetActive(false);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
