using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestRole : RoleType
{
    public byte _abilty;
    

    public override void Ability(Vector3 target)
    {
        Debug.Log("Ability called at: " + target + ", ability value: " + _abilty);
        
    }

    public override void Update()
    {
      
    }

    public TestRole(string agentID)
    {
        _agentID = agentID;
        Initialise(agentID);
    }

    public TestRole(byte [] roleValues) :base(roleValues)
    {
        _abiltyPrefab = (GameObject)Resources.Load("TestRoleAbility");
    }
}
