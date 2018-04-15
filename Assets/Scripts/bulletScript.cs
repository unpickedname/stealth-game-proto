using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {
	public float lifeTime;
	public Vector3 direction;
	public float speed;
	float lifeTimer;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		lifeTimer = 0.0f;
		rb = this.GetComponent<Rigidbody> ();
		rb.velocity = direction * speed;
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeTimer += Time.deltaTime;

		if (lifeTimer > lifeTime)
			Destroy (this.gameObject);
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerHealth> ().AlterHealth (-1);
		}

		if (col.gameObject.tag == "Player" || col.gameObject.layer == 8)
			Destroy (this.gameObject);
	}
}
