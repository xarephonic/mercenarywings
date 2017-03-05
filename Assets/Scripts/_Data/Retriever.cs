using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.IO;
using System;

public class Retriever : MonoBehaviour {

    public static Retriever retriever;

    public delegate void RetrieveDataCallback(string s);

	public static IEnumerator RetrieveRemoteData(string url, WWWForm postForm = null,RetrieveDataCallback callback = null)
    {
        WWW w;

        if (postForm != null)
			w = new WWW(url, postForm);
        else
            w = new WWW(url);

        yield return w;

		if(!string.IsNullOrEmpty(w.error)){
			Debug.Log(w.error);
			callback("error");
		}
		else{
        	callback(w.text);
		}

    }

	public static IEnumerator RetrieveLocalData(string path, RetrieveDataCallback callback){

		string file = Directory.GetFiles(path)[0];

		Debug.Log("file: "+file);

		string data = File.ReadAllText(file);

		yield return true;

		callback(data);

	}

    void Awake()
    {
        if(retriever == null)
        {
            retriever = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }

    }
}
