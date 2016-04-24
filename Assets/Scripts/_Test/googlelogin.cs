using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System;

public class googlelogin : MonoBehaviour {

    public Text log;

	// Use this for initialization
	void Start () {
        Social.localUser.Authenticate(delegate(bool b)
        {
            log.text = Social.Active.localUser.userName;
            Debug.Log(Social.Active.localUser.userName);
            Debug.Log(b);

            
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
