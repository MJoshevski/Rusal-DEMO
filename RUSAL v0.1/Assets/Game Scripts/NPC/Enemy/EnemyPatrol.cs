using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {

	public float moveSpeed;
	public bool moveRight;

	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;

	Animator enemyAnim;
	Rigidbody2D enemyRB;
	// Use this for initialization
	void Start () {
		enemyRB = GetComponent<Rigidbody2D> ();
		enemyAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);

		if (hittingWall)
			moveRight = !moveRight;
		
		if(moveRight){
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
	} else {
			transform.localScale = new Vector3 (1f, 1f, 1f);
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}
}

	private void Flip(float move)
	{
		if (move > 0 && !moveRight || move < 0 && moveRight)
		{
			moveRight = !moveRight;
			Vector3 thisScale = transform.localScale;
			thisScale.x *= -1;
			transform.localScale = thisScale;
		}
	}
}

