using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SimulationManager :MonoBehaviour
{
    //Dictionaries
    public Dictionary<int, GameObject> _agentDictionary;
    public Dictionary<int, CoevolutionManager> _coevolutionDictionary;

    //customizable round settings
    [SerializeField]
    private int _roundLimit;
    [SerializeField]
    private float _roundTimelimit;
    public GameObject _agentPrefab;
    [SerializeField]
    public string _simulationName;


    // round data
    private int _agentsAmount = 2;
    [SerializeField]
    private float _roundTimer;
    [SerializeField]
    private bool _roundActive;
    [SerializeField]
    public float _roundCounter;
    public bool _simulationActive;

    public Material _agent0Material;
    public Material _agent1Material;
    private string _saveDatapath;


    void Start()
    {
        _simulationActive = true;
        _saveDatapath = Application.dataPath + _simulationName + ".txt";
        _agentDictionary = new Dictionary<int, GameObject>();
        _coevolutionDictionary = new Dictionary<int, CoevolutionManager>();       
        
        for (int i = 0; i < _agentsAmount; i++)
        {
            _coevolutionDictionary.Add(i, new CoevolutionManager());
            _coevolutionDictionary[i].Initialise(_agentPrefab, "AgentID" + i, 0.1f, 1.5f);
        }
        _roundCounter = 1;
        StartRound();
    }
    public void StartRound()
    {
        _roundTimer = 0f;
        _roundActive = true;
        SpawnAgents();
        
    }
    public void Update()
    {
        if (_roundActive)
        {
            _roundTimer += Time.deltaTime;
            if (_roundTimer > _roundTimelimit)
            {
                EndRound();
            }
            if (_agentDictionary != null)
            {
               
            }

            foreach (var coevolution in _coevolutionDictionary)
            {
                coevolution.Value.Update();
            }

        }       
    }
    public void SpawnAgents()
    {
        for (int i = 0; i < _agentsAmount; i++)
        {
            var obj = Instantiate(_agentPrefab, new Vector3(0 + i * 10, 1f , 0), Quaternion.identity);
            obj.name = "AgentID" + i;
            _agentDictionary.Add(i, obj);
        }

        foreach(var agent in _agentDictionary)
        {
            AgentController agentController = agent.Value.GetComponent<AgentController>();
            agentController.Initialise();
        }

        _agentDictionary[0].GetComponent<Renderer>().material = _agent0Material;
        _agentDictionary[1].GetComponent<Renderer>().material = _agent1Material;


    }
    public void EndRound()
    {

        var agent1controller = _agentDictionary[0].GetComponent<AgentController>();
        var coevolutionManager1 = _coevolutionDictionary[0];
        var matchresult0 = new MatchData();
        matchresult0.agentID = agent1controller.name;
        matchresult0.matchNumber = (int)_roundCounter;

        var agent2controller = _agentDictionary[1].GetComponent<AgentController>();
        var coevolutionManager2 = _coevolutionDictionary[1];
        var matchresult1 = new MatchData();
        matchresult1.agentID = agent2controller.name;
        matchresult1.matchNumber = (int)_roundCounter;

        if (agent1controller._role._alive && !agent2controller._role._alive)
        {
            matchresult0.result = Result.Win;
            matchresult1.result = Result.Loss;
            coevolutionManager1._populationPool.MatchUpdate(true);
            coevolutionManager2._populationPool.MatchUpdate(false);
        }

        if (agent2controller._role._alive && !agent1controller._role._alive)
        {
            matchresult0.result = Result.Loss;
            matchresult1.result = Result.Win;
            coevolutionManager2._populationPool.MatchUpdate(true);
            coevolutionManager1._populationPool.MatchUpdate(false);
        }

        if (agent1controller._role._alive && agent2controller._role._alive)
        {
            matchresult0.result = Result.Draw;
            matchresult1.result = Result.Draw;
            Debug.Log("draw, no deaths");
        }

        if (!agent1controller._role._alive && !agent2controller._role._alive)
        {
            matchresult0.result = Result.Draw;
            matchresult1.result = Result.Draw;
            Debug.Log("draw, two deaths");
        }

        matchresult0.winrate = Mathf.Max(((_coevolutionDictionary[0]._populationPool._wins / _roundCounter) * 100), 0);
        matchresult1.winrate = Mathf.Max(((_coevolutionDictionary[1]._populationPool._wins / _roundCounter) * 100), 0);

        matchresult0.geneValues = _coevolutionDictionary[0]._populationPool._geneInfo;
        matchresult1.geneValues = _coevolutionDictionary[1]._populationPool._geneInfo;

        WriteToFile(matchresult0);

        WriteToFile(matchresult1);

        foreach (KeyValuePair<int, GameObject> agent in _agentDictionary)
        {
            Destroy(agent.Value.gameObject);
        }

        _agentDictionary.Clear();

        _roundCounter++;
        _roundActive = false;

        if (_roundCounter >= _roundLimit)
        {
            EndSimulation();
        }
        else
        {
            StartRound();
        }
    }

    public void EndSimulation()
    {
        _simulationActive = false;
        Debug.Log("Simulation End");
    }


    public void WriteToFile(MatchData matchData)
    {
        string Text = matchData.agentID + ", Match Number: "+ matchData.matchNumber + ", Result: " + matchData.result.ToString() + ", Winrate: " + matchData.winrate + "%, Genes:" + matchData.geneValues.GeneString() + "\r\n";

        if (!File.Exists(_saveDatapath))
        {
            File.WriteAllText(_saveDatapath, "Starting simulation data recording:" + "\r\n" +  Text + "\r\n");
        }
        else
        {
            using (var writer = new StreamWriter(_saveDatapath, append:true))
            {
                writer.WriteLine(Text);
            }
            
        }
    }

    public struct MatchData
    {
        public string agentID;
        public Result result;
        public int matchNumber;
        public float winrate;
        public GeneInfo geneValues;
    }

    public enum Result
    {
        Win,
        Loss,
        Draw,
    }
}
