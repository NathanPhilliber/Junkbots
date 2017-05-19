using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class responsible for generating terrain/levels.
 * 
 */
public class TerrainGenerator : MonoBehaviour {

	public float levelWidth; //How long the level should be

	public GameObject[] frames;


	/*
	 * Call the generate function at start
	 */
	void Start(){
		GenerateLevel ();
	}


	/*
	 * Call once to generate an entire level
	 */
	public void GenerateLevel(){
		float curWidth = 0;
		TerrainFrame lastFrame = null;

		while (curWidth < levelWidth) {		//	Spawn a new frame until we reach level width
			GameObject spawned = (GameObject)Instantiate (GetRandomFrame (), new Vector3 (transform.position.x + curWidth, transform.position.y, transform.position.z), Quaternion.identity);
			if (spawned.GetComponent<PlatformFrame> () != null) {
				spawned.GetComponent<PlatformFrame> ().spawnPattern = 1;
			}
			else if (spawned.GetComponent<LandFrame> () != null) {
				spawned.GetComponent<LandFrame> ().slopeFactor = Random.Range(0,75);
				spawned.GetComponent<LandFrame> ().maxSteepness = Random.Range(1,5);

				spawned.GetComponent<LandFrame> ().landThickness = Random.Range(12,20);
			}
			if (lastFrame != null) {
				spawned.GetComponent<TerrainFrame> ().enterY = lastFrame.exitY;
			}
			lastFrame = spawned.GetComponent<TerrainFrame> ();
			spawned.GetComponent<IFrame> ().FillFrame ();
			curWidth += spawned.GetComponent<TerrainFrame> ().width;

		}
	}


	/*
	 * Get a random frame prefab
	 */
	public GameObject GetRandomFrame(){
		return frames [Random.Range (0, frames.Length)];
	}
}
