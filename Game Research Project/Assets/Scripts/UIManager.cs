using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SimulationManager simulation;
    public Text matchCounter;
    public Text agent0Text;
    public Text agent1Text;
    public GeneInfo ReadAgentData(string geneID)
    {
        Stream stream = File.Open(geneID + ".dat", FileMode.Open);
        BinaryFormatter binary = new BinaryFormatter();
        GeneInfo geneInfo = (GeneInfo)binary.Deserialize(stream);
        stream.Dispose();
        return geneInfo;
    }

    // Start is called before the first frame update
    void Start()
    {
        simulation = FindObjectOfType<SimulationManager>();
        Debug.Log("simulation found:" + simulation);
    }

    // Update is called once per frame
    void Update()
    {
        if (simulation._simulationActive)
        {
            matchCounter.text = ("Match number: " + simulation._roundCounter);
            string Agent0String = new string("");
            string Agent1String = new string("");


            GameObject Agent0 = simulation._agentDictionary[0];

            GeneInfo genes0 = ReadAgentData(Agent0.name);

            foreach (byte gene in genes0._geneArray)
            {
                Agent0String += gene.ToString() + ",";
            }

            GameObject Agent1 = simulation._agentDictionary[1];

            GeneInfo genes1 = ReadAgentData(Agent1.name);

            foreach (byte gene in genes1._geneArray)
            {
                Agent1String += gene.ToString() + ",";
            }

            agent0Text.text = "agent 0 gene values" + Agent0String;
            agent1Text.text = "agent 1 gene values" + Agent1String;
        }
        else
        {
            matchCounter.text = ("Simulation concluded");
        }
    }
}
