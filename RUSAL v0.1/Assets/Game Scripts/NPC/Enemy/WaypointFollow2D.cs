using UnityEngine;
using System.Collections;

public class WaypointFollow2D : MonoBehaviour {

	public float accel = 0.8f; //This is the rate of accelleration after the function "Accell()" is called. Higher values will cause the object to reach the "speedLimit" in less time.

	public float inertia = 0.9f; //This is the the amount of velocity retained after the function "Slow()" is called. Lower values cause quicker stops. A value of "1.0" will never stop. Values above "1.0" will speed up.

	public float speedLimit = 10.0f; //This is as fast the object is allowed to go.

	public float minSpeed = 1.0f; //This is the speed that tells the functon "Slow()" when to stop moving the object.

	public float stopTime = 1.0f; //This is how long to pause inside "Slow()" before activating the function "Accell()" to start the script again.

	//This variable "currentSpeed" is the major player for dealing with velocity.
	//The "currentSpeed" is mutiplied by the variable "accel" to speed up inside the function "accell()".
	//Again, The "currentSpeed" is multiplied by the variable "inertia" to slow things down inside the function "Slow()".
	private float currentSpeed = 0.0f;

	//The variable "functionState" controlls which function, "Accell()" or "Slow()", is active. "0" is function "Accell()" and "1" is function "Slow()".
	private float functionState = 0f;

	//The next two variables are used to make sure that while the function "Accell()" is running, the function "Slow()" can not run (as well as the reverse).
	private bool accelState;
	private bool slowState;

	//This variable will store the "active" target object (the waypoint to move to).
	private Transform waypoint;

	//This is the speed the object will rotate to face the active Waypoint.
	public float rotationDamping = 6.0f;

	//If this is false, the object will rotate instantly toward the Waypoint. If true, you get smoooooth rotation baby!
	public bool smoothRotation = true;

	//This variable is an array. []< that is an array container if you didnt know. It holds all the Waypoint Objects that you assign in the inspector.
	public Transform [] waypoints;

	//This variable keeps track of which Waypoint Object, in the previously mentioned array variable "waypoints", is currently active.
	private int WPindexPointer;
	//previous Waypoint
	public int WPStoreGlobal;

	//Wallchecking vars
	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;


	public bool completedPatrol=false;
	public bool moveRight;

	Animator enemyAnim;
	Rigidbody2D enemyRB;



	void Start ()
	{
		functionState = 0f; //When the script starts set "0" or function Accell() to be active.
		enemyAnim = GetComponent<Animator>();
		enemyRB = GetComponent<Rigidbody2D>();
	}




	void Update ()
	{
		if (functionState == 0) //If functionState variable is currently "0" then run "Accell()". Withouth the "if", "Accell()" would run every frame.
		{
			Accell ();
		}
		if (functionState == 1) //If functionState variable is currently "1" then run "Slow()". Withouth the "if", "Slow()" would run every frame.
		{
			Slow ();
		}
		waypoint = waypoints[WPindexPointer]; //Keep the object pointed toward the current Waypoint object.
	}

	void FixedUpdate ()
	{
		
		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);

		float moveDirection = enemyRB.velocity.x;
		//flipping direction
//		if(moveDirection<=0){
//			moveRight = !moveRight;
//			Flip (enemyRB.velocity.x);
//		}
		//flipping if enemy hits wall
		if (hittingWall) {
			moveRight = !moveRight;
			Flip (enemyRB.velocity.x);
		} else if (enemyRB.velocity.x > 0) {
			moveRight = true;
			Flip (enemyRB.velocity.x);

		} else if (enemyRB.velocity.x < 0) {
			moveRight = false;
			Flip (enemyRB.velocity.x);
		}
	}



	void Accell ()
	{
		if (accelState == false) 	//
		{                   		//
			accelState = true;     //Make sure that if Accell() is running, Slow() can not run.
			slowState = false;    //
		}

	
		//Now do the accelleration toward the active waypoint untill the "speedLimit" is reached
		currentSpeed = currentSpeed + accel * accel;

		if (WPStoreGlobal != WPindexPointer) {
			if (WPStoreGlobal > WPindexPointer) {
				transform.Translate (Time.deltaTime * currentSpeed * -1, 0, 0);
				enemyAnim.SetFloat ("speed", Time.deltaTime * currentSpeed);
			} else {
				transform.Translate (Time.deltaTime * currentSpeed, 0, 0);
				enemyAnim.SetFloat ("speed", Time.deltaTime * currentSpeed);
			}
		}

//		transform.Translate (0,0,0);
//		enemyAnim.SetFloat ("speed", Time.deltaTime * currentSpeed);

		//When the "speedlimit" is reached or exceeded ...
		if (currentSpeed >= speedLimit)
		{
			// ... turn off accelleration and set "currentSpeed" to be exactly the "speedLimit". Without this, the "currentSpeed will be slightly above "speedLimit"
			currentSpeed = speedLimit;
		}
	}




	//The function "OnTriggerEnter" is called when a collision happens.
	void OnTriggerEnter2D (Collider2D other)
	{
		functionState = 1; //When the GameObject collides with the waypoint's collider, activate "Slow()" by setting "functionState" to "1".
		WPStoreGlobal = WPindexPointer;
		Debug.Log ("Pointerot bese na " + WPStoreGlobal.ToString ());
		//WPindexPointer = Random.Range(0, waypoints.Length);  //When the GameObject collides with the waypoint's collider, change the active waypoint to the next one in the array variable "waypoints".
		if (WPindexPointer < waypoints.Length && !completedPatrol) {
			WPindexPointer++;
		}
		Debug.Log ("Pointerot sega e na " + WPindexPointer.ToString ());

		//When the array variable reaches the end of the list ...
		if (WPindexPointer >= waypoints.Length)
			completedPatrol = true;
		
		if (completedPatrol) {
			WPindexPointer--;
			if (WPindexPointer <= 0)
				completedPatrol = false;
		}
	}


	public void Slow(){
		StartCoroutine ("SlowCo");
	}

	public IEnumerator SlowCo ()
	{
		if (slowState == false) 
		{                  
			accelState = false; 	//Make sure that if Slow() is running, Accell() can not run.
			slowState = true;  
		}                  

		//Begin to do the slow down (or speed up if inertia is set above "1.0" in the inspector).
		currentSpeed = currentSpeed * inertia;
		transform.Translate (Time.deltaTime * currentSpeed, 0, 0);
		enemyAnim.SetFloat ("speed", currentSpeed);

		//When the "minSpeed" is reached or exceeded ...
		if (currentSpeed <= minSpeed)
		{
			currentSpeed = 0.0f; 	// ... Stop the movement by setting "currentSpeed to Zero.
			yield return new WaitForSeconds (stopTime); 	//Wait for the amount of time set in "stopTime" before moving to next waypoint.
			functionState = 0;	  //Activate the function "Accell()" to move to next waypoint.
		}
	}

	private void Flip(float move)
	{
		if (move > 0 && !moveRight || move < 0 && moveRight)
		{
			//moveRight = !moveRight;
			Vector3 thisScale = transform.localScale;
			thisScale.x *= -1;
			transform.localScale = thisScale;
		}
	}

}
