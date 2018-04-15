using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int health;
	public Text healthUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

		healthUI.text = "Health:" + health;
		
	}

	public void AlterHealth(int changeValue)
	{
		health += changeValue;
	}
}
