using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionLoader : MonoBehaviour {

	public GameObject loadingView;
	public Image loadingBar;

	IEnumerator UpdateProgressBar(AsyncOperation aop)
	{
		if(!aop.isDone)
		loadingBar.fillAmount = aop.progress;

		yield return new WaitForSeconds(0.01f);

		StartCoroutine(UpdateProgressBar(aop));
	}

	public void LoadHangar()
	{
		Application.LoadLevel("Hangar");
	}

	public void LoadTestFlightMission()
	{
		loadingView.SetActive(true);

		AsyncOperation asyncOp = Application.LoadLevelAsync("TestFlight");

		StartCoroutine(UpdateProgressBar(asyncOp));
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
