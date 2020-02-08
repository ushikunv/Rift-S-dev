using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCopy : MonoBehaviour
{   
    public Animator IKAnimator=null;
    private Animator thisAnimator=null;
    private HumanPose humanPose;
    private HumanPoseHandler thisHandler;
    private HumanPoseHandler IKHandler;
    public Transform headObj;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator=GetComponent<Animator>();
        thisHandler = new HumanPoseHandler(thisAnimator.avatar,thisAnimator.transform);
        IKHandler = new HumanPoseHandler(IKAnimator.avatar,IKAnimator.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //GetでIKされたhumanPoseを読み取る
        IKHandler.GetHumanPose(ref humanPose);
        
        //IKControlのSetLookAtPositionでは反映されないｘ軸周りの回転を反映させる。オイラーでとると通常状態が０度（→360）になる
        float zAngle = headObj.transform.eulerAngles.z;
        if(zAngle>180){
            zAngle-=360f;
        }
        humanPose.muscles[10]=-zAngle/45f;
        
        //手のグッパー。Primaryが左手
            for(int i=55;i<=74;i++){
                float readValue=OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger);
                humanPose.muscles[i]=-readValue+0.5f;   
            }

            for(int i=75;i<=94;i++){
                float readValue=OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger);
                humanPose.muscles[i]=-readValue+0.5f;
            }
        
        //modifyしたhumanPoseをSetする
        thisHandler.SetHumanPose(ref humanPose);
    }

        [ContextMenu("HumanPoseの配列に対応したリグ名を表示")]
    private void ShowMuscleList()
    {
        string[] muscleName = HumanTrait.MuscleName;
        int i = 0;
        while (i < HumanTrait.MuscleCount) {
            Debug.Log(i + " : " + muscleName[i]+" min: " + HumanTrait.GetMuscleDefaultMin(i) + " max: " + HumanTrait.GetMuscleDefaultMax(i));
 
            i++;
        }
    }
}
