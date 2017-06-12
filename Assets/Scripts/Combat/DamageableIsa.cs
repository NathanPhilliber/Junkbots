using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableIsa : Damageable {

    public SpriteRenderer LEyeSprite;
    public SpriteRenderer REyeSprite;

	public GameObject fadeOutEffect;
    Shield shield;

	private SoundManager sounds;

    void Start()
    {
        shield = GetComponentInChildren<Shield>();
		sounds = Camera.main.GetComponent<SoundManager> ();
    }

    public override void OnHealthDecreased(int amount)
    {
		sounds.PlaySound (14);

        if (health < maxHealth / 2)
        {
            LEyeSprite.color = Color.yellow;
            REyeSprite.color = Color.yellow;
        }
        if (health < maxHealth / 4)
        {
            LEyeSprite.color = Color.red;
            REyeSprite.color = Color.red;
        }
        if (shield != null)
        {
            shield.Disable(gameObject);
        }
    }


	private bool died = false;
    public override void OnHealthZero()
    {
		if (died == false) {
			died = true;
			int bi = SceneManager.GetActiveScene ().buildIndex;
			gameObject.SetActive (false);
			EffectOnDestroy.isSceneChange = true;
			Camera.main.GetComponent<LoadScene>().LoadDelayed(bi, 150);
			GameObject fadeout = (GameObject)Instantiate (fadeOutEffect, Camera.main.transform.position - new Vector3 (0, 25, -10), Quaternion.identity);
			fadeout.transform.parent = Camera.main.transform;
		}
    }
		
}
