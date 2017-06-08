using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{

    public int maxHealth;
    public int health;
    public int defense;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoDamage(int damage)
    {
        damage -= defense;
        if (damage <= 0)
        {
            OnDefended();
        }
        else
        {
            health -= damage;
            OnHealthDecreased(damage);

            if (health <= 0)
            {
                health = 0;
                OnHealthZero();
            }
        }
        
    }

    public void OnDefended()
    {
        // Override for implementation
    }

    public abstract void OnHealthDecreased(int amount);

    public abstract void OnHealthZero();
}
