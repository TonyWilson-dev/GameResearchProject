using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoleType 
{
    protected float _maxHealth;
    protected float _maxMoveSpeed;
    protected float _size;
    
    protected float _currentHealth;
    protected float _currentMoveSpeed;
    public abstract void Ability(Vector3 target);
    public virtual void Initialise()
    {
        _currentHealth = _maxHealth;
        _currentMoveSpeed = _maxMoveSpeed;
    }

}
