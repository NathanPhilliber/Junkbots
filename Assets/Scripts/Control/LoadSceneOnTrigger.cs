using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour {

	public LayerMask mask;
	public string level;



	void OnTriggerEnter2D(Collider2D other){

		if (mask == (mask | (1 << other.gameObject.layer))) {
			SceneManager.LoadSceneAsync(level);
		}

	}

}
