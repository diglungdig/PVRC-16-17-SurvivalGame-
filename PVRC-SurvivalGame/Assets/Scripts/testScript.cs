using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

    public SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;

    public GameObject axe;

	// Use this for initialization
	void Start () {

        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
    }
	
	// Update is called once per frame
	void Update () {
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Instantiate(axe);
        }
	}
}
