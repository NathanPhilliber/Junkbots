using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandFrame : MonoBehaviour, IFrame {

	public float xDifference;		//Used for how far away the next vertex will be
	public float maxYDifference;	//Used for how high/low the next vertex can be from this one
	public float landDepth;
	public Material material;

	private TerrainFrame frame;
	private EdgeCollider2D collider;
	private Mesh mesh;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FillFrame(){
		frame = GetComponent<TerrainFrame> ();
		mesh = new Mesh ();
		meshFilter = (MeshFilter)gameObject.AddComponent (typeof(MeshFilter));
		meshRenderer = (MeshRenderer)gameObject.AddComponent (typeof(MeshRenderer));
		collider = (EdgeCollider2D)gameObject.AddComponent (typeof(EdgeCollider2D));
		meshFilter.mesh = mesh;

		int impVertices = (int)((frame.width / xDifference) + 3);
		int middleVertices = (int)((impVertices - 5) / 2);
		Vector3[] vertices = new Vector3[impVertices + middleVertices];
		Vector2[] uvs = new Vector2[vertices.Length];
		Vector2[] points = new Vector2[vertices.Length];
		int[] triangles = new int[(vertices.Length - 2) * 3];

		mesh.Clear ();


		vertices [0] = new Vector2(0, -landDepth);
		vertices [1] = new Vector2(0, frame.enterY - transform.position.y);

		points[0] = new Vector2(vertices[0].x, vertices[0].y);
		points[1] = new Vector2(vertices[1].x, vertices[1].y);

		float xCur = 0;
		float yCur = vertices[1].y;

		for (int i = 2; i < vertices.Length - 1 - middleVertices; i++) {
			xCur += xDifference;
			yCur += Random.Range (-maxYDifference, maxYDifference);
			vertices [i] = new Vector3 (xCur, yCur, 0);
			points [i] = new Vector2 (xCur, yCur);

		}

		xCur = frame.width;

		vertices [vertices.Length - 1 - middleVertices] = new Vector3 (xCur, -landDepth, 0);
		points [vertices.Length - 1 - middleVertices] = new Vector2 (xCur, -landDepth);


		for (int i = vertices.Length - middleVertices; i < vertices.Length; i++) {
			xCur -= xDifference*2;
			vertices [i] = new Vector3 (xCur, -landDepth, 0);
			points [i] = new Vector2 (xCur, -landDepth);
		}

		//Calculate Triangles

		int size = vertices.Length;
		int forwards = 1;
		int anchor = 0;
		int backwards = -1;

		for (int i = 0; i < (int)(triangles.Length);) {
			triangles [i] = anchor;
			triangles [i + 1] = forwards;
			triangles [i + 2] = ++forwards;
			//print ("Triangle 1: (" + triangles [i] + ", " + triangles [i + 1] + ", " + triangles [i + 2] + ")");
			i += 3;

			triangles [i] = anchor;
			triangles [i + 1] = forwards;
			triangles [i + 2] = ++forwards;
			//print ("Triangle 2: (" + triangles [i] + ", " + triangles [i + 1] + ", " + triangles [i + 2] + ")");
			i += 3;

			triangles [i] = anchor;
			triangles [i + 1] = forwards;
			triangles [i + 2] = size + backwards;
			//print ("Triangle 3: (" + triangles [i] + ", " + triangles [i + 1] + ", " + triangles [i + 2] + ")");
			i += 3;

			anchor = size + backwards;
			backwards--;

		}

		for (int i = 0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
		}
		//print ("Total Triangles: " + (triangles.Length / 3));
		frame.exitY = yCur + transform.position.y;

		collider.points = points;
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		mesh.RecalculateNormals ();
		meshRenderer.sharedMaterial = material;
	}
}
