using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Transform barrelEnd;

    public Projectile projectile;


    private float angleMod;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void Fire()
    {
        Projectile newProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
    }

    public void Fire(Vector2 extraVelocity)
    {
        Projectile newProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation);
        newProjectile.GetComponent<Rigidbody2D>().velocity += extraVelocity;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float size = .3f;

        Gizmos.DrawLine(barrelEnd.position - Vector3.up * size, barrelEnd.position + Vector3.up * size);
        Vector3 barrelAim = new Vector3(size * 3 * Mathf.Cos((90 +barrelEnd.eulerAngles.z) * Mathf.Deg2Rad),
                                        size * 3 * Mathf.Sin((90 + barrelEnd.eulerAngles.z) * Mathf.Deg2Rad));
        Gizmos.DrawLine(barrelEnd.position, barrelEnd.position + barrelAim);
    }
}
