using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabber : MonoBehaviour
{
    public Transform handAnchor;
    private List<GameObject> enteredObject = new List<GameObject>();

    private FixedJoint grabbedJoint;

    private Vector3 prePosition;
    private Vector3 preRotation;
    private bool nowGrab=false;

    public enum Hand{
        Right,
        Left
    }

    public Hand handName;

    // Start is called before the first frame update
    void Start()
    {
        grabbedJoint =gameObject.AddComponent<FixedJoint>();
        grabbedJoint.enableCollision=true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!nowGrab){
        if(getTriggerValue()>0.6){
            if(enteredObject.Count>0){
            grabbedJoint.connectedBody=enteredObject[0].GetComponent<Rigidbody>();
            nowGrab=true;
            }
        }
        }else{
        if(getTriggerValue()<0.1){
             Vector3 preVelocity= (enteredObject[0].transform.position - prePosition)/Time.fixedDeltaTime;
            Debug.Log(preVelocity);
            grabbedJoint.connectedBody=null;
            enteredObject[0].GetComponent<Rigidbody>().velocity=preVelocity;
            nowGrab=false;
            enteredObject.Clear();
            return;
        }  
        prePosition=enteredObject[0].transform.position;   
        }

        
    }

    float getTriggerValue(){

        if(handName.ToString()=="Left"){
        return OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger);
        }else{
        return OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger);
        }
        
    }

    void OnTriggerEnter(Collider collider){
        if(getTriggerValue()>0.8){
            return;
        }
        if(!nowGrab&&collider.gameObject.GetComponent<Grabbable>()){
        enteredObject.Add(collider.gameObject);
        }
    } 

    void OnTriggerExit(Collider collider){
        if(enteredObject.Count!=0){
        if(!nowGrab&&collider.gameObject.GetComponent<Grabbable>()){
        enteredObject.Remove(collider.gameObject);
        }
        }

    }

    
}
