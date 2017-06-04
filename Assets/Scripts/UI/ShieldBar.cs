using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBar : MonoBehaviour {

    public float scaleY, scaleZ;
    public Shield shield;

    // Use this for initialization
    void Start()
    {
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        float scaleX = (float)shield.currentCharge / (float)shield.maxCharge;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
