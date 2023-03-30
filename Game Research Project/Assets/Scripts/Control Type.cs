using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlType
{
    private bool _active;
    private Vector3 _destination;

    public Vector3 Destination { get => _destination; set => _destination = value; }

    protected bool Get_active()
    {
        return _active;
    }

    private void Set_active(bool value)
    {
        _active = value;
    }
    public virtual void setDestination(Vector3 destination)
    {
        _destination = destination;
    }

    public virtual void setSpeed(float speed)
    {
        
    }

    public virtual void setTarget(Vector3 target)
    {
        Debug.Log("Setting target: " + target);

    }


    public virtual void useAbility()
    {
        Debug.Log("using ability");
    }

    public virtual void Initialise()
    {
        Set_active(true);
    }
}
