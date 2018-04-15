using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanState : State {

	public float lookLeftAndRightSpeed;
	bool finishedLookingRight;
	Quaternion orignalRotation;
	Quaternion lookRightRotation;
	StateMachine statemachine;
	AlertState alertState;


	public override void Enter()
	{
		finishedLookingRight = false;
	   orignalRotation = this.transform.rotation;
		 lookRightRotation = new Quaternion();
		statemachine = this.GetComponent<StateMachine> ();
		alertState = this.GetComponent<AlertState> ();
	}

public override void Execute ()
	{

			if (Quaternion.Angle (transform.rotation, orignalRotation) < 45.0f && !finishedLookingRight) {

				transform.Rotate (Vector3.up * Time.deltaTime * lookLeftAndRightSpeed);
			} else if(!finishedLookingRight) {
				finishedLookingRight = true;
				lookRightRotation = this.transform.rotation;
			}

			if (finishedLookingRight) {
				if (Quaternion.Angle (transform.rotation, lookRightRotation) < 90.0f)
					transform.Rotate (Vector3.up * Time.deltaTime * -lookLeftAndRightSpeed);
				else {
				OnFinishScan ();
				}

			}

		if (this.GetComponent<FieldOfView> ().canSeeTarget)
			statemachine.ChangeState (alertState);		
   }

 public	virtual void OnFinishScan()
	{
		statemachine.ChangeToPreviousState ();
	}
}
