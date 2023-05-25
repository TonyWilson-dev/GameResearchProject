using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class RoleType 
{
    public GeneInfo _geneInfo;
    protected byte _maxHealth; //change to ushort
    protected float _maxMoveSpeed;
    protected byte _controlGene;
    protected string _agentID;
    public GameObject _abiltyPrefab;
    protected byte _currentHealth; 
    protected float _currentMoveSpeed;
    public byte _abilityValue;
    public bool _alive;

    public abstract void Update();

    public abstract void Ability(Vector3 target);

    public virtual void Initialise(string GeneID)
    {
        _alive = true;
        _agentID = GeneID;
        Stream stream = File.Open(GeneID + ".dat", FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        GeneInfo geneInfoFile;
        if (stream.Length != 0)
        {
            geneInfoFile = (GeneInfo)binaryFormatter.Deserialize(stream);
            SetGeneInfo(geneInfoFile);
            
        }
        else
        {
            _geneInfo = new GeneInfo(GeneID);
            SetGeneInfo(_geneInfo);
            
        }
        stream.Dispose();
    }

    public void SetGeneInfo(GeneInfo geneInfo)
    {
        _geneInfo = geneInfo;
        _maxHealth = geneInfo._geneArray[0];
        _maxMoveSpeed = geneInfo._geneArray[1];
        _controlGene = geneInfo._geneArray[2];
        _abilityValue = geneInfo._geneArray[3];
        
        if (_maxHealth == 0) { _maxHealth = 1; }
        if (_maxMoveSpeed == 0) { _maxMoveSpeed = 1; }

        _currentHealth = _maxHealth;
        _currentMoveSpeed = _maxMoveSpeed;
    }

    public float GetCurrentSpeed()
    {
        return _currentMoveSpeed;
    }

    public void TakeDamage(byte damage)
    {
        if (damage > _currentHealth)
        {
            _currentHealth = 0;
        }
        else
        {

            _currentHealth -= damage;
        }
        if ( _currentHealth <= 0)
        {
            killAgent();
        }
    }

    public void killAgent()
    {
        _alive = false;
    }

    public RoleType()
    {
        Debug.Log("RoleType default constructor used");
    }
    public RoleType(byte[] roleValues)
    {
        _maxHealth = roleValues[0];
        _maxMoveSpeed = roleValues[1];
        _controlGene = roleValues[2];

        if (_maxHealth == 0) { _maxHealth = 1; }
        if (_maxMoveSpeed == 0) { _maxMoveSpeed = 1; }

        _currentHealth = _maxHealth;
        _currentMoveSpeed = _maxMoveSpeed;
    }
}
