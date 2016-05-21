using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class googlelogin : MonoBehaviour {

    public Text log;

	public void GoogleLoginM(){
		Social.localUser.Authenticate((bool success) => {
			// handle success or failure
			log.text = success.ToString();
		});
	}

	// Use this for initialization
	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress.
			.EnableSavedGames()
			// registers a callback for turn based match notifications received while the
			// game is not running.
			.RequireGooglePlus()
			.Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
