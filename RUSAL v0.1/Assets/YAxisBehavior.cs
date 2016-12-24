using UnityEngine;
using System.Collections;

public class YAxisBehavior : MonoBehaviour
{
	#region Public Fields

	[SerializeField]
	Camera camera;

	[SerializeField]
	string tag;

	[SerializeField]
	float YOffsetValue=10;

	#endregion

	#region Private

	private float previousOffset;

	private Camera2DFollow trackingBehavior;

	private bool isOffsetted = false;

	#endregion

	// Use this for initialization
	void Start()
	{
		trackingBehavior = camera.GetComponent<Camera2DFollow>();

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == tag && !isOffsetted)
		{
			isOffsetted = true;
			PushTarget(YOffsetValue);
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == tag && isOffsetted)
		{
			isOffsetted = false;
			PopTarget();
		}
	}

	private void PushTarget(float YOffset)
	{
		previousOffset = trackingBehavior.YOffset;
		trackingBehavior.YOffset = YOffset;
	}

	private void PopTarget()
	{
		
		trackingBehavior.YOffset = previousOffset;
	}


}


