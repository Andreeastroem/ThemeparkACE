using UnityEngine;
using System.Collections;

public class CamTest : MonoBehaviour {

    public string DeviceName;

    private WebCamTexture wct;

	// Use this for initialization
	void Start () {
	    WebCamDevice[] devices = WebCamTexture.devices;
        DeviceName = devices[0].name;

        wct = new WebCamTexture(DeviceName, 100, 100, 12);

        renderer.material.mainTexture = wct;

        //wct.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
