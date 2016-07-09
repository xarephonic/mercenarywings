using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissionLoader : MonoBehaviour {

	public static MissionLoader instance;

	public GameObject loadingView;
	public Image loadingBar;

	IEnumerator UpdateProgressBar(AsyncOperation aop)
	{
		if(!aop.isDone)
			loadingBar.fillAmount = aop.progress;
		else
			yield break;

		yield return new WaitForSeconds(0.01f);

		StartCoroutine(UpdateProgressBar(aop));
	}

	public void LoadHangar()
	{
		loadingView.SetActive(true);

		AsyncOperation asyncOp = Application.LoadLevelAsync("Hangar");

		StartCoroutine(UpdateProgressBar(asyncOp));
	}

	public void LoadTestFlightMission()
	{
		loadingView.SetActive(true);

		AsyncOperation asyncOp = Application.LoadLevelAsync("TestFlight");

		StartCoroutine(UpdateProgressBar(asyncOp));
	}

	public void GetLoadingElements(){
		loadingView = GameObject.Find("LoadView");
		loadingBar = GameObject.Find("ProgressBar").GetComponent<Image>();
		
		loadingView.SetActive(false);
	}

	void OnLevelWasLoaded(){
		GetLoadingElements();
	}

	// Use this for initialization
	void Start () {
		GetLoadingElements();

		if(instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
			DontDestroyOnLoad(loadingView.transform.parent.gameObject);
		}else {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
