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
        Debug.Log("updating");
       _control.Update();
    }
}
