using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParticleAtDistance : MonoBehaviour {

	public Transform camera;
	public float disableRadius;

	private ParticleSystem particles;
	public bool disabled = false;

	void Start(){
		particles = GetComponent<ParticleSystem> ();
		if (camera == null) {
			camera = Camera.main.transform;
		}
	}

	void Update () {
		if (!disabled && Mathf.Abs(Vector3.Magnitude (camera.position - transform.position)) > disableRadius) {
			disabled = true;
			particles.Stop ();
		}
		else if(disabled && Mathf.Abs(Vector3.Magnitude (camera.position - transform.position)) < disableRadius){
			disabled = false;
			particles.Play ();
		}
	}


}
