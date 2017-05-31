using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        transform.position = mouse;
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }
}
