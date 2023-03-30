using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualControl : ControlType, AIInterface
{
    private bool _selected;

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

    public override void Initialise()
    {
        base.Initialise();
        _selected = false;
    }

    public void Update()
    {
        if (_selected && base.Get_active())
        {
            UpdateDestination();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 _mouseScreenPosition = Input.mousePosition;
            Vector3 _mouseWorldPostion = Camera.main.ScreenToWorldPoint(_mouseScreenPosition);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Agent")
                {            
                    Set_selected(!Get_selected());
                }
            }
        } //select/unselect agent on left click
        if (Input.GetMouseButtonDown(1))
        {
            Set_selected(false);
        }; //deselect agent on right click
    }

    public void UpdateDestination()
    {
        Vector3 _mouseScreenPosition = Input.mousePosition;
        Vector3 _mouseWorldPostion = Camera.main.ScreenToWorldPoint(_mouseScreenPosition);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag != "Agent") 
            {
                setDestination(hit.point);
            }
        }
    }

}
