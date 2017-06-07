using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public bool playSoundEffects = true;
	public bool playMusic = true;

	public AudioClip[] soundEffects;

	private AudioSource src;
	private AudioSource src2;

	void Start(){
		src = GetComponent<AudioSource> ();
		src2 = gameObject.AddComponent<AudioSource> ();
	}

	public void PlaySound(int clipNum){
		if (playSoundEffects) {
			src.PlayOneShot (soundEffects [clipNum]);
		}
	}

	public void PlaySound(int clipNum, float delay){
		if (playSoundEffects) {
			src2.clip = soundEffects [clipNum];
			src2.PlayDelayed (delay);
		}
	}

	public void PlaySoundDelay1(int clipNum){
		if (playSoundEffects) {
			src2.clip = soundEffects [clipNum];
			src2.PlayDelayed (.55f);
		}
	}
}
