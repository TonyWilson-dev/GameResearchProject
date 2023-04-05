using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class RoleType :Component
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
        
    }

    public virtual void Initialise(byte[] genes)
    {
        Stream stream = File.Open("GeneData.dat", FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        GeneInfo geneInfo;

        geneInfo = (GeneInfo)binaryFormatter.Deserialize(stream);

        Debug.Log(geneInfo._geneID + " serialized");
        Debug.Log(geneInfo._geneArray[1] + " generray");

        _maxHealth = geneInfo._geneArray[0];
        _maxMoveSpeed = geneInfo._geneArray[1];
        _size = geneInfo._geneArray[3];

        if (_maxHealth == 0) { _maxHealth = 1; }
        if (_maxMoveSpeed == 0) { _maxMoveSpeed = 1; }

        _currentHealth = _maxHealth;
        _currentMoveSpeed = _maxMoveSpeed;
    }

    public float GetCurrentSpeed()
    {
        return _currentMoveSpeed;
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

    public RoleType()
    {
        
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
