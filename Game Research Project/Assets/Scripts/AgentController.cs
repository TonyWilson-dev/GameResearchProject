using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class AgentController : MonoBehaviour
{
    public bool _active;
    public AutoControl _control;
    public RoleType _role;
    public Text header;
    public Material _agentDeadMaterial;

    public void Initialise()
    {
        _active = true;

        byte[] roleValues = ReadAgentData();

        if (roleValues != null)
        {
            _role = new TestRole(roleValues);
            _role.Initialise(gameObject.name);
        }
        else
        {
            Debug.Log("roleValues null for: " + gameObject.name);
        }     
        _control = new AutoControl();
        _control.Initialise(this.name);
    }

    public byte[] ReadAgentData()
    {
        Stream stream = File.Open(this.gameObject.name + ".dat", FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        GeneInfo geneInfoFile;
        if (stream.Length != 0)
        {
            geneInfoFile = (GeneInfo)binaryFormatter.Deserialize(stream);
            Debug.Log(geneInfoFile._geneID + " Serialized, Gene Array Values:" + geneInfoFile._geneArray[0] + geneInfoFile._geneArray[1] + geneInfoFile._geneArray[2] + geneInfoFile._geneArray[3]);
            stream.Dispose();
            return geneInfoFile._geneArray;

        }
        else
        {
            stream.Dispose();
            return null;
        }      
    }

    public void Update()
    { 
        if (_active)
        {
            header.text = gameObject.name;
            _control.Update();
            MoveToPosition(_control.Destination);
            CheckAbility();

            if (!_role._alive)
            {
                _active = false;
                gameObject.GetComponent<Renderer>().material = _agentDeadMaterial;
            }
        }
    }

    public void MoveToPosition(Vector3 destination)
    {
        Vector3 movementVector = destination - gameObject.transform.position;
        movementVector.y = 0f; 
        Vector3.Normalize(movementVector);
        transform.Translate(movementVector * _role.GetCurrentSpeed() * Time.deltaTime * 0.5f);
    }

    public void CheckAbility()
    {
        if (_control._useAbility && _role._alive)
        {
            var abiltityObject = Instantiate(_role._abiltyPrefab, _control.Destination, Quaternion.identity);

            AbilityController abilityController = abiltityObject.GetComponent<AbilityController>();
            abilityController.Initialise(_role._abilityValue, this.gameObject.name);
            _control._useAbility = false;
        }

    }
}
