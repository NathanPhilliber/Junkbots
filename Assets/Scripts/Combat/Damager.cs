using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    public int damage;
    public LayerMask victimMask;

    /* 
     * Reduces the object by the appropriate amount of health
     * and returns the amount of damage done or -1 if not on 
     * appropriate layer to be damaged
     * */
    public int Damage(Damageable damageable)
    {
        if (IsVictim(damageable.gameObject))
        {
            int damageDone = damage - damageable.defense;
            damageable.DoDamage(damage - damageable.defense);
            return damageDone;
        }
        else
        {
            return -1;
        }
    }

    public bool IsVictim(GameObject gameObject)
    {
        return (victimMask & (1 << gameObject.layer)) != 0;
    }
}
