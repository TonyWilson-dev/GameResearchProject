using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable()]
public class GeneInfo : ISerializable
{
    public bool _dataChangeFlag;
    public byte[] _geneArray;
    public string _geneID;
    public float _learningRate;
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("GeneArray", _geneArray);
        info.AddValue("GeneID", _geneID);
        info.AddValue("LearningRate", _learningRate);
    }
    public GeneInfo(byte[] geneArray, string geneID, float learningRate)
    {
        _geneArray = geneArray;
        _geneID = geneID;
        _learningRate = learningRate;
    }

    public GeneInfo(string geneID)
    {
        _geneID = geneID;
    }

    public string GeneString()
    {
        string genes = "";

        foreach (byte gene in _geneArray )
        {
            genes += gene.ToString();
        }

        return genes;
    }
    public GeneInfo(SerializationInfo info, StreamingContext context)
    {
         _geneArray = (byte[])info.GetValue("GeneArray",typeof(object));
         _geneID = (string)info.GetValue("GeneID", typeof(string));
    }
   
}
