using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoleAbility : AbilityController
{
    private float _particlePersistence;
    private bool _damageUsed;
    private GameObject[] _agents;
    private float _range = 1f;


    private void Start()
    {
        _agents = new GameObject[2];
        _damageUsed = false;
    }


    void FixedUpdate()
    {
        CheckCollisions();
    }

    void CheckCollisions()
    {
        _agents = GameObject.FindGameObjectsWithTag("Agent");
        foreach (GameObject agent in _agents)
        {
            if (agent != null)
            {
                float agentDistance = new float();
                agentDistance = Vector3.Distance(agent.transform.position, transform.position);

                if (agentDistance < _range && agent.name != _parentID && _damageUsed == false)
                {
                    (agent.GetComponent<AgentController>())._role.TakeDamage(_damage);
                    _damageUsed = true;
                }
            }
        }
    }
}
