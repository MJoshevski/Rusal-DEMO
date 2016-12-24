using UnityEngine;
using System.Collections;


[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2; //the offset so we don't get errors

	//these are used for checking if we need to instatiate buddies
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

	//used if the object isn't tilable
	public bool reverseScale = false;

	//width of our element
	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;

	void Awake ()
	{
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		//does it still need buddies, if not do nothing
		if (hasALeftBuddy == false || hasARightBuddy == false){
			// calculate the cameras extend (half the width) of what the camera cann see in pixels
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

			//calculate the x position where the camera can see the edge of the sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;

			//checking if we can see the edge of the element and then calling MakeNewBuddy if we can
			if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBuddy(1);
				hasARightBuddy = true;
			}
			else if(cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
				MakeNewBuddy(-1);
				hasALeftBuddy = true;
			}
		}
	
	}

	//a function that creates a buddy on the side required
	void MakeNewBuddy (int rightOrLeft){
		// calculating the new position for our new buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);

		// instantiating our new buddy and storing him in a variable
		Transform newBuddy = (Transform) Instantiate (myTransform, newPosition,myTransform.rotation);

		//if not tilable let's reverse the x size of our object to get rid of ugly backgrounds
		if (reverseScale == true){
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		//parenting cloned tiles so we don't get a mess in hierarchy
		newBuddy.parent = myTransform.parent;

		//checks if the newly created buddy has a buddy which is the old clone from which newBuddy was created
		if(rightOrLeft > 0){
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}
		else{
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}

	}
}
