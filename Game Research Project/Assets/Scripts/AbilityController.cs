using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    protected byte _damage;
    protected string _parentID;
    private float _timer;
    private float _timeLimit = 1f;


    public void Initialise(byte Damage, string ParentID)
    {
        _damage = Damage;
        _parentID = ParentID;
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeLimit)
        {
            Destroy(gameObject);
        }
    }
}
