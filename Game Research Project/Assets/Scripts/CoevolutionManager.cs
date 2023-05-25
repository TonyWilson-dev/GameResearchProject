using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CoevolutionManager
{
    private bool _active;
    public float _learningRate;
    public string _geneID;
    [SerializeField]
    public PopulationPool _populationPool;

    public void WriteAgentData()
    {
        Stream stream = File.Open(_geneID + ".dat", FileMode.Create);
        BinaryFormatter binary = new BinaryFormatter();
        binary.Serialize(stream, _populationPool._geneInfo);
        stream.Dispose();

        Debug.Log("writing data:" + _populationPool._geneInfo._geneArray);

    }

    public GeneInfo ReadAgentData()
    {
        Stream stream = File.Open(_geneID + ".dat", FileMode.Open);
        BinaryFormatter binary = new BinaryFormatter();
        GeneInfo geneInfo = (GeneInfo)binary.Deserialize(stream);
        stream.Dispose();
        return geneInfo;
    }

    private bool CheckAgentData()
    {
 
        return File.Exists(_geneID + ".dat");    
    }

    public void Update()
    {
        if (_active && _populationPool._geneInfo._dataChangeFlag)
        {
            WriteAgentData();
            _populationPool._geneInfo._dataChangeFlag = false;
        }
    }

    public void Initialise(GameObject agentPrefab, string geneID, float learningRate, float mutationSensitivity)
    {
        _active = true;
        _geneID = geneID;
        _learningRate = learningRate;

        if (CheckAgentData() == false)
        {
            
            _populationPool = new PopulationPool(geneID);
            _populationPool.Randomise();
            WriteAgentData();
        }
        else
        {
            _populationPool = new PopulationPool(ReadAgentData(),mutationSensitivity, learningRate, _geneID);
        }
    }
}
