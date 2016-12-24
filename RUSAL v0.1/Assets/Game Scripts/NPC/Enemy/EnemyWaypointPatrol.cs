using UnityEngine;
using System.Collections;

public class EnemyWaypointPatrol : MonoBehaviour {

	public Transform [] waypoint; //amount of waypoints you want

	public float patrolSpeed = 3f;	//walking speed between waypoints

	public bool loop = true;	//do you want to keep repeating waypoints?

	public float dampingLook = 6f;  //how slowly to turn

	public float pauseDuration = 0;   //how long to pause at a waypoint

	private float currentTime;
	private int currentWaypoint = 0;
	private Rigidbody2D enemy;


	// Use this for initialization
	void Start () {
		enemy = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWaypoint < waypoint.Length) {
			patrol ();
		} else {
			if (loop) {
				currentWaypoint = 0;
			}
		}
	}

	public void patrol(){
		Vector3 target = waypoint [currentWaypoint].position;

		target.y = transform.position.y;	//Keep waypoint at character's height

		Vector3 moveDirection = target - transform.position;

		if (moveDirection.magnitude < 0.5) {
			if (currentTime == 0) {
				currentTime = Time.time;	//Pause over the waypoint
			}

			if ((Time.time - currentTime)	>= pauseDuration) {
				currentWaypoint++;
				currentTime = 0;
			} else {
				Quaternion rotation = Quaternion.LookRotation(target - transform.position);

				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * dampingLook);

				enemy.velocity = new Vector2 (moveDirection.normalized.x * patrolSpeed * Time.deltaTime, enemy.velocity.y);
			}
		}
	
	}
}
