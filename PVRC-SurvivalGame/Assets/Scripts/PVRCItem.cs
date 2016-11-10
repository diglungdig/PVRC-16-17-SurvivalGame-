using UnityEngine;
using System.Collections;

public class PVRCItem : MonoBehaviour {
    /// <summary>
    /// A light-weighted NewtonVR implementantion, developped by Wei
    /// </summary>


    public Transform interactionPoint;

    private const float VelocityMagic = 6000f;
    private const float MaxVelocityChange = 10f;
    private const float MaxAngularVelocityChange = 30f;
    private const float AngularVelocityMagic = 100f;

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

    protected virtual void FixedUpdate()
    {
        if (isAttached)
        {

            Quaternion rotationDelta = Quaternion.identity;
            Vector3 positionDelta = Vector3.zero;

            float angle;
            Vector3 axis;

            if(interactionPoint != null)
            {
                positionDelta = hand.transform.position - interactionPoint.position;
                rotationDelta = hand.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            }


            rotationDelta.ToAngleAxis(out angle, out axis);

            if (angle > 180)
                angle -= 360;

            if(angle != 0)
            {
                Vector3 angularTarget = angle * axis;
                if(float.IsNaN(angularTarget.x) == false)
                {
                    angularTarget = (angularTarget * AngularVelocityMagic) * Time.fixedDeltaTime;
                    this.Rigidbody.angularVelocity = Vector3.MoveTowards(this.Rigidbody.angularVelocity, angularTarget, MaxAngularVelocityChange);
                }
            }

            Vector3 velocityTarget = (positionDelta * VelocityMagic) * Time.fixedDeltaTime;

            if (float.IsNaN(velocityTarget.x) == false)
            {
                this.Rigidbody.velocity = Vector3.MoveTowards(this.Rigidbody.velocity, velocityTarget, MaxVelocityChange);
            }
        }
    }

}
