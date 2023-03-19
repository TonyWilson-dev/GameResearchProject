using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualControl : ControlType, AIInterface
{
    private bool _selected;

    private Vector3 _mouseScreenPosition;
    private Vector3 _mouseWorldPostion;
    private bool Get_selected()
    {
        return _selected;
    }

    private void Set_selected(bool value)
    {
        _selected = value;
    }

    public override void setDestination(Vector3 destination)
    {
        base.setDestination(destination);
    }

    public override void setSpeed(float speed)
    {
        base.setSpeed(speed);
    }

    public override void setTarget(Vector3 target)
    {
        base.setTarget(target);
    }

    public void Update()
    {
        _mouseScreenPosition = Input.mousePosition;
        _mouseWorldPostion = Camera.main.ScreenToWorldPoint(_mouseScreenPosition);
        
    }
}
