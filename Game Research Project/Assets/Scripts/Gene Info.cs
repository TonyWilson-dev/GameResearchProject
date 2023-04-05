using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable()]
public class GeneInfo : ISerializable
{
    public byte[] _geneArray;
    public string _geneID = "test gene ID";
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("GeneArray", _geneArray);
        info.AddValue("GeneID", _geneID);
    }
    public GeneInfo(byte[] geneArray, string geneID)
    {
        _geneArray = geneArray;
        _geneID = geneID;
    }

    public GeneInfo(SerializationInfo info, StreamingContext context)
    {
         _geneArray = (byte[])info.GetValue("GeneArray",typeof(object));
         _geneID = (string)info.GetValue("GeneID", typeof(string));
    }
}
