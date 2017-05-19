using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFrame : MonoBehaviour, IFrame {

	public GameObject platform;				//	The basic platform object to be spawned
	public int minPlatforms, maxPlatforms;	//	The min/max number of platforms that can spawn
	public float minXDistance, minYDistance;	//	The minimum distance for platforms to be from each other
	public float maxXDistance, maxYDistance;	//	The minimum distance for platforms to be from each other

	public int spawnPattern;	//	The pattern in which the platforms will spawn in
								//		0 = complete random
								//		1 = 1 per column

	private TerrainFrame frame;	//	Contains basic information about this frame
	private int numPlatforms;	//	The number of platforms to be spawned. Determined by random range between min/max


	/*
	 * Generate the platforms in this frame
	 */
	public void FillFrame(){

		frame = GetComponent<TerrainFrame> ();						//	Initialize the frame object
		numPlatforms = Random.Range (minPlatforms, maxPlatforms);	//	Determine the number of platforms to be spawned
		ArrayList platforms = new ArrayList();

		if (spawnPattern == 0) { 			//		0 = complete random		//////
			for (int i = 0; i < numPlatforms; i++) {
				GameObject spawned = (GameObject)Instantiate (platform, new Vector2 (Random.Range (0, frame.width) + transform.position.x, Random.Range (0, frame.height) + transform.position.y), Quaternion.identity);
				spawned.transform.parent = gameObject.transform;
				platforms.Add (spawned);
			}
		} 
		else if (spawnPattern == 1) {		//		1 = 1 per column	//////
			float curX = transform.position.x;
			float platformWidth = platform.GetComponent<BoxCollider2D> ().size.x;
			float lastY = frame.enterY;												// TODO: Change to enter range

			while (curX < transform.position.x + frame.width) {				//	Keep placing platforms until we reach end of frame
				float deltaX = Random.Range (minXDistance, maxXDistance);	//	X Change
				float deltaY = Random.Range (minYDistance, maxYDistance);	//	Y Change

				if (Random.Range (0, 2) == 0) {				// 50% chance of next platform being below last one
					deltaY *= -1;
					if (deltaY < transform.position.y) {	//Check if we are below frame
						deltaY = transform.position.y;
					}
				}

				if (deltaY + lastY > transform.position.y + frame.height) {		//	Check if we are above frame
					deltaY = 0;
				}				

				GameObject spawned = (GameObject)Instantiate (platform, new Vector2 (deltaX + curX + platformWidth, deltaY + lastY), Quaternion.identity);
				spawned.transform.parent = gameObject.transform;	//	Make this platform child of frame
				platforms.Add (spawned);
				curX += platformWidth + deltaX;						//	Advance X counter
				lastY = spawned.transform.position.y;				//	Keep track of this Y height

			}
		}

		//Get rightmost platform
		float maxX = ((GameObject)platforms[0]).transform.position.x;
		float y = 0;
		for (int i = 0; i < platforms.Count; i++) {
			float x = ((GameObject)platforms [i]).transform.position.x;
			if (x > maxX) {
				maxX = x;
				y = ((GameObject)platforms [i]).transform.position.y;
			}
		}

		frame.exitY = y;
	}
}
