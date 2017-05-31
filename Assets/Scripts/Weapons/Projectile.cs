using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Projectile : MonoBehaviour {

    public float speed;
    public LayerMask collisionMask;

    Rigidbody2D rb;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        float angle = transform.rotation.eulerAngles.z + 90;

        rb.velocity += new Vector2(speed * Mathf.Cos(angle * Mathf.Deg2Rad), speed * Mathf.Sin(angle * Mathf.Deg2Rad));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((collisionMask & (1 << other.gameObject.layer)) > 0)
            Destroy(gameObject);
    }
}
