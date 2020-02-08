using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineObject : MonoBehaviour
{
    private OVRGrabber grabber;
    private OVRGrabber anotherGrabber;
    public GameObject anotherController;
    // Start is called before the first frame update
    void Start()
    {   
        grabber = GetComponent<OVRGrabber>();
        anotherGrabber = anotherController.GetComponent<OVRGrabber>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabber.grabbedObject==null||anotherGrabber.grabbedObject==null){
            return;
        }

            if(OVRInput.GetUp(OVRInput.Button.One)){
                if(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null&&anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null
                ||anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null&&grabber.grabbedObject.gameObject.GetComponent<FixedJoint>().connectedBody!=anotherGrabber.grabbedObject.grabbedRigidbody
                ||grabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null&&anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>().connectedBody!=grabber.grabbedObject.grabbedRigidbody){
                grabber.grabbedObject.gameObject.AddComponent<FixedJoint>();
                grabber.grabbedObject.grabbedRigidbody.GetComponent<FixedJoint>().connectedBody=anotherGrabber.grabbedObject.grabbedRigidbody;
                grabber.grabbedObject.grabbedRigidbody.GetComponent<FixedJoint>().enableCollision=true;
                }else{
                    if(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>()!=null){
                        Destroy(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>());
                    }
                    if(anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>()!=null){
                        Destroy(anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>());
                    }
                }
                
                }
        
        
    }
}

