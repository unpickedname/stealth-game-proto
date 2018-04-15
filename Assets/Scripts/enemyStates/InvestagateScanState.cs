using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestagateScanState : ScanState {


	public override void OnFinishScan ()
	{
		StateMachine stateMachine = this.GetComponent<StateMachine> ();
		ReturnToPatrolState returnToPatrolState = this.GetComponent<ReturnToPatrolState> ();

		stateMachine.ChangeState (returnToPatrolState);
	}

}
