using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Rigidbody2D MyRB;

	private Vector2 direction;


	// Use this for initialization
	void Start () {

		MyRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		MyRB.velocity = direction * speed;
	}

	//deletes knives when outside of screen
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	public void Initialize(Vector2 direction)
	{
		this.direction = direction;
	}
}
