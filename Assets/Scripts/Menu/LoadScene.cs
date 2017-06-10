using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public int sceneToLoad;

	public void Load(){
		EffectOnDestroy.isSceneChange = true;
		SceneManager.LoadScene (sceneToLoad, LoadSceneMode.Single);
	}

	public void Load(int scene){
		EffectOnDestroy.isSceneChange = true;
		SceneManager.LoadScene (scene, LoadSceneMode.Single);
	}

	public void LoadDelay50(int scene){
		this.delay = 50;
		delayedScene = scene;
	}

	public void LoadDelayed(int scene, int delay){
		if (this.delay == 0) {
			this.delay = delay;
			delayedScene = scene;
		}
	}

	private int delay;
	private int delayedScene;

	public void Update(){
		if (delay > 0) {
			delay--;

			if (delay <= 0) {
				EffectOnDestroy.isSceneChange = true;
				SceneManager.LoadScene (delayedScene, LoadSceneMode.Single);
			}
		}
	}

	public void QuitApp(){
		Application.Quit ();
	}
}
