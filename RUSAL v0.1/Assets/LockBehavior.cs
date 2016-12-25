using UnityEngine;
using System.Collections;

public class LockBehavior : MonoBehaviour
{
	#region Public Fields

	[SerializeField]
	Camera camera;

	[SerializeField]
	string tag;

	#endregion

	#region Private

	private Transform previousTarget;

	private Camera2DFollow trackingBehavior;

	private bool isLocked = false;

	#endregion

	// Use this for initialization
	void Start()
	{
		trackingBehavior = camera.GetComponent<Camera2DFollow>();
		previousTarget = transform;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == tag && !isLocked)
		{
			isLocked = true;
			PushTarget();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == tag && isLocked)
		{
			isLocked = false;
			PopTarget(other.transform);
		}
	}

	private void PushTarget()
	{
		trackingBehavior.trackingTarget = previousTarget;
	}

	private void PopTarget(Transform newTarget)
	{
		trackingBehavior.trackingTarget = newTarget;
	}

}
