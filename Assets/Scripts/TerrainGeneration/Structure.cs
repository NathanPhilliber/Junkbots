using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {



	public GameObject staircase;
	public GameObject platform;

	private LandFrame frame;

	private float stairOffsetX = 8f;
	private float stairOffsetY = 5.8f;

	private float platformOffsetX = 19f;
	private float platformOffsetY = 5.4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateStructure(LandFrame frame, int vertex){
		this.frame = frame;
		Vector3[] vertices = frame.GetComponent<MeshFilter> ().mesh.vertices;

		Vector3 anchor = frame.transform.TransformPoint (vertices [vertex]);
		Vector2 newPos = new Vector2 (anchor.x + stairOffsetX, anchor.y + stairOffsetY);

		GameObject spawned = (GameObject)Instantiate (staircase, newPos, Quaternion.identity);
		spawned.transform.parent = transform;



		newPos = new Vector2 (spawned.transform.position.x + platformOffsetX, spawned.transform.position.y + platformOffsetY);
		spawned = (GameObject)Instantiate (platform, newPos, Quaternion.identity);
		spawned.transform.parent = transform;
	}
}
