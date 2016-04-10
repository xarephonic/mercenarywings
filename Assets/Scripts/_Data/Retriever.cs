using UnityEngine;
using SimpleJSON;
using System.Collections;

public class Retriever : MonoBehaviour {

    public static Retriever retriever;

    public delegate void RetrieveDataCallback(string s);

    public static IEnumerator RetrieveData(string url, WWWForm postForm = null,RetrieveDataCallback callback = null)
    {
        WWW w;

        if (postForm != null)
            w = new WWW(url, postForm);
        else
            w = new WWW(url);

        yield return w;

        callback(w.text);

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
