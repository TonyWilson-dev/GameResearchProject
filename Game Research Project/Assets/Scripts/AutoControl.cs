using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoControl : ControlType, AIInterface
{
    private float _movementTimer = 0f;
    private float _movementTimeLimit = 1f;

    private float _abilityTimer = 0f;
    private float _abilityTimeLimit = 2f;

    private Vector3 _movementOffset;
    private GameObject[] _agents;
    public GameObject _enemyAgent;
    

    public override void Initialise(string AgentID)
    {
        base.Initialise(AgentID);
        _agents = GameObject.FindGameObjectsWithTag("Agent");
        Set_active(true);
        _movementTimeLimit += UnityEngine.Random.Range(-0.2f, 0.2f);
        RandomiseVectors();
        foreach (GameObject agent in _agents)
        {
            if (agent.name != base._agentID)
            {
                _enemyAgent = agent;
            }
        }      
    }
    public override void Update()
    {
        if (base.Get_active())
        {
            _movementTimer += Time.deltaTime;
            if (_movementTimer > _movementTimeLimit)
            {
                RandomiseVectors();
                _movementTimer = 0f;

                _movementTimeLimit += UnityEngine.Random.Range(0.1f, 0.1f);
            }

            _abilityTimer += Time.deltaTime;
            if (_abilityTimer > _abilityTimeLimit)
            {          
                _abilityTimer = 0f;
                useAbility();
                setTarget(_enemyAgent.transform.position);
            }   
            UpdateDestination();           
        }
    }

    private void RandomiseVectors()
    {
        _movementOffset.x = UnityEngine.Random.Range(-4.0f, 4.0f);
        _movementOffset.z = UnityEngine.Random.Range(-4.0f, 4.0f);
    }

    public override void useAbility()
    {
        base.useAbility();
    }

    private void UpdateDestination()
    {
        Vector3 targetDestination = new Vector3();
        targetDestination = _enemyAgent.transform.position;

        targetDestination.x += _movementOffset.x;
        targetDestination.z += _movementOffset.z;

        setDestination(targetDestination);
    }
    public override void setSpeed(float speed)
    {

    }
    public override void setTarget(Vector3 target) 
    {
        base.setTarget(target);
    }
    public override void setDestination(Vector3 destination)
    {
        base.setDestination(destination);
    }
}
