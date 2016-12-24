using UnityEngine;
using System.Collections;

public class DodgeAbility : MonoBehaviour {
	public DodgeState dodgeState;
	public float dodgeTime;
	public float maxDodge = 20f;
	public float dodgeForceStill;

	public playerController instance;

	public Vector2 savedVelocity;
	public Rigidbody2D MyRB;
	Animator myAnim;

	void Start ()
	{
		MyRB = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator>();
		instance = GetComponent<playerController>();

	}

	void Update () 
	{
		switch (dodgeState) 
		{
		case DodgeState.Ready:
			var isDashKeyDown = Input.GetKeyDown (KeyCode.LeftShift);
			if(isDashKeyDown && instance.OnGround)
			{
				if (MyRB.velocity.x < 0.1f && MyRB.velocity.x >= 0) {
					if (instance.facingRight) {
						savedVelocity = MyRB.velocity;
						MyRB.AddForce (new Vector2 (-dodgeForceStill, 0));
						dodgeState = DodgeState.Dodging;
						myAnim.SetBool ("hasDodged", false);
						break;
					} else {
						savedVelocity = MyRB.velocity;
						MyRB.AddForce (new Vector2 (dodgeForceStill, 0));
						dodgeState = DodgeState.Dodging;
						myAnim.SetBool ("hasDodged", false);
						break;
					}
				} else if (instance.facingRight) {
					savedVelocity = MyRB.velocity;
					MyRB.AddForce(new Vector2(dodgeForceStill, 0));
					dodgeState = DodgeState.Dodging;
					myAnim.SetBool("hasDodged", false);
				} else {
					savedVelocity = MyRB.velocity;
					MyRB.AddForce(new Vector2(-dodgeForceStill, 0));
					dodgeState = DodgeState.Dodging;
					myAnim.SetBool("hasDodged", false);
				}


			}
			break;

		case DodgeState.Dodging:
			dodgeTime += Time.deltaTime * 3;
			if(dodgeTime >= maxDodge)
			{
				dodgeTime = maxDodge;
				MyRB.velocity = savedVelocity;
				dodgeState = DodgeState.Cooldown;
				myAnim.SetBool("hasDodged", true);
			}
			break;

		case DodgeState.Cooldown:
			dodgeTime -= Time.deltaTime;
			if(dodgeTime <= 0)
			{
				dodgeTime = 0;
				dodgeState = DodgeState.Ready;

			}
			break;
		}
	}
	public enum DodgeState 
	{
		Ready,
		Dodging,
		Cooldown
	}
}




