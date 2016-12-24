using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera2DFollow : MonoBehaviour {

	[SerializeField]
	public Transform target;

	[SerializeField]
	public float damping = 1;

	[SerializeField]
	float yOffset = 0;

	[SerializeField]
	float lookAheadFactor = 3;

	[SerializeField]
	float lookAheadReturnSpeed = 0.5f;

	[SerializeField]
	float lookAheadMoveThreshold = 0.1f;

	[SerializeField]
	float yPosRestriction;

	[SerializeField]
	float cameraDistance;  //how far does the camera pan when holding vision inputs

	[SerializeField]
	float btnHoldTime;  //how long does the player need to hold the button for the camera shift
	
	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;

	float nextTimeToSearch = 0;

	private	Transform kamera; //reference to camera's transform
	private float HoldTime; //temporary storing var

	public Transform trackingTarget {
		get{
			return target;
		}
		set{
			target = value;
		}
	}

	public float YOffset{
		get{
			return yOffset;
		}
		set{
			yOffset = value;
		}
	}




	// Use this for initialization
	void Start () {
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
		HoldTime = btnHoldTime;
		kamera = transform;
	}


	
	// Update is called once per frame
	void LateUpdate () {
		

		
		if (target == null) {
			FindPlayer ();
			return;
		}

		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
		}
		
		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ + Vector3.up * yOffset;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

		newPos = new Vector3 (newPos.x, Mathf.Clamp (newPos.y, yPosRestriction, Mathf.Infinity), newPos.z);

		transform.position = newPos;

		lastTargetPosition = target.position;		

		HandleVision();


	}

	void FindPlayer () {
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null)
				target = searchResult.transform;
			nextTimeToSearch = Time.time + 0.5f;
		}
	}

	void HandleVision()
	{
		kamera = transform;	
		if(Input.GetKey(KeyCode.S))
		{				
			HoldTime -= Time.deltaTime;
			if(HoldTime <= 0)
			{
				transform.Translate(new Vector3(0,-cameraDistance * Time.deltaTime,0));

			}
		}
		if(Input.GetKeyUp(KeyCode.S))
		{
			transform.Translate(new Vector3(0,kamera.position.y  * Time.deltaTime,0));
			HoldTime = btnHoldTime;
		}

		if(Input.GetKey(KeyCode.W))
		{				
			HoldTime -= Time.deltaTime;
			if(HoldTime <= 0)
			{
				transform.Translate(new Vector3(0,cameraDistance * Time.deltaTime,0));

			}
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			transform.Translate(new Vector3(0,kamera.position.y * Time.deltaTime,0));
			HoldTime = btnHoldTime;

		}
	}
}
