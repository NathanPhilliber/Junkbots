using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
	
	public float levelWidth;					//	How long a level should be (not exact, level will be slightly longer)	

	public GameObject[] frames;					//	Prefabs for different frame types

	public GameObject structure;	

	private List<GameObject> existingFrames = new List<GameObject>();	//	Store all of the spawned frames

	private int ravineChancePercent = 30;

	void Start () {
		
		float xCur = transform.position.x;		//	Start x counter at the beginning of this frame
		float lastY = 0;						//	Declare a y tracker, this will set exit points to enter points
		GameObject lastLandFrame = null;

		while (xCur < levelWidth + transform.position.x) {															//	Keep going until we are past level width
			GameObject frame = (GameObject)Instantiate (frames [0], new Vector2 (xCur, 0), Quaternion.identity);	//	Spawn a frame
			frame.GetComponent<TerrainFrame> ().enterY = lastY;														//	Set the enter point to the last exit point

			existingFrames.Add (frame);

			if (frame.GetComponent<LandFrame> () != null) {			//	If this is a land frame

				if (lastLandFrame != null) {
					lastLandFrame.GetComponent<LandFrame> ().nextFrame = frame;
				}
				lastLandFrame = frame;

				float yMax, yMin;
				if (Random.Range (0, 2) == 0) {						//	50% chance of flat
					yMax = Random.Range (0f, .2f);	
					yMin = Random.Range (-.2f, 0f);	

					if(Random.Range(0,100) < ravineChancePercent){
						frame.GetComponent<LandFrame> ().generateRavine = true;
						frame.GetComponent<LandFrame> ().spawnBridge = true;
					}
				}
				else if (Random.Range (0, 10) == 0) {						//	50% chance of flat
					yMax = Random.Range (-1f, -.5f);	
					yMin = Random.Range (-3f, -1f);	
				}else {											//	50% chance of getting a chance to be more extreme
					yMax = Random.Range (0f, 4f);
					yMin = Random.Range (-4f, 0f);
				}
				frame.GetComponent<LandFrame> ().maxYDifference = yMax;
				frame.GetComponent<LandFrame> ().minYDifference = yMin;
			}
			frame.GetComponent<IFrame> ().FillFrame ();																//	Generate frame
			frame.transform.parent = transform;																		//	Child the frame to this object

			xCur += frame.GetComponent<TerrainFrame>().width;														//	Add frame width to x counter
			lastY = frame.GetComponent<TerrainFrame> ().exitY;														//	Save the exit point from the new frame
		}

		int numStructures = Random.Range (1, 5);
		for (int i = 0; i < numStructures; i++) {
			GameObject frame = existingFrames [Random.Range (0, existingFrames.Count - 2)];
			if (frame.GetComponent<LandFrame> () != null) {
				if (frame.GetComponent<LandFrame> ().maxYDifference < 2 && frame.GetComponent<LandFrame> ().minYDifference > -2) {
					if (frame.GetComponent<LandFrame> ().generateRavine == false) {
						GameObject spawnedStructure = (GameObject)Instantiate (structure, Vector3.zero, Quaternion.identity);
						spawnedStructure.GetComponent<Structure> ().GenerateStructure (frame.GetComponent<LandFrame> (), Random.Range (2, 12));
					}
				}
			}

		}

	}
}



// ToDo: generate all land first, link up frames. Then go and spawn structures in.