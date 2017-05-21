using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/*
 * Basic information for one frame used in terrain generation
 * 
 */
public class TerrainFrame : MonoBehaviour {

	public float width;			//	Dimension of this frame
	public float enterY;		//	Enter spot
	public float exitY;			//	Exit spot

}


/*
 * All specific frames must implement this interface
 * Basic frame will not implement this interface
 */
public interface IFrame{
	void FillFrame ();	//	Generate the frame
}
