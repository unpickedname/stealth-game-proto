using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : State {
	public GameObject target;
	InvestigateState investigationState;
	StateMachine stateMachine;

	public override void  Enter()
	{
		Debug.Log ("Entering alert state");
		investigationState = this.GetComponent<InvestigateState> ();
		stateMachine = this.GetComponent<StateMachine> ();
	}

	public override void Execute()
	{
		if (this.GetComponent<FieldOfView> ().canSeeTarget)
			transform.LookAt (target.transform);
		else {
			this.GetComponent<InvestigateState> ().investigationSubject = this.GetComponent<FieldOfView> ().GetLastSeenTargetPosition ();
			stateMachine.ChangeState (investigationState);
		}

		if (this.GetComponent<BasicEnemy> ().CanFire ()) {
			this.GetComponent<BasicEnemy> ().Fire (target.transform.position - transform.position, 10);
		}
	}
}
