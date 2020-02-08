using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    public Transform headObj;
    
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 primaryAxis =OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick);
        rb.velocity=Quaternion.FromToRotation(rb.transform.forward,Vector3.ProjectOnPlane(headObj.forward,Vector3.up))*(new Vector3(primaryAxis.x,0,primaryAxis.y ));
    }
}
