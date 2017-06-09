using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableErl : Damageable {

	public GameObject fadeOutEffect;

    public override void OnHealthDecreased(int amount)
    {
        // Nothing yet
    }

	private bool died = false;
	public override void OnHealthZero()
	{
		if (died == false) {
			died = true;
			gameObject.SetActive (false);
			int bi = SceneManager.GetActiveScene ().buildIndex;
			////SceneManager.LoadSceneAsync(bi);
			EffectOnDestroy.isSceneChange = true;
			Camera.main.GetComponent<LoadScene>().LoadDelayed(bi, 150);
			GameObject fadeout = (GameObject)Instantiate (fadeOutEffect, Camera.main.transform.position - new Vector3 (0, 25, -10), Quaternion.identity);
			fadeout.transform.parent = Camera.main.transform;
		}
	}
		
}
