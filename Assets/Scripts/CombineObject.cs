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
                if(!grabber.grabbedObject.combined&&!anotherGrabber.grabbedObject.combined){
                //どっちも結合してない
                FixedJoint fj = grabber.grabbedObject.gameObject.AddComponent<FixedJoint>();
                fj.connectedBody=anotherGrabber.grabbedObject.grabbedRigidbody;
                fj.enableCollision=true;
                grabber.grabbedObject.name=Time.frameCount.ToString();
                anotherGrabber.grabbedObject.name=Time.frameCount.ToString()+"a2";
                grabber.grabbedObject.combined=true;
                anotherGrabber.grabbedObject.combined=true;
                grabber.grabbedObject.combinedObj.Add(anotherGrabber.grabbedObject.gameObject);
                anotherGrabber.grabbedObject.combinedObj.Add(grabber.grabbedObject.gameObject);

                }else if(anotherGrabber.grabbedObject.combined){
                //anotherは結合している場合
                FixedJoint fj = grabber.grabbedObject.gameObject.AddComponent<FixedJoint>();
                fj.connectedBody=anotherGrabber.grabbedObject.grabbedRigidbody;
                fj.enableCollision=true;
                //anotherの結合してるオブジェクトと結合させる
                foreach(GameObject gm in anotherGrabber.grabbedObject.combinedObj){
                FixedJoint fj2 = grabber.grabbedObject.gameObject.AddComponent<FixedJoint>();
                fj2.connectedBody=gm.GetComponent<Rigidbody>();
                fj2.enableCollision=true;
                grabber.grabbedObject.combinedObj.Add(gm);
                gm.GetComponent<OVRGrabbable>().combinedObj.Add(grabber.grabbedObject.gameObject);
                }
                grabber.grabbedObject.combined=true;
                grabber.grabbedObject.combinedObj.Add(anotherGrabber.grabbedObject.gameObject);
                anotherGrabber.grabbedObject.combinedObj.Add(grabber.grabbedObject.gameObject);

                }else{
                    if(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>()!=null){
                        Destroy(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>());
                    }
                    if(anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>()!=null){
                        Destroy(anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>());
                    }
                }
            }

                //         if(OVRInput.GetUp(OVRInput.Button.One)){
                // if(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null&&anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null
                // ||anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null&&grabber.grabbedObject.gameObject.GetComponent<FixedJoint>().connectedBody!=anotherGrabber.grabbedObject.grabbedRigidbody
                // ||grabber.grabbedObject.gameObject.GetComponent<FixedJoint>()==null&&anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>().connectedBody!=grabber.grabbedObject.grabbedRigidbody){
                // grabber.grabbedObject.gameObject.AddComponent<FixedJoint>();
                // grabber.grabbedObject.grabbedRigidbody.GetComponent<FixedJoint>().connectedBody=anotherGrabber.grabbedObject.grabbedRigidbody;
                // grabber.grabbedObject.grabbedRigidbody.GetComponent<FixedJoint>().enableCollision=true;
                // }else{
                //     if(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>()!=null){
                //         Destroy(grabber.grabbedObject.gameObject.GetComponent<FixedJoint>());
                //     }
                //     if(anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>()!=null){
                //         Destroy(anotherGrabber.grabbedObject.gameObject.GetComponent<FixedJoint>());
                //     }
                // }
                
                // }
        
        
    }
}

