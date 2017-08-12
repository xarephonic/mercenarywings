using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HangarBottomBar : MonoBehaviour {

	public HangarPlaneDisplayControl ps;

	public GameObject bottomBarPlaneButtonPrefab;

	// Use this for initialization
	void Start () {
        
		for (int i = 0; i < AssetKeeper.instance.playerPlanes.Count; i++) {

			GameObject x = Instantiate(bottomBarPlaneButtonPrefab,Vector3.zero,Quaternion.identity) as GameObject;

			x.transform.SetParent(gameObject.transform);

            x.GetComponent<Button>().image.sprite = AssetKeeper.instance.playerPlanes[i].hangarPicture;

			x.transform.localScale = Vector3.one;

			int indexToGoTo = i;

			x.GetComponent<Button>().onClick.AddListener(() => ps.GoToIndex(indexToGoTo));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
