using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTimer : MonoBehaviour {

	public float timer;
	public float oldSpeed;
	public string whose;

	private GameObject player;

	void Start () {
		player = GameObject.Find (whose);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			
			Destroy (gameObject, 0.0F);
		}
	}
}
