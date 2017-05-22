using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {



	public GameObject staircase;			//	Staircase prefab
	public GameObject staircaseDown;		//	Down Staircase prefab
	public GameObject platform;				//	Platform prefab

	private LandFrame frame;				//	The frame in which we are spawning structures on

	private float stairOffsetX = 8.9f;		//	Distance from middle to edge on x
	private float stairOffsetY = 5.4f;		//	Distance from middle to edge on y

	private float platformOffsetX = 10.2f;	//Distance from middle to edge on x


	/*
	 * Generate a structure starting at a provided point
	 */
	public void GenerateStructure(LandFrame frame, int vertex){
		this.frame = frame;
		Mesh mesh = frame.GetComponent<MeshFilter> ().mesh;
		Vector3[] vertices = mesh.vertices;

		Vector3 anchor = frame.transform.TransformPoint (vertices [vertex]);
		Vector2 point = new Vector2 (anchor.x, anchor.y);

		point = SpawnStaircase (point);

		bool keepSpawning = true;
		int numPieces = 0;

		while (keepSpawning) {
			numPieces++;
			int id = Random.Range (0, 3);
				
			if (id == 0) { //staircase up
				point = SpawnStaircase(point);
			} else if (id == 1) { //platform
				point = SpawnPlatform(point);
			} else if (id == 2) { //staircase down
				point = SpawnStaircaseDown(point);
			}

			vertex = GetNextVertex (frame, vertex, point.x);

			if (frame.transform.TransformPoint (vertices [vertex]).y > point.y || numPieces > 2) {
				keepSpawning = false;
			}
		}

	}

	/*
	 * Get the next vertex that is past provided x.
	 * ToDo: Support cross-frame travel
	 */
	public int GetNextVertex(LandFrame frame, int vertex, float x){
		Mesh mesh = frame.GetComponent<MeshFilter> ().mesh;
		Vector3[] vertices = mesh.vertices;
		while (frame.transform.TransformPoint(vertices [vertex]).x < x) {
			vertex++;
		}
		return vertex;
	}

	/*
	 * Spawn a staircase starting from point, returns point at end of staircase
	 */
	public Vector2 SpawnStaircase(Vector2 anchor){
		GameObject spawned = (GameObject)Instantiate (staircase, new Vector2(anchor.x + stairOffsetX, anchor.y + stairOffsetY), Quaternion.identity);
		spawned.transform.parent = transform;

		return new Vector2 (spawned.transform.position.x + stairOffsetX, spawned.transform.position.y + stairOffsetY);
	}

	/*
	 * Spawn a platform starting from point, returns point at end of platform
	 */
	public Vector2 SpawnPlatform(Vector2 anchor){
		GameObject spawned = (GameObject)Instantiate (platform, new Vector2(anchor.x + platformOffsetX, anchor.y), Quaternion.identity);
		spawned.transform.parent = transform;

		return new Vector2 (spawned.transform.position.x + platformOffsetX, spawned.transform.position.y);
	}

	/*
	 * Spawn a staircase starting from point, returns point at end of staircase
	 */
	public Vector2 SpawnStaircaseDown(Vector2 anchor){
		GameObject spawned = (GameObject)Instantiate (staircaseDown, new Vector2(anchor.x + stairOffsetX, anchor.y - stairOffsetY), Quaternion.identity);
		spawned.transform.parent = transform;

		return new Vector2 (spawned.transform.position.x + stairOffsetX, spawned.transform.position.y - stairOffsetY);
	}

}
