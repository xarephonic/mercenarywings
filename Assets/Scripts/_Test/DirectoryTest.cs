using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class DirectoryTest : MonoBehaviour {

	public Text text;

	public void CheckDirectory(){
		if(!Directory.Exists(Path.Combine(Application.persistentDataPath,"hello")))
		{
			text.text = "does not exist";
		} else {
			text.text = "exists";
		}
	}

	public void CreateDirectory(){
		Directory.CreateDirectory(Path.Combine(Application.persistentDataPath,"hello"));
		text.text = "created directory at "+Path.Combine(Application.persistentDataPath,"hello");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
