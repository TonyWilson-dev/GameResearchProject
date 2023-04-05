using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CoevolutionManager : MonoBehaviour
{

    private byte[] _testGeneArray = new byte[6] { 1, 2, 3, 4, 5, 6 };

    private GeneInfo _testGeneInfo = new GeneInfo(new byte[] {1,2,3,4,5,6}, "testGeneID"); 

    public GameObject _agentPrefab;

    public void Start()
    {
        Instantiate(_agentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        TestRole agent = _agentPrefab.GetComponent<TestRole>();

        Stream stream = File.Open("GeneData.dat", FileMode.Create);

        BinaryFormatter binary = new BinaryFormatter();

        binary.Serialize(stream, _testGeneInfo);

        stream.Dispose();
        
        agent.Initialise(_testGeneArray);

    }
}
