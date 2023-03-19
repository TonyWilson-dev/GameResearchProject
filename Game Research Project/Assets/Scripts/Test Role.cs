using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRole : RoleType
{
   

    public override void Ability(Vector3 target)
    {
        Debug.Log("Ability called" + _maxHealth);
    }

    public override void Initialise()
    {
        _maxHealth = 50f;
        _maxMoveSpeed = 10f;
        base.Initialise();
    }
}
