using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

	State currentState;
	State PreviousState;



	public void ChangeState(State newState)
	{
		PreviousState = currentState;
		currentState = newState;

		if(PreviousState != null)
			PreviousState.Exit ();
		if(currentState != null)
			currentState.Enter ();
	}

	public void ExecuteCurrentState()
	{
		if(currentState != null)
			currentState.Execute ();

	}

	public void ChangeToPreviousState()
	{
		ChangeState (PreviousState);
	}
}
