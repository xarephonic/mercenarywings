using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AltitudeIndicator : MonoBehaviour {

	public LineRenderer altitudeIndicator;
	public GameObject hitIndicator;

	public bool show;

	GameObject grid;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
		altitudeIndicator.enabled = show;
		hitIndicator.SetActive(show);

		if(grid == null)
		{
			try {
				grid = GameObject.Find("GridManager").GetComponent<GridManager>().grid;
			} catch (System.Exception ex) {
				Debug.Log(ex.Message);
			}
			return;
		}

		if(!show)
			return;

		if(!grid.activeInHierarchy){
			Ray downRay = new Ray(transform.position,Vector3.down);
			RaycastHit hit = new RaycastHit();


			if(Physics.Raycast(downRay,out hit)){
				altitudeIndicator.SetPosition(0,transform.position);
				altitudeIndicator.SetPosition(1,hit.point);
				hitIndicator.transform.position = hit.point +new Vector3(0,1,0);
				hitIndicator.transform.eulerAngles = hit.normal - new Vector3(90,0,0);
			}else{
				altitudeIndicator.SetPosition(0,transform.position);
				altitudeIndicator.SetPosition(1,new Vector3(transform.position.x,0,transform.position.z));
				hitIndicator.transform.position = new Vector3(transform.position.x,1,transform.position.z);
				hitIndicator.transform.eulerAngles = new Vector3(90,0,0);
			}
		}else {
			altitudeIndicator.SetPosition(0,transform.position);
			altitudeIndicator.SetPosition(1,new Vector3(transform.position.x,grid.transform.position.y,transform.position.z));
			hitIndicator.transform.position = new Vector3(transform.position.x,grid.transform.position.y,transform.position.z);
			hitIndicator.transform.eulerAngles = new Vector3(90,0,0);
		}


	}
}
