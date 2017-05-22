using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
	
	public float levelWidth;					//	How long a level should be (not exact, level will be slightly longer)	

	public GameObject[] frames;					//	Prefabs for different frame types

	public GameObject structure;

	void Start () {
		
		float xCur = transform.position.x;		//	Start x counter at the beginning of this frame
		float lastY = 0;						//	Declare a y tracker, this will set exit points to enter points

		while (xCur < levelWidth + transform.position.x) {															//	Keep going until we are past level width
			GameObject frame = (GameObject)Instantiate (frames [0], new Vector2 (xCur, 0), Quaternion.identity);	//	Spawn a frame
			frame.GetComponent<TerrainFrame> ().enterY = lastY;														//	Set the enter point to the last exit point
			if (frame.GetComponent<LandFrame> () != null) {			//	If this is a land frame
				float yMax, yMin;
				if (Random.Range (0, 2) == 0) {						//	50% chance of flat
					yMax = Random.Range (0f, .2f);
					yMin = Random.Range (-.2f, 0f);
				} else {											//	50% chance of getting a chance to be more extreme
					yMax = Random.Range (0f, 4f);
					yMin = Random.Range (-4f, 0f);
				}
				frame.GetComponent<LandFrame> ().maxYDifference = yMax;
				frame.GetComponent<LandFrame> ().minYDifference = yMin;
			}
			frame.GetComponent<IFrame> ().FillFrame ();																//	Generate frame
			frame.transform.parent = transform;																		//	Child the frame to this object

			if (Random.Range (0, 10) == 0) {
				GameObject spawnedStructure = (GameObject)Instantiate (structure, Vector3.zero, Quaternion.identity);
				spawnedStructure.GetComponent<Structure> ().GenerateStructure (frame.GetComponent<LandFrame>(), 5);
			}

			xCur += frame.GetComponent<TerrainFrame>().width;														//	Add frame width to x counter
			lastY = frame.GetComponent<TerrainFrame> ().exitY;														//	Save the exit point from the new frame
		}
	}
	

}



// ToDo: generate all land first, link up frames. Then go and spawn structures in.