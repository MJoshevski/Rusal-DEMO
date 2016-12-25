using UnityEngine;
using System.Collections;

public class SmoothFollow2D : MonoBehaviour {


   
   
	public float xMargin; //Distance in the x axis the player can move before the camera follows
	public float yMargin; //Distance in the y axis the player can move before the camera follows
	public float xSmooth; //How smoothly the camera catches up with it's target movement in the x axis
	public float ySmooth; //How smoothly the camera catches up with it's target movement in the y axis
	public Vector2 maxXAndY;	//The maximum x and y coordinates the camera can have	
	public Vector2 minXAndY;	//The minimum x and y coordinates the camera can have	
   
	private Transform player; //reference to player's transform
	private	Transform kamera; //reference to camera's transform

	public float btnHoldTime;  //how long does the player need to hold the button for the camera shift
	private float HoldTime; //temporary storing var
	public float cameraDistance;  //how far does the camera pan when holding vision inputs


    void Awake()
    {
		//initializing reference
		player = GameObject.FindGameObjectWithTag("Player").transform;
		HoldTime = btnHoldTime;
	}

	void Update()
	{
		HandleVision();
	}

	bool CheckXMargin(){
	
		//Returns true if the distance between the camera and the player in the x axis is greater than the x margin
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}

	bool CheckYMargin(){

		//Returns true if the distance between the camera and the player in the y axis is greater than the x margin
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


    void FixedUpdate()
    {
		TrackPlayer();
    }


	void HandleVision()
	{
		kamera = transform;	
		if(Input.GetKey(KeyCode.S))
		{				
			HoldTime -= Time.deltaTime;
			if(HoldTime <= 0)
			{
				Debug.Log("vlegov 2 nadolu");
				transform.Translate(new Vector3(0,-cameraDistance * Time.deltaTime,0));

			}
		}
		if(Input.GetKeyUp(KeyCode.S))
		{
			transform.Translate(new Vector3(0,kamera.position.y  * Time.deltaTime,0));
			HoldTime = btnHoldTime;
			Debug.Log("vlegov 3 nadolu");
		}

		if(Input.GetKey(KeyCode.W))
		{				
			HoldTime -= Time.deltaTime;
			if(HoldTime <= 0)
			{
				Debug.Log("vlegov 2 nagore");
				transform.Translate(new Vector3(0,cameraDistance * Time.deltaTime,0));

			}
		}
		if(Input.GetKeyUp(KeyCode.W))
		{
			transform.Translate(new Vector3(0,kamera.position.y * Time.deltaTime,0));
			HoldTime = btnHoldTime;
			Debug.Log("vlegov 3 nagore");
		}
	}

	void TrackPlayer(){
		//By default the target x and y coordinates of the camera are it's current x and y coordinates
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		//If the player has moved beyond the x margin...
		if(CheckXMargin())
			//...the target x coordinate should be a Lerp between the camera's current x position and the player's current x position
			targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		//If the player has moved beyond the y margin...
		if(CheckYMargin())
			//...the target x coordinate should be a Lerp between the camera's current y position and the player's current x position
			targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		//the target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		//targetX = Mathf.Clamp(targetX,minXAndY.x,maxXAndY.x);
		//targetY = Mathf.Clamp(targetY,minXAndY.y,maxXAndY.y);

		//set the camera's position to the target position with the same z component
		transform.position = new Vector3 (targetX, targetY, transform.position.z);
		
	}

   
}


