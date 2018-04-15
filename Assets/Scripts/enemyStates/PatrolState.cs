using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {
	StateMachine stateMachine;
	AlertState alertState;
	ScanState scanState;
	public GameObject[] waypoints;
	public float lookLeftAndRightSpeed;
	public  Vector3 currentWaypoint;
	float speed;
	IEnumerator patrolCoroutine;
	IEnumerator lookLeftAndRightCoroutine;
	bool canMove;
	bool PatrolRunning = false;


	public override void Enter()
	{
		Debug.Log ("entering patrol state");
		stateMachine = this.GetComponent<StateMachine> ();
		alertState = this.GetComponent<AlertState> ();
		scanState = this.GetComponent<ScanState> ();
		patrolCoroutine = Patrol ();
		if(!PatrolRunning)
		  StartCoroutine (patrolCoroutine);
		speed = this.GetComponent<BasicEnemy> ().speed;
		canMove = true;

	}
	
	public override void Execute()
	{
		if (this.GetComponent<FieldOfView> ().canSeeTarget)
			stateMachine.ChangeState (alertState);
	}

	public override void Exit()
	{
		
		canMove = false;
	}



	IEnumerator Patrol()
	{
		currentWaypoint = waypoints[0].transform.position;
		int pathIndex = 0;
		PatrolRunning = true;

		while(true)
		{
			if(transform.position == currentWaypoint  && canMove)
			{
				stateMachine.ChangeState(scanState);

				pathIndex++;

				if (pathIndex >= waypoints.Length)
					pathIndex = 0;

				currentWaypoint = waypoints[pathIndex].transform.position;
			}



			if (canMove) {
				Vector3 waypointDirection = currentWaypoint - this.transform.position;
				transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				transform.LookAt (waypoints [pathIndex].transform);
			}

			yield return null;
		}
	}


}
