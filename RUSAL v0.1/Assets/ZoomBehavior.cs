using UnityEngine;
using System.Collections;

public class ZoomBehavior : MonoBehaviour
{
	#region Public Fields

	[SerializeField]
	Camera camera;

	[SerializeField]
	string tag;

	[SerializeField]
	float zoomFactor = 1.0f;

	[SerializeField]
	float zoomSpeed = 5.0f;

	[SerializeField]
	float duration = 1.0f;

	#endregion

	#region Private

	private float originalSize = 0f;

	private bool isZoomed = false;

	private bool zoomIn = false;

	private bool zoomOut = false;

	private float elapsed = 0.0f;

	private float targetSize;
	#endregion

	// Use this for initialization
	void Start()
	{
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera> ();
		originalSize = camera.orthographicSize;
		targetSize = originalSize * zoomFactor;
	}

	void Update() 
	{
		if (zoomIn) 
		{
			elapsed += (Time.deltaTime / duration) * zoomSpeed;
			if (targetSize != camera.orthographicSize) 
				{
					camera.orthographicSize = Mathf.SmoothStep (camera.orthographicSize, targetSize, elapsed);
				}
			if (elapsed > 1.0f) 
				{
					zoomIn = false;
				}
		}

		if (zoomOut)
		{
			elapsed += (Time.deltaTime / duration) * zoomSpeed;
			if (camera.orthographicSize != originalSize) 
				{
					camera.orthographicSize = Mathf.SmoothStep (camera.orthographicSize, originalSize, elapsed);
				}
			if (elapsed > 1.0f) 
				{
					zoomOut = false;
				}
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == tag && !isZoomed)
		{
			isZoomed = true;
			zoomIn = true;
			elapsed = 0.0f;
		}


	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == tag && isZoomed)
		{
			isZoomed = false;
			zoomOut = true;
			elapsed = 0.0f;
		}
	}

}
