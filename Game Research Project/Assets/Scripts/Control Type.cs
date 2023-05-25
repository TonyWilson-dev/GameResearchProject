using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlType
{
    protected string _agentID;
    private bool _active;
    private Vector3 _destination;
    protected Vector3 _target;
    public bool _useAbility;
    public Vector3 Destination { get => _destination; set => _destination = value; }

    protected bool Get_active()
    {
        return _active;
    }

    protected void Set_active(bool value)
    {
        _active = value;
    }

    public virtual void Update()
    { }


    public virtual void setDestination(Vector3 destination)
    {
        if (destination.x >= 15f )
        {
            destination.x = 15f;
        }
        if (destination.x <= -15f)
        {               
            destination.x = -15f;
        }
        if (destination.z >= 15f)
        {
            destination.z = 15f;
        }
        if (destination.z <= -15f)
        {               
            destination.z = -15f;
        }

        _destination = destination;
    }

    public virtual void setSpeed(float speed)
    {
        
    }

    public virtual void setTarget(Vector3 target)
    {      
        _target = target; 
    }

    public virtual void Initialise(string AgentID)
    {
        _agentID = AgentID;      
    }

    public virtual void useAbility()
    {
        _useAbility = true;
    }
}
