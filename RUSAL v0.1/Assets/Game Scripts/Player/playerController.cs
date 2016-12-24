using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	private static playerController instance;
	//player singleton
	public static playerController Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<playerController>();
			}
			return instance;
		}
	}

	//movement vars
	[SerializeField]
	private float maxSpeed;

	[SerializeField]
	private LayerMask layerMask;

	//grounding vars
	public bool grounded;
	public Transform top_left;
	public Transform bottom_right;
	public LayerMask ground_layers;

	//knife prefab
	[SerializeField]
	private GameObject knifePrefab;

	//knife position
	[SerializeField]
	private Transform knifePos;

	//jumping vars
	[SerializeField]
	private float jumpHeight;
	//public int dblJumpCount;
	public int jumpCount;
	public int jumpCountDbl;
	bool isGrounded;
	public bool jumped;
	public bool dblJumped;
	[SerializeField]
	private int dblJumpDecreaseFactor = 5;

	//variable jump vars
	public float jumpShortSpeed = 1000f;   // Velocity for the lowest jump
	bool jump = false;
	bool jumpCancel = false;


	//falling vars
	[SerializeField]
	private float fallTimeThreshold;
	public float fallingTimer = 0.0f;

	//standard vars
	Animator myAnim;
	public bool facingRight;
	public Rigidbody2D MyRB;
	BoxCollider2D boxCollider;

	//active vars
	public bool Attack
	{
		get;
		set;
	}
	public bool Throw
	{
		get;
		set;
	}
	public bool Jump
	{
		get;
		set;
	}
	public bool Dodge
	{
		get;
		set;
	}
	public bool OnGround
	{
		get;
		set;
	}
	public bool DoubleJump
	{
		get;
		set;
	}

	public bool Crouch
	{
		get;
		set;
	}


	// Use this for initialization
	void Start () {
		MyRB = GetComponent<Rigidbody2D>(); 
		myAnim = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
		facingRight = true;



	}

	// Update is called once per frame

	void Update()
	{
		HandleInput();
	}


	void FixedUpdate () {


		float move = Input.GetAxis("Horizontal");

		HandleMovement(move);

		Flip(move);

	}

	void LateUpdate(){

		//check if we are grounded
		grounded = Physics2D.OverlapArea(top_left.position, bottom_right.position, ground_layers);
		OnGround = grounded;
		myAnim.SetBool("isGrounded", grounded);

				if(OnGround)
					ResetValues();
	}

	private void HandleMovement(float move)
	{
		//falling functionality
		if(MyRB.velocity.y < 0 && !OnGround)
		{
			fallingTimer += Time.deltaTime;
			if(fallingTimer >= fallTimeThreshold)
			{
				myAnim.SetBool("isFalling", true);
			}
		}

		//able to move, if not attacking, throwing, dodging or crouching
		if(!Attack && !Dodge && !Throw && !Crouch)
		{
			MyRB.velocity = new Vector2(move * maxSpeed, MyRB.velocity.y);
		}

		if(Jump && jumpCount == 0 && !jumped) 
		{
			MyRB.velocity = new Vector2(0, 0);
			MyRB.AddForce(new Vector2(0, jumpHeight));
			jumpCount++;
			jumped = true;
			Debug.Log ("single jump");

		}
		// Cancel the jump when the button is no longer pressed
		if (jumpCancel)
		{
			if (MyRB.velocity.y > jumpShortSpeed)
				MyRB.velocity = new Vector2(MyRB.velocity.x, jumpShortSpeed);
				jumpCancel = false;
		}

		//double jump
		if (!Jump && !OnGround && DoubleJump && jumpCountDbl == 0)
		{
			MyRB.velocity = new Vector2(MyRB.velocity.x,0);
			MyRB.AddForce(new Vector2(0, (jumpHeight - jumpHeight / dblJumpDecreaseFactor)));
			Debug.Log("dbl jump");
			jumpCountDbl++;
			dblJumped = true;
		}

		//speed parameter in animator
		myAnim.SetFloat("speed", Mathf.Abs(move));

	}


	private void HandleInput()
	{
		//light-slash input
		if (Input.GetKeyDown (KeyCode.Mouse0)) 
		{	
			myAnim.SetTrigger("lightSlash");			
		}

		//throw input
		if(Input.GetKeyDown(KeyCode.Mouse1))
		{
			myAnim.SetTrigger("throw");
		}

		//jumping input
		if(Input.GetButtonDown("Jump"))
		{
			myAnim.SetTrigger("hasJumped");
		}

		if(Input.GetButtonUp("Jump") && !isGrounded)
		{
			jumpCancel = true;

		}

		//crouching input
		if(Input.GetKey(KeyCode.S))
		{
			if (OnGround) {
				myAnim.SetBool ("isCrouching", true);
				MyRB.velocity = Vector2.zero;

			}
		}

		if(Input.GetKeyUp(KeyCode.S))
		{

			myAnim.SetBool("isCrouching", false);

		}

		//dodge input
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			myAnim.SetTrigger("Dodge");
		}



	}	


	private void Flip(float move)
	{
		if (move > 0 && !facingRight || move < 0 && facingRight)
		{
			facingRight = !facingRight;
			Vector3 thisScale = transform.localScale;
			thisScale.x *= -1;
			transform.localScale = thisScale;
		}
	}



	public void ThrowKnife(int value)
	{
		if(!OnGround && value == 1 || OnGround && value==0)
		{
			if(facingRight)
			{
				GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0,0,180)));
				tmp.GetComponent<Knife>().Initialize(Vector2.right);
			}else
			{
				GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0,0,0)));
				tmp.GetComponent<Knife>().Initialize(Vector2.left);
			}
		}

	}

	public void CrouchSpeed(){

	}

	public void ResetValues(){
		fallingTimer = 0;
		jumpCount = 0;
		jumpCountDbl = 0;

	}


}

