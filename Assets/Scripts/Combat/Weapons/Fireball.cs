using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	public GameObject wall;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), wall.GetComponent<Collider2D> ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
