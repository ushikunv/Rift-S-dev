using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPosition : MonoBehaviour {

	public GameObject mirror;
	public GameObject player;
	private Vector3 playerPos;
	private RenderTexture texture;
	private Plane firstPlane;
	private Quaternion firstRotation;
	private Vector3 closestPoint;
	public GameObject camera;
	private Camera mirrorCamera;
	public float mirrorSize = 5f;

	// Use this for initialization
	void Start () {
		texture= mirror.GetComponent<RenderTexture>();
		mirrorCamera = camera.GetComponent<Camera>();
		firstPlane=new Plane(mirror.transform.up, mirror.transform.position);
		firstRotation = mirror.transform.rotation; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
			closestPoint = firstPlane.ClosestPointOnPlane(player.transform.position);
			this.transform.SetPositionAndRotation(2*closestPoint -player.transform.position,Quaternion.identity);
			mirror.transform.SetPositionAndRotation(closestPoint, firstRotation);
			mirrorCamera.fieldOfView=180/Mathf.PI*2*Mathf.Atan(mirrorSize/Vector3.Distance(closestPoint,this.transform.position));
			}
}
