using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Transform barrel;
    public Transform axis;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float size = .3f;

        Gizmos.DrawLine(barrel.position - Vector3.up * size, barrel.position + Vector3.up * size);
        Gizmos.DrawLine(barrel.position - Vector3.left * size, barrel.position + Vector3.left * size);

        Gizmos.color = Color.green;

        Gizmos.DrawLine(axis.position - Vector3.up * size, axis.position + Vector3.up * size);
        Gizmos.DrawLine(axis.position - Vector3.left * size, axis.position + Vector3.left * size);

    }
}
