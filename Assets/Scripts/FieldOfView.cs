using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FieldOfView : MonoBehaviour {

    public GameObject targetToSee;
    public float viewingAngle;
    public float viewingDistance;
    public LayerMask obstacleMask;
	public bool canSeeTarget;
	Vector3 lastSeentTargetPosition;


    // Use this for initialization
    void Start () {
		canSeeTarget = false;
		lastSeentTargetPosition = new Vector3 ();
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 vectorToTarget = findVectorToTarget();
        Vector2 xyforward = new Vector2(this.transform.forward.x, this.transform.forward.z);

       float angle =  Vector2.Angle(xyforward, vectorToTarget);
		if (angle <= viewingAngle && vectorToTarget.magnitude <= viewingDistance &&  (!Physics.Raycast (this.transform.position, targetToSee.transform.position - this.transform.position, vectorToTarget.magnitude, obstacleMask))){
			
				Debug.Log ("I  see you");
				Debug.DrawRay (this.transform.position, targetToSee.transform.position - this.transform.position);
				canSeeTarget = true;
			lastSeentTargetPosition = targetToSee.transform.position;
			} 
			else {
			Debug.Log ("Are you still there?");
			canSeeTarget = false;
		}
		
	}

    Vector2 findVectorToTarget()
    {
        //converts 3d position to 2d
        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 targetPosition = new Vector2(targetToSee.transform.position.x, targetToSee.transform.position.z);

        return targetPosition - position;



    }

	public 	Vector3 GetLastSeenTargetPosition()
	{
		return lastSeentTargetPosition;
	}
}
