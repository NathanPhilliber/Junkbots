using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    public int damage;
    public LayerMask victimMask;

    public bool unstoppable = false;

    /* 
     * Reduces the object by the appropriate amount of health on
     * appropriate layer to be damaged
     * */
    public void Damage(Damageable damageable)
    {
        if (IsVictim(damageable.gameObject))
        {
            damageable.DoDamage(damage);
        }
    }

    public bool IsVictim(GameObject gameObject)
    {
        return (victimMask & (1 << gameObject.layer)) != 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            Damage(damageable);
        }
    }
}
