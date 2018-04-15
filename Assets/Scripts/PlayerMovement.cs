using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed;
	public Camera camera;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.W))
        {
            rb.velocity = (Vector3.forward * movementSpeed);
			transform.rotation = Quaternion.LookRotation (Vector3.forward);
        }

        else if(Input.GetKey(KeyCode.S))
        {
            rb.velocity = (Vector3.forward * -movementSpeed);
			transform.rotation = Quaternion.LookRotation (Vector3.back);
        }

       if(Input.GetKeyUp(KeyCode.W)  || Input.GetKeyUp(KeyCode.S))
        {
            float oldX = rb.velocity.x;
            float oldY = rb.velocity.y;
            rb.velocity = new Vector3(oldX, oldY, 0.0f);
        }


        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = (Vector3.right * movementSpeed);
			transform.rotation = Quaternion.LookRotation (Vector3.right);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = (Vector3.right * -movementSpeed);
			transform.rotation = Quaternion.LookRotation (Vector3.left);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            float oldy = rb.velocity.y;
            float oldZ = rb.velocity.z;
            rb.velocity = new Vector3(0.0f, oldy, oldZ);
        }

		camera.transform.position = new Vector3 (this.transform.position.x, camera.transform.position.y, this.transform.position.z);
			



    }
}
