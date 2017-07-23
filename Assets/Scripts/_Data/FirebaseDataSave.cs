using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using DataClasses;

public class FirebaseDataSave : MonoBehaviour {

    DatabaseReference reference;

	// Use this for initialization
	void Start () {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://mercenary-wings-94494733.firebaseio.com/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
