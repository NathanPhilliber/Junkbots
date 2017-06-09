using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnDestroy : MonoBehaviour {
    public GameObject destroyEffect;
    bool quitting = false;

	public static bool isSceneChange = false;

	void OnDestroy()
    {
		if (!quitting) {
			if (!isSceneChange) {
				GameObject.Instantiate (destroyEffect, transform.position, Quaternion.identity);
			}
		}
    }

	void Start(){
		isSceneChange = false;
	}

    void OnApplicationQuit()
    {
        quitting = true;
    }
}
