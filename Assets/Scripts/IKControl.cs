using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{
        
    protected Animator animator;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform lookAtObj = null;
    public Transform headObj = null;
    private Quaternion preRotation;
    private Vector3 prePosition;
    private float movedDistance=0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
        preRotation = headObj.rotation;
        prePosition = headObj.position;
    }   

    void OnAnimatorIK(){

             if(animator){
            //手のトラッキングによるIK
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);
            animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position- new Vector3(0,0,0));
            animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation*Quaternion.AngleAxis(-110,Vector3.forward));

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandObj.position- new Vector3(0,0,0));
            animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObj.rotation*Quaternion.AngleAxis(110,Vector3.forward));

            //頭の向き。Weightはサイト参照。x軸周りの回転は反映されない（rotationが反映されない？）
            animator.SetLookAtPosition(lookAtObj.position);
            animator.SetLookAtWeight(1.0f,0.2f,0.9f,0.7f,0.4f);
            
            //HMDの前後の傾きによる前後の調整（→下を向くと前に出る）
            float adjz = 0.15f*Mathf.Sin(headObj.rotation.eulerAngles.x/180*Mathf.PI);
            //HMDの前後の傾きによる高さの調整（→下を向くと下に下がる）cosだと上を向いた場合も正になるから負に
            float adjy = 0.25f-0.25f*Mathf.Cos(headObj.rotation.eulerAngles.x/180*Mathf.PI);
            if(headObj.rotation.eulerAngles.x>180){
                adjy*=-1;
            }
            //上の調整分をHMDの向きに対して加える
            animator.bodyPosition = headObj.position+Quaternion.FromToRotation(Vector3.forward,Vector3.ProjectOnPlane(headObj.forward,Vector3.up))*(new Vector3(0,-0.55f+adjy,-adjz-0.1f));
            //Delay bodyRotation reaction 
            Quaternion qua = Quaternion.Slerp(preRotation,headObj.rotation,0.03f);
            preRotation = qua;
            //体にはy軸だけの回転を反映させる
            animator.bodyRotation=Quaternion.FromToRotation(transform.forward,Vector3.ProjectOnPlane(qua*transform.forward,Vector3.up));
            

            movedDistance +=Vector3.Distance(prePosition,new Vector3(animator.bodyPosition.x,prePosition.y,animator.bodyPosition.z));
            float footPosition = 0.15f*Mathf.Sin(10f*movedDistance);
            
            prePosition = animator.bodyPosition;
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1);
            //animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1);
            animator.SetIKPosition(AvatarIKGoal.RightFoot,new Vector3(animator.bodyPosition.x,0.1f,animator.bodyPosition.z)+Quaternion.FromToRotation(Vector3.forward,Vector3.ProjectOnPlane(animator.bodyRotation*Vector3.forward,Vector3.up))*(new Vector3(0.1f,0,footPosition)));
            //animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
            
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1);
            //animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,1);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot,new Vector3(animator.bodyPosition.x,0.1f,animator.bodyPosition.z)+Quaternion.FromToRotation(Vector3.forward,Vector3.ProjectOnPlane(animator.bodyRotation*Vector3.forward,Vector3.up))*(new Vector3(-0.1f,0,-footPosition)));
            //animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);


        }

     }

    // void OnAnimatorMove(){
    //     transform.position = 
    //     transform.rotation = headObj.rotation;     
    // }

    // Update is called once per frame



    void Update()
    {   
       
   
    }

}

