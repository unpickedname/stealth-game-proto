       using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : State {
	//this class adapts code from unit.cs, written with instruction from Sebastian Lague YouTube tutorial for A* algorithm 
	public Vector3 investigationSubject;
	Vector3[] path;
	int targetIndex;
	float speed;
	AlertState alertState;
	ReturnToPatrolState returnToPatrolState; 
	InvestagateScanState investigateScanState;
	PatrolState patrolState;
	StateMachine stateMachine;
	IEnumerator FollowPathCoroutine;

	public override void Enter()
	{
		PathRequestManager.RequestPath(transform.position, investigationSubject, OnPathFound);
		speed = this.GetComponent<BasicEnemy> ().speed;
		alertState = this.GetComponent<AlertState> ();
		returnToPatrolState = this.GetComponent<ReturnToPatrolState> ();
		patrolState = this.GetComponent<PatrolState> ();
		investigateScanState = this.GetComponent<InvestagateScanState> ();
		stateMachine = this.GetComponent<StateMachine> ();
		FollowPathCoroutine = FollowPath ();

	}
	
	// Update is called once per frame
	public override void Execute()
	{
		if (this.GetComponent<FieldOfView> ().canSeeTarget) {
			stateMachine.ChangeState (alertState);
		}
		
	}

	public override void Exit()
	{
		StopCoroutine (FollowPathCoroutine);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if(pathSuccessful)
		{
			path = newPath;
			StopCoroutine(FollowPathCoroutine);
			StartCoroutine(FollowPathCoroutine);
		}
	}

	public virtual void OnFinishPath()
	{
		returnToPatrolState.investigationSubject = patrolState.currentWaypoint;
		stateMachine.ChangeState (investigateScanState);

	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];

		while(true)
		{
			if(transform.position == currentWaypoint)
			{
				targetIndex++;
				if(targetIndex >= path.Length)
				{
					OnFinishPath ();
					yield break;

				}

				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed *Time.deltaTime);
			transform.LookAt (currentWaypoint);
			yield return null;
		}
	}
}
