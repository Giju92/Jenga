using UnityEngine.Networking;
using System.Collections;
using UnityEngine;


public class NetworkManager : BaseObserver
{

    void Start()
    {
        RequestDataFromURI(GameConstant.URI);
    }

    public void RequestDataFromURI(string URI)
    {
        // A correct website page.
        StartCoroutine(GetRequest(URI));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            
            Debug.Log($"Web result: {webRequest.result}");
            
            if(webRequest.result == UnityWebRequest.Result.Success)
                Notify(GameEventEnum.NETWORK_RESPONSE_READY, new object[] { webRequest.downloadHandler.text });
            else
                Notify(GameEventEnum.NETWORK_RESPONSE_READY, new object[] { "" });
        }
    }
}
