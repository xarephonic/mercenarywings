using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Facebook;
using Facebook.Unity;
using Firebase;

public class Authenticator : MonoBehaviour {

    FirebaseAuth auth;

    public void FBLoginToFirebase (string accessToken)
    {
        Credential credential = FacebookAuthProvider.GetCredential(accessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            LoginComplete(newUser.UserId);
        });
    }

    public void FBLoginPrompt ()
    {
        Debug.Log("FB is initialized? " + FB.IsInitialized);

        var perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            FBLoginToFirebase(aToken.TokenString);
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public void AnonLoginPrompt()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            LoginComplete(newUser.UserId);
        });
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void LoginComplete (string userId)
    {
        Debug.Log("LOGIN COMPLETE");

        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://mercenary-wings-94494733.firebaseio.com/");

        FirebaseDatabase.DefaultInstance
            .GetReference("playerPlanes/" + userId)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception.Message);
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    Debug.Log(snapshot.GetRawJsonValue());

                    if(snapshot.GetRawJsonValue() == null)
                    {
                        GivePlayerStartingPlanes(userId);
                    } else
                    {
                        StartPullingInData(userId);
                    }
                }
            });
    }

    private void GivePlayerStartingPlanes (string userId)
    {
        Debug.Log("Give Player Starting Planes");

        FirebaseDatabase.DefaultInstance
            .GetReference("playerPlanes/"+userId)
            .SetRawJsonValueAsync("[2,2,2,2]").ContinueWith(task =>
            {
                if(task.IsFaulted)
                {
                    Debug.Log(task.Exception.Message);
                } else if(task.IsCompleted)
                {
                    Debug.Log("Gave player with id: " + userId + " starting planes");
                    StartPullingInData(userId);
                }
            });
    }

    private void StartPullingInData(string userId)
    {
        AssetKeeper.instance.GetAllDataFromDB(userId);   
    }

    void Awake ()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    // Use this for initialization
    void Start () {

    auth = FirebaseAuth.DefaultInstance;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
