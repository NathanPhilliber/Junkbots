using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour {
    public bool isEnabled = false;
    public bool isToggle = false;
    public bool locked = false;

    void Update()
    {
        if (isEnabled)
        {
            UpdateWhileEnabled();
        }
        else
        {
            UpdateWhileDisabled();
        }
    }

    public void Enable(GameObject activator)
    {
        if (!isEnabled && !locked)
        {
            isEnabled = true;
            OnEnabled(activator);
        }
    }

    public void Disable(GameObject activator)
    {
        if (isEnabled && !locked)
        {
            isEnabled = false;
            OnDisabled(activator);
        }
    }

    public bool Toggle(GameObject activator)
    {
        if (isEnabled)
        {
            Disable(activator);
        }
        else
        {
            Enable(activator);
        }

        return isEnabled;
    }

    public bool ToggleOrEnable(GameObject activator, bool toggleSignal, bool enableSignal)
    {
        if (isToggle)
        {
            if (toggleSignal)
            {
                Toggle(activator);
            }
        }
        else if (enableSignal)
        {
            Enable(activator);
        }
        else
        {
            Disable(activator);
        }

        return isEnabled;
    }

    public abstract void OnEnabled(GameObject activator);

    public abstract void OnDisabled(GameObject activator);

    public abstract void UpdateWhileEnabled();

    public virtual void UpdateWhileDisabled() {} // Override if needed
}
