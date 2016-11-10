using UnityEngine;
using System.Collections;

public class PVRCItem : MonoBehaviour {
    /// <summary>
    /// A light-weighted NewtonVR implementantion, developped by Wei
    /// </summary>


    public Transform interactionPoint;

    private const float VelocityMagic = 6000f;
    private const float MaxVelocityChange = 10f;


    private bool isAttached = false;
    private GameObject hand = null;

    public Rigidbody Rigidbody;

    public void setAttached(GameObject hand)
    {
        isAttached = !isAttached;
        this.hand = hand;
    }

	// Use this for initialization
	void Start () {
        Rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    protected virtual void FixedUpdate()
    {
        if (isAttached)
        {

            Quaternion rotationDelta;
            Vector3 positionDelta = Vector3.zero;

            if(interactionPoint != null)
            {
                positionDelta = hand.transform.position - interactionPoint.position;
            }

            Vector3 velocityTarget = (positionDelta * VelocityMagic) * Time.fixedDeltaTime;

            if (float.IsNaN(velocityTarget.x) == false)
            {
                this.Rigidbody.velocity = Vector3.MoveTowards(this.Rigidbody.velocity, velocityTarget, MaxVelocityChange);
            }
        }
    }

}
