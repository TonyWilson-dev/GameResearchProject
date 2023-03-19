using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRole
{
    void Initalise();
    float Health { get; set; }
    void TakeDamage(float damageValue);
    
}