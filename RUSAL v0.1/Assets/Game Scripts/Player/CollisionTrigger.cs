using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour {

	private BoxCollider2D playerBoxCollider;
	private CircleCollider2D playerCircleCollider;

	[SerializeField]
	private BoxCollider2D platformCollider;

	[SerializeField]
	private BoxCollider2D platformTrigger;

	// Use this for initialization
	void Start () {
		playerBoxCollider = GameObject.Find("_MAIN CHARACTER").GetComponent<BoxCollider2D>();
		playerCircleCollider = GameObject.Find("_MAIN CHARACTER").GetComponent<CircleCollider2D>();
		Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
		}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "_MAIN CHARACTER")
		{
			Physics2D.IgnoreCollision(platformCollider, playerBoxCollider, true);
			Physics2D.IgnoreCollision(platformCollider, playerCircleCollider, true);

		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.name == "_MAIN CHARACTER")
		{
			Physics2D.IgnoreCollision(platformCollider, playerBoxCollider, false);
			Physics2D.IgnoreCollision(platformCollider, playerCircleCollider, false);

		}
	}
}
