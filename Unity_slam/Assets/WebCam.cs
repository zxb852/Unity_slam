using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    public string deviceName;
    WebCamTexture camTexture;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            deviceName = devices[0].name;
            camTexture = new WebCamTexture(deviceName, 800, 600, 12);
            transform.GetComponent<MeshRenderer>().material.mainTexture = camTexture;
            camTexture.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
