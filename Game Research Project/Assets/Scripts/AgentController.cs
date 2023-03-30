using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    public ManualControl _control;
    public TestRole _role;
    private List<GameObject> _SenseArray = new List<GameObject>();

    private void Start()
    {
       
        _control = new ManualControl();
        _role = new TestRole();
        _control.Initialise();
        _role.Initialise();
    }

    private void Update()
    {
       _control.Update();
        MoveToPosition(_control.Destination);
    }

    public void MoveToPosition(Vector3 destination)
    {
        Vector3 movementVector = destination - gameObject.transform.position;
        movementVector.y = 0f; 
        Vector3.Normalize(movementVector);
        transform.Translate(movementVector * _role.GetCurrentSpeed() * Time.deltaTime);
    }



}
