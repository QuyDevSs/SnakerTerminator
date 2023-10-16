using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
using UnityEngine.Networking;
using LTAUnityBase.Base.Network;
// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class Test : MonoBehaviour
{
    private void Start()
    {
        RestRequest request = new RestRequest("http://localhost:3000/test", UnityWebRequest.kHttpVerbPOST);
        JSONObject json = new JSONObject();
        json.Add("a", 1);
        json.Add("b", 78);
        request.AddBody(json.ToString());
        StartCoroutine(request.IeRequest(new ActionOnResponse(OnSuccess, OnError)));
    }

    void OnSuccess(string data)
    {
        Debug.Log(data);
    }
    void OnError(string data)
    {
        Debug.Log(data);
    }
    //void Start()
    //{
    //    // A correct website page.
    //    StartCoroutine(GetRequest("http://localhost:3000/4/2"));

    //    //JSONObject 
    //    // A non-existing page.
    //    //StartCoroutine(GetRequest("https://error.html"));
    //}

    //IEnumerator GetRequest(string uri)
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //    {
    //        // Request and wait for the desired page.
    //        yield return webRequest.SendWebRequest();

    //        string[] pages = uri.Split('/');
    //        int page = pages.Length - 1;

    //        switch (webRequest.result)
    //        {
    //            case UnityWebRequest.Result.ConnectionError:
    //            case UnityWebRequest.Result.DataProcessingError:
    //                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
    //                break;
    //            case UnityWebRequest.Result.ProtocolError:
    //                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
    //                break;
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //                break;
    //        }
    //    }
    //}
}