using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {

	public GameObject staircase;			//	Staircase prefab
	public GameObject staircaseDown;		//	Down Staircase prefab
	public GameObject platform;				//	Platform prefab

	public Material material;				//	Material for mesh under the platforms and staircases

	//private LandFrame frame;				//	The frame in which we are spawning structures on	//	Not currently being used

	private float stairOffsetX = 8.9f;		//	Distance from middle to edge on x
	private float stairOffsetY = 5.4f;		//	Distance from middle to edge on y

	private float platformOffsetX = 10f;	//	Distance from middle to edge on x

	private Mesh mesh;						//	Land mesh
	private MeshFilter meshFilter;			//	Mesh filter
	private MeshRenderer meshRenderer;		//	Renderer

	/*
	 * Generate a structure starting at a provided point
	 */
	public void GenerateStructure(LandFrame frame, int vertex){
		//this.frame = frame;	//	Not currently being used
		Mesh mesh = frame.GetComponent<MeshFilter> ().mesh;
		Vector3[] vertices = mesh.vertices;

		Vector3 anchor = frame.transform.TransformPoint (vertices [vertex]);
		Vector2 point = new Vector2 (anchor.x, anchor.y);

		point = SpawnStaircase (point);

		bool keepSpawning = true;
		int numPieces = 0;

		int lastVertex = 0;

		List<Vector3> backingVertices = new List<Vector3> ();
		backingVertices.Add (anchor);

		while (keepSpawning) {
			numPieces++;
			int id = Random.Range (0, 100);
				
			if (numPieces > 5 && Random.Range(0,3) == 0) {	//	Encourage more down-staircases if the structure is big
				id = 99;
			}

			if ((id == 0 && id < 15) || numPieces == 1) { 	//	staircase up
				point = SpawnStaircase(point);
			} else if (id >= 15 && id < 85) { 				//	platform
				point = SpawnPlatform(point);
			} else if (id >= 85 && id < 100) { 				//	staircase down
				point = SpawnStaircaseDown(point);
			}

			backingVertices.Add (new Vector3 (point.x, point.y, 0));

			vertex = GetNextVertex (frame, vertex, point.x);

			if (vertex == -1) {
				keepSpawning = false;
			} else {
				if (lastVertex > vertex) {
					frame = frame.GetComponent<LandFrame> ().nextFrame.GetComponent<LandFrame> ();
					mesh = frame.GetComponent<MeshFilter> ().mesh;
					vertices = mesh.vertices;
				}
				lastVertex = vertex;

				if (frame.transform.TransformPoint (vertices [vertex]).y > point.y) {
					keepSpawning = false;
				}
			}
		}

		float yB = backingVertices [backingVertices.Count - 1].y;

		if (yB > backingVertices [0].y) {
			yB = backingVertices [0].y;
		}

		backingVertices.Add (new Vector3 (backingVertices [backingVertices.Count - 1].x, yB - 30, 0));
		backingVertices.Insert (0, new Vector3 (backingVertices[0].x, yB - 30, 0));

		int size = backingVertices.Count;
		int backPoint = size - 2;

		for (int i = 3; i < size; i += 2) {
			backPoint -= 2;
			backingVertices.Add (new Vector3(backingVertices [backPoint].x, yB - 30, 0));
		}

		Vector3[] backVertices = backingVertices.ToArray ();

		Vector2[] uvs = new Vector2[backVertices.Length];

		for (int i = 0; i < uvs.Length; i++) {								//	Assign all uvs, just copy vertices
			uvs[i] = new Vector2(backVertices[i].x, backVertices[i].y);
		}

		int[] triangles = new int[(backVertices.Length - 2)* 3];			//	Create triangles

		int refPoint = size - 1;
		int back = refPoint + 1;
		int forward = refPoint - 1;

		for (int i = 0; i < triangles.Length;) {
			triangles [i] = forward - 1;
			triangles [i + 1] = forward--;
			triangles [i + 2] = refPoint;

			i += 3;

			if (i >= triangles.Length) {
				break;
			}

			triangles [i] = forward - 1;
			triangles [i + 1] = forward--;
			triangles [i + 2] = refPoint;

			i += 3;

			if (i >= triangles.Length) {
				break;
			}

			triangles [i] = forward;
			triangles [i + 1] = refPoint++;
			triangles [i + 2] = back++;

			i += 3;
		}

		mesh = new Mesh ();																//	New mesh
		meshFilter = (MeshFilter)gameObject.AddComponent (typeof(MeshFilter));			//	Add mesh filter
		meshRenderer = (MeshRenderer)gameObject.AddComponent (typeof(MeshRenderer));	//	Add mesh renderer

		meshFilter.mesh = mesh;		

		mesh.vertices = backVertices;													//	Assign data to mesh
		mesh.uv = uvs;
		mesh.triangles = triangles;
		mesh.RecalculateNormals ();		

		meshRenderer.sharedMaterial = material;	

	}

	/*
	 * Get the next vertex that is past provided x.
	 * Returns -1 if at end of all land
	 */
	public int GetNextVertex(LandFrame frame, int vertex, float x){
		Mesh mesh = frame.GetComponent<MeshFilter> ().mesh;
		Vector3[] vertices = mesh.vertices;

		float lastX = frame.transform.TransformPoint(vertices [vertex]).x;							//	Used to detect if we need to go into next frame

		while (frame.transform.TransformPoint(vertices [vertex]).x < x) {

			if (lastX > frame.transform.TransformPoint (vertices [vertex]).x) {						//	Check if we need to go into the next frame

				if (frame.GetComponent<LandFrame> ().nextFrame == null) {							//	If we are at the end return -1
					return -1;
				}

				frame = frame.GetComponent<LandFrame> ().nextFrame.GetComponent<LandFrame> ();		//	Get the next frame

				mesh = frame.GetComponent<MeshFilter> ().mesh;
				vertices = mesh.vertices;
				vertex = 1;
			} else {																				//	If we are in the same frame, then return next vertex
				lastX = frame.transform.TransformPoint(vertices [vertex]).x;
				frame.GetComponent<LandFrame> ().activityStrip [vertex] = 1;
				vertex++;
			}
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
