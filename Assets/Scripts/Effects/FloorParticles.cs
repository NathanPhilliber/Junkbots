using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParticles : MonoBehaviour {

	public LayerMask playerLayer;
	public GameObject particle;

	public float xDiff;
	private float lastX = 0;


	void OnTriggerStay2D(Collider2D other){
		if ((other.transform.position.x - lastX > xDiff || lastX - other.transform.position.x > xDiff) && playerLayer == (playerLayer | (1 << other.gameObject.layer))) {
			lastX = other.transform.position.x;
			GameObject spawn = (GameObject)Instantiate (particle, other.transform.position - new Vector3(0,2.5f,0), Quaternion.Euler(new Vector3(0,0,180)));
			Destroy(spawn, 1.5f);
		}
	}
}
