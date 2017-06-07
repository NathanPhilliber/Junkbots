using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleIsaCam : Device {

    public ErlIsaCamera cam;

    void Start()
    {
        isEnabled = true;
        
    }

    public override void OnDisabled(GameObject activator)
    {
        if (cam != null)
            cam.followErl = true;
    }

    public override void OnEnabled(GameObject activator)
    {
        if (cam != null)
            cam.followErl = false;
    }

    public override void UpdateWhileEnabled()
    {
        
    }


}
