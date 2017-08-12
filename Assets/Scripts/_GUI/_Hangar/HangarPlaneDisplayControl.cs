using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DataClasses;

public class HangarPlaneDisplayControl : MonoBehaviour {

	public int displayedPlaneIndex;

	public GameObject displayedPlane;

	public GameObject displayedPlanesParent;

	public void GoToIndex(int i)
	{
        int newIndex = Mathf.Clamp(i, 0, AssetKeeper.instance.playerPlanes.Count - 1);

        if(newIndex == displayedPlaneIndex)
        {
            return;
        }

        displayedPlaneIndex = newIndex;
        Destroy(displayedPlane);

        PlaneVO pvo = AssetKeeper.instance.playerPlanes[displayedPlaneIndex];
        GameObject loadedAsset;

        AssetLoader.instance.loadedAssets.TryGetValue(pvo.id, out loadedAsset);

        if (loadedAsset != null)
        {
            DisplayPlane(pvo, loadedAsset);
        }
        else
        {
            StartCoroutine(AssetLoader.instance.LoadAsset(pvo.hangarAssetUrl, pvo.name, pvo.assetVersion, pvo.id, delegate (GameObject asset, int id)
            {
                DisplayPlane(pvo, asset);
            }));
        }
    }

    void DisplayPlane(PlaneVO pvo, GameObject asset)
    {
        GameObject x = Instantiate(asset, Vector3.zero, Quaternion.identity) as GameObject;
        x.transform.SetParent(displayedPlanesParent.transform);
        x.transform.localPosition = new Vector3(pvo.hangarPosX, pvo.hangarPosY, pvo.hangarPosZ);
        x.transform.localEulerAngles = Vector3.zero;
        x.SetActive(true);

        displayedPlane = x;
    }

	// Use this for initialization
	void Start () {
        PlaneVO pvo = AssetKeeper.instance.playerPlanes[displayedPlaneIndex];
        GameObject loadedAsset;

        AssetLoader.instance.loadedAssets.TryGetValue(pvo.id, out loadedAsset);

        if (loadedAsset != null)
        {
            DisplayPlane(pvo, loadedAsset);
        }
        else
        {
            StartCoroutine(AssetLoader.instance.LoadAsset(pvo.hangarAssetUrl, pvo.name, pvo.assetVersion, pvo.id, delegate (GameObject asset, int id)
            {
                DisplayPlane(pvo, asset);
            }));
        }   
    }
	
	// Update is called once per frame

    void Update () {


	}
}
