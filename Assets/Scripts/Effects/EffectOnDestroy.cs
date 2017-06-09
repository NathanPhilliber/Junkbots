using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnDestroy : MonoBehaviour {
    public GameObject destroyEffect;
    bool quitting = false;

	void OnDestroy()
    {
        if (!quitting)
        GameObject.Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }

    void OnApplicationQuit()
    {
        quitting = true;
    }
}
