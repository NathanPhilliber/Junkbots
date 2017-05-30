using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Erl")
        {
            Destroy(other.gameObject);
        }
    }
}
