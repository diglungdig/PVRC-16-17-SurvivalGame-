using UnityEngine;
using System.Collections;

public class Grab : MonoBehaviour {
    //developped by Wei the Master
 
    public bool isGrabbing = false;
    public float maxDistanceToGrab = 0.2f;

    public GameObject hand = null;
    public SteamVR_TrackedObject controller;
    SteamVR_Controller.Device device;

    [SerializeField]
    private GameObject grabbedGameObject = null;

    // Hand (low poly) by anura is licensed under CC Attribution
    // Use this for initialization
    void Start () {
        //safe check
        if (hand == null)
        {
            Debug.LogError("Hand Model is not attached");
        }
        if(GetComponent<SteamVR_TrackedObject>() == null)
        {
            Debug.LogError("Grab script must be attached with a SteamVR Object");
        }
        if((controller = GetComponent<SteamVR_TrackedObject>()) == null)
        {
            Debug.LogError("This object doesnt have a trackObject component");
        }

        //assign
        device = SteamVR_Controller.Input((int)controller.index);
    }
	
    void startGrabbing(GameObject thing)
    {
        hand.SetActive(false);
        thing.transform.SetParent(gameObject.transform);
        thing.transform.localPosition = Vector3.zero;
        thing.GetComponent<Rigidbody>().isKinematic = false;
    }

    void dropOrThrow(GameObject thing)
    {
        //set isGrabbing to false
        //give velocity
        hand.SetActive(true);
        thing.transform.SetParent(null);
        thing.GetComponent<Rigidbody>().isKinematic = true;
    }

    GameObject checkSphereAroundHand(Vector3 centerPoint, float distance)
    {
        //overlap sphere around the center, fetch the nearest one if there is
        //need optimization?
        Collider[] hits = Physics.OverlapSphere(centerPoint, distance);
        GameObject temp = null;
        
        //2 cases
        if(hits.Length > 0)
        {
            //loop check the nearest one
            float nearDistance = distance;

            foreach (Collider a in hits)
            {
                if(Vector3.Distance(a.transform.position, centerPoint) < nearDistance)
                {
                    nearDistance = Vector3.Distance(a.transform.position, centerPoint);
                    temp = a.gameObject;
                }
            }
        }
        return temp;
    }

	// Update is called once per frame
	void Update () {
        if((device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) && (grabbedGameObject=checkSphereAroundHand(transform.position, maxDistanceToGrab)) != null && isGrabbing == false)
        {
            //grab
            //do something to grabbedGameObject
            isGrabbing = true;
            startGrabbing(grabbedGameObject);
        }
        else if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) && grabbedGameObject != null && isGrabbing)
        {
            //drop or throw
            isGrabbing = false;
            dropOrThrow(grabbedGameObject);
        }
	}
}
