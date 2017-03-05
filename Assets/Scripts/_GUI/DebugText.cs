using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugText : MonoBehaviour {

	public static DebugText inst;

	public Text t;

	List<string> lines = new List<string>();

	public void AddLine(string newLine){
		t.text = "";

		lines.Add(newLine);

		int iterations = Mathf.Min(3,lines.Count);

		for (int i = 0; i < iterations; i++) {
			t.text += "\n"+lines[lines.Count-i-1];
		}
	}
	// Use this for initialization
	void Awake () {
		inst = this;

		DontDestroyOnLoad(gameObject);

		t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
