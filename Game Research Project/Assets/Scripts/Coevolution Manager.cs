using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoevolutionManager : MonoBehaviour
{
    private byte[] _testGeneArray = new byte[6] { 1, 2, 3,4,5,6};
    public GameObject _agentPrefab;

    public void Start()
    {
        Instantiate(_agentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        TestRole agent = _agentPrefab.GetComponent<TestRole>();
        agent.Initialise(_testGeneArray);
    }

}
