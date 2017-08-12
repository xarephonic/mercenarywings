using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataClasses;
using UnityEngine.SceneManagement;

public class MissionLoadoutDataKeeper : MonoBehaviour {

	public static MissionLoadoutDataKeeper instance;

	public List<PlaneVO> planesToTakeIntoMission = new List<PlaneVO>();

	// Use this for initialization
	void Awake () {
		if(instance == null)
		{
			instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start () {
        SceneManager.sceneLoaded += delegate (Scene scene, LoadSceneMode lsm)
        {
            if(scene.name == "Hangar")
            {
                planesToTakeIntoMission.Clear();
            }
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
