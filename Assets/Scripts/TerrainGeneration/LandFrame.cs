using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandFrame : MonoBehaviour, IFrame {

	public float xDifference;			//	Used for how far away the next vertex will be
	public float maxYDifference;		//	Used for how high the next vertex can be from this one
	public float minYDifference;		//	Used for how low the next vertex can be from this one
	public float landDepth;				//	How deep the land should be
	public Material material;			//	The material for this piece of land

	public GameObject nextFrame = null;

	private TerrainFrame frame;			//	The land frame
	private EdgeCollider2D collider;	//	Collider
	private Mesh mesh;					//	Land mesh
	private MeshFilter meshFilter;		//	Mesh filter
	private MeshRenderer meshRenderer;	//	Renderer

	public void FillFrame(){
		frame = GetComponent<TerrainFrame> ();											//	Assign frame
		mesh = new Mesh ();																//	New mesh
		meshFilter = (MeshFilter)gameObject.AddComponent (typeof(MeshFilter));			//	Add mesh filter
		meshRenderer = (MeshRenderer)gameObject.AddComponent (typeof(MeshRenderer));	//	Add mesh renderer
		collider = (EdgeCollider2D)gameObject.AddComponent (typeof(EdgeCollider2D));	//	Add edge collider
		meshFilter.mesh = mesh;															//	Assign mesh to filter

		int impVertices = (int)((frame.width / xDifference) + 3);						//	The number of important vertices
		int middleVertices = (int)((impVertices - 5) / 2);								//	The number of vertices that are used for triangles
		Vector3[] vertices = new Vector3[impVertices + middleVertices];					//	Vertices array
		Vector2[] uvs = new Vector2[vertices.Length];									//	UVs array
		Vector2[] points = new Vector2[vertices.Length];								//	Collider points array
		int[] triangles = new int[(vertices.Length - 2) * 3];							//	Triangles array

		mesh.Clear ();																	//	Reset mesh

		vertices [0] = new Vector2(0, frame.enterY -landDepth);										//	Start in bottom left corner
		vertices [1] = new Vector2(0, frame.enterY - transform.position.y);				//	Upper left corner

		points[0] = new Vector2(vertices[0].x, vertices[0].y);							//	Bottom left corner for collider point
		points[1] = new Vector2(vertices[1].x, vertices[1].y);							//	Upper left corner for collider points

		float xCur = 0;																	//	The current x position to place a vertex at
		float yCur = vertices[1].y;														//	The current y position to place a vertex at
		float lastRandom = 0;															//	Used to see if we went up or down last time

		for (int i = 2; i < vertices.Length - 1 - middleVertices; i++) {				//	Place all important vertices
			xCur += xDifference;														//	Add x displacement to xCur
			float curRandom = Random.Range (minYDifference, maxYDifference);			//	Get a random y offset
			if (Mathf.Sign (curRandom) != Mathf.Sign (lastRandom) && lastRandom != 0) {	//	If we're going in a different direction than last time
				curRandom = 0;															//	Then make this y change zero, this prevents sharp points
			}

			yCur += curRandom;															//	Add the y change to the current y
			lastRandom = curRandom;														//	Save the y change for next iteration

			vertices [i] = new Vector3 (xCur, yCur, 0);									//	Save the vertice
			points [i] = new Vector2 (xCur, yCur);										//	Save the collider point

		}

		xCur = frame.width;																		//	Start xCur at the end of the frame

		vertices [vertices.Length - 1 - middleVertices] = new Vector3 (xCur, frame.enterY -landDepth, 0);	//	Place the bottom right vertex
		points [vertices.Length - 1 - middleVertices] = new Vector2 (xCur,frame.enterY  -landDepth);			//	Place the bottom right collider point


		for (int i = vertices.Length - middleVertices; i < vertices.Length; i++) {		//	Place all the middle vertices, these are used to create nice triangles
			xCur -= xDifference*2;														//	Move xCur 2 x displacements to the left
			vertices [i] = new Vector3 (xCur, frame.enterY -landDepth, 0);							//	Place a vertex along the bottom
			points [i] = new Vector2 (xCur, frame.enterY -landDepth);								//	Place a collider point along the bottom, this is unnecessary, remove this to optimize
		}

		int size = vertices.Length;								//	The number of vertices
		int forwards = 1;										//	Used to keep track of vertex on top of triangle
		int anchor = 0;											//	Used to keep track of vertex on bottom left of triangle
		int backwards = -1;										//	Used to keep track of vertex on bottom right of triangle

		for (int i = 0; i < (int)(triangles.Length);) {			//	Loop through all triangle points
			triangles [i] = anchor;								//	Create first triangle, 2 points on top, one on bottom
			triangles [i + 1] = forwards;
			triangles [i + 2] = ++forwards;
			i += 3;

			triangles [i] = anchor;								//	Create second triangle, 2 points on top, one on bottom
			triangles [i + 1] = forwards;
			triangles [i + 2] = ++forwards;
			i += 3;

			triangles [i] = anchor;								//	Create third triangle, 1 point on top, two on bottom
			triangles [i + 1] = forwards;
			triangles [i + 2] = size + backwards;
			i += 3;

			anchor = size + backwards;							//	Move the anchor to the right
			backwards--;										//	Move the bottom tracker to the right

		}

		for (int i = 0; i < uvs.Length; i++) {						//	Assign all uvs, just copy vertices
			uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
		}

		frame.exitY = yCur + transform.position.y;					//	Save exit position

		collider.points = points;									//	Assign collier points
		mesh.vertices = vertices;									//	Assign vertices
		mesh.uv = uvs;												//	Assign uvs
		mesh.triangles = triangles;									//	Assign triangles
		mesh.RecalculateNormals ();									//	Calculate the normals with the new vertices
		meshRenderer.sharedMaterial = material;						//	Set the material
	}
}
