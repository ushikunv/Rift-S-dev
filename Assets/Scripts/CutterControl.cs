using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterControl : MonoBehaviour
{
	public GameObject obj;
	private bool reBool = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        		if (OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger)>0.8) {
			if (reBool) {
				GameObject[] desObj = GameObject.FindGameObjectsWithTag ("obj");
				foreach (GameObject obj in desObj) {
					Destroy (obj);
				}
				Instantiate (obj);
				reBool = false;
				Invoke ("boolOn",0.5f);
			}
                }
        
    }


    	void boolOn(){
		reBool = true;
	}


}
