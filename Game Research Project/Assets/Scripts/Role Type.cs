using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoleType 
{
    protected byte _maxHealth; //change to ushort
    protected byte _maxMoveSpeed;
    protected byte _size;

    //ability ushort
    
    protected byte _currentHealth; 
    protected byte _currentMoveSpeed; 

    public abstract void Ability(Vector3 target);
    public virtual void Initialise()
    {
        if (_maxHealth == 0) { _maxHealth = 50; }
        if (_maxMoveSpeed == 0) { _maxMoveSpeed = 5; }
        _currentHealth = _maxHealth;
        _currentMoveSpeed = _maxMoveSpeed;
    }

    public virtual void Initialise(byte[] genes)
    {
        _maxHealth = genes[0];
        _maxMoveSpeed = genes[1];
        _size = genes[2];

        Initialise();
    }

    public float GetCurrentSpeed()
    {
        return _currentMoveSpeed;
    }
    public RoleType()
    {
    }

    public RoleType(string bitString)
    {
        //TODO::
        //check CNA and data serialising
        //break into 16 bit sections
        //for loop for each character in segment
        //binary to string to ushort 

        //convert sections of text into binary data

        //use data to set up member variables
    }

    public void TakeDamage(byte damage)
    {
        _currentHealth -= damage;

        if ( _currentHealth <= 0)
        {
            killAgent();
        }
    }

    public void killAgent()
    {
        Debug.Log("Agent Killed");
    }

}
