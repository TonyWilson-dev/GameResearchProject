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

    public override void Initialise(byte[] genes)
    {
        _abilty = genes[3];
        base.Initialise(genes);
    }
}
