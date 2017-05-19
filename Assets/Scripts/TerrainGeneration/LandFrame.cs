using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandFrame : MonoBehaviour, IFrame
{

	public GameObject landTile;
	//	The basic platform object to be spawned
	public int minHeight, maxHeight;
	//	The min/max hill height
	public int minSteepness, maxSteepness;
	//	How much elevation can change by
	public int slopeFactor;
	//	The probability of elevation change (int 0-100)
	public int steepFactor;
	//	The probability of how much steepness change (int 0-100)
	public int landThickness;
	//	How thick the land should be

	private TerrainFrame frame;
	//	Contains basic information about this frame

	/*
	 * Generate the platforms in this frame
	 */
	public void FillFrame ()
	{
		frame = GetComponent<TerrainFrame> ();								//	Initialize the frame object
		float curX = transform.position.x;									//	Initialize x counter, used to know when we're done with this frame
		float tileWidth = landTile.GetComponent<BoxCollider2D> ().size.x;	//	The width of each tile
		float tileHeight = landTile.GetComponent<BoxCollider2D> ().size.y;	//	The height of each tile
		int targetHillHeight = (int)(frame.enterY * tileHeight);			//	The height of the current hill to be spawned	//	TODO: Change to enter range
		int curStackHeight = targetHillHeight;								//	The height of the current column
		bool goingUp = true;

		while (curX < transform.position.x + frame.width) {			//	Repeat for every x in frame
			SpawnColumn (curX, curStackHeight);						//	Spawn a column at x and of current height
			frame.exitY = transform.position.y + curStackHeight * tileHeight;

			curStackHeight = GetRandomColumnHeight (curStackHeight, (int)(targetHillHeight - curStackHeight));	//	Find next column height

			if (goingUp && curStackHeight >= targetHillHeight) {						//	Check if we reached the target
				targetHillHeight = Random.Range (minHeight, maxHeight);
				if (curStackHeight < targetHillHeight) {
					goingUp = false;
				}
			} else if (!goingUp && curStackHeight <= targetHillHeight) {
				targetHillHeight = Random.Range (minHeight, maxHeight);
				if (curStackHeight > targetHillHeight) {
					goingUp = true;
				}
			}

			curX += tileWidth;
		}


	}


	/*
	 * Get a new random column height.
	 * 		int curHeight: The height of the last column
	 * 		int direction: Positive/zero if up, negative if down
	 * 
	 */
	private int GetRandomColumnHeight (int curHeight, int direction)
	{
		if (Random.Range (0, 100) <= slopeFactor) {		//	Do an elevation change

			//Find out how much to change elevation by
			int rangeDelta = maxSteepness - minSteepness;
			float scaleFactor = 100f / rangeDelta;

			int delta = Random.Range (0, 100);

			if (direction >= 0) {
				return curHeight + (int)(delta / scaleFactor + minSteepness);
			} else {
				return curHeight - (int)(delta / scaleFactor + minSteepness);
			}

		} else { //	No elevation change
			return curHeight;
		}
	}


	/*
	 * Spawn a column of tiles at x position
	 */
	private void SpawnColumn (float x, int stackHeight)
	{
		float tileHeight = landTile.GetComponent<BoxCollider2D> ().size.y;

		for (int i = stackHeight - landThickness > 0 ? stackHeight - landThickness : 0; i < stackHeight; i++) {
			GameObject spawned = (GameObject)Instantiate (landTile, new Vector2 (x, i * tileHeight + transform.position.y), Quaternion.identity);
			spawned.transform.parent = transform;
		}
	}
}
