using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	public float levelWidth;

	public GameObject[] frames;
	// Use this for initialization
	void Start () {
		float xCur = transform.position.x;
		float lastY = 0;

		while (xCur < levelWidth + transform.position.x) {
			GameObject frame = (GameObject)Instantiate (frames [0], new Vector2 (xCur, 0), Quaternion.identity);
			frame.GetComponent<TerrainFrame> ().enterY = lastY;
			frame.GetComponent<IFrame> ().FillFrame ();
			xCur += frame.GetComponent<TerrainFrame>().width;
			lastY = frame.GetComponent<TerrainFrame> ().exitY;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
