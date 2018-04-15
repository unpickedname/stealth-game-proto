using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPatrolState : InvestigateState {



	public override void OnFinishPath ()
	{
		StateMachine stateMachine = this.GetComponent<StateMachine> ();
		PatrolState patrolState = this.GetComponent<PatrolState> ();

		stateMachine.ChangeState (patrolState);
	}


}
