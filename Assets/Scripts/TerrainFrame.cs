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

	public float height, width;	//	Dimensions of this frame
	public int enter0, enter1;	//	Enter range
	public int exit0, exit1;	//	Exit range

}


/*
 * All specific frames must implement this interface
 * Basic frame will not implement this interface
 */
public interface IFrame{
	void FillFrame ();	//	Generate the frame
}
