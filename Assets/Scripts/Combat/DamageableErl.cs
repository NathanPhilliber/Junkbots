using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableErl : Damageable {
    public override void OnHealthDecreased(int amount)
    {
        // Nothing yet
    }

    public override void OnHealthZero()
    {
        int bi = SceneManager.GetActiveScene().buildIndex;    
        SceneManager.LoadSceneAsync(bi);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
