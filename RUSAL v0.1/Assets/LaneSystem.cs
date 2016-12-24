using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaneSystem : MonoBehaviour {


	[SerializeField]
	Transform reference; //what lane is referenced?

	[SerializeField]
	List<Transform> lanes; //array of available lanes

	[SerializeField]
	float followSpeed = 5f; //follow speed of lane switch


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float targetYCoord = transform.position.y;
		if (lanes.Count > 1) {
			int i = 0;
			for (i = 0; i < lanes.Count - 1; i++) {
				if ((reference.position.y > lanes [i].position.y) && (reference.position.y <= lanes [i + 1].position.y)) {
					targetYCoord = lanes [i + 1].position.y;
					break;
				}
			}
			if (i == lanes.Count - 1)
				targetYCoord = lanes [lanes.Count - 1].position.y;
		} else {
			targetYCoord = lanes [0].position.y;
		}
		float yCoord = Mathf.Lerp (transform.position.y, targetYCoord, Time.deltaTime * followSpeed);
		transform.position = new Vector3 (transform.position.x, yCoord, transform.position.z);
	}
}
