using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	PatrolState patrolState;
	public float speed;
	public float coolDownTime;
	float cooldownTimer;
	StateMachine fsm;
	public GameObject bulletType;
	bool canFire;

	// Use this for initialization
	void Start () {
		
		patrolState = this.GetComponent<PatrolState> ();
		fsm =  this.GetComponent<StateMachine>();
		fsm.ChangeState (patrolState);
		canFire = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		

		if (cooldownTimer > coolDownTime) {
			canFire = true;
		}
		else
			cooldownTimer += Time.deltaTime;
			

		fsm.ExecuteCurrentState ();

	}

	public void Fire(Vector3 direction, float speed)
	{
		bulletType.GetComponent<bulletScript>().speed = speed;
		bulletType.GetComponent<bulletScript>().direction = direction;

		Instantiate(bulletType, transform.position, transform.rotation);
		cooldownTimer = 0.0f;
		canFire = false;
	}

	public 	bool CanFire()
	{
		return canFire;
	}


}
