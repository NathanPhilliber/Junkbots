using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandFrame : MonoBehaviour, IFrame {

	public GameObject landTile;	//	The basic platform object to be spawned

	private TerrainFrame frame;	//	Contains basic information about this frame

	void Start(){
		FillFrame ();
	}

	/*
	 * Generate the platforms in this frame
	 */
	public void FillFrame(){
		frame = GetComponent<TerrainFrame> ();						//	Initialize the frame object
		float curX = transform.position.x;
		float curY = transform.position.y;
		float tileWidth = landTile.GetComponent<BoxCollider2D> ().size.x;

		while (curX < transform.position.x + frame.width) {
			GameObject spawned = (GameObject)Instantiate (landTile, new Vector2 (curX, curY), Quaternion.identity);
			spawned.transform.parent = transform;
			curX += tileWidth;
		}
	}
}
