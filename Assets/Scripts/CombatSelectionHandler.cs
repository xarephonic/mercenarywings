using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CombatSelectionHandler : MonoBehaviour {

	public SceneAssetsKeeper sceneAssetsKeeper;

	public static GameObject selectedObject;

	public GameObject oldSelectedObject;

	public UnityEvent OnSelectedObjectChanged;

	public void SelectRandomOtherObject(GameObject objectNotToChoose)
	{
		int rand = Random.Range(0,sceneAssetsKeeper.playerAssets.Count);

		if(sceneAssetsKeeper.playerAssets[rand] != objectNotToChoose)
		{
			selectedObject = sceneAssetsKeeper.playerAssets[rand];
		}
		else
		{
			SelectRandomOtherObject(objectNotToChoose);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(oldSelectedObject == null)
		{
			oldSelectedObject = selectedObject;

			OnSelectedObjectChanged.Invoke();
		}
		else if(oldSelectedObject != selectedObject)
		{
			OnSelectedObjectChanged.Invoke();

			oldSelectedObject = selectedObject;
		}
	}
}
