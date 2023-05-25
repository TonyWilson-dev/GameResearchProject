using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationPool
{  
    public GeneInfo _geneInfo;
    public string _geneID;
    private float _mutationSensitivity;
    public int _StartingMatches = 6;
    public float _matches;
    public float _wins;
    public float _losses;
    public float _learningRate;
    public float winrate;
    public float lossrate;


    public PopulationPool(string GeneID)
    {
        _geneID = GeneID;
    }

    public PopulationPool(GeneInfo geneInfo, float MutationSensitivity, float LearningRate, string GeneID)
    {
        _geneID = GeneID;
        _learningRate = LearningRate;
        _geneInfo = geneInfo;
        _mutationSensitivity = MutationSensitivity;
        _geneInfo._dataChangeFlag = false;
    }
    public void GeneUpdate()
    {
        winrate = (_wins / _matches * 100);
        lossrate = (_losses / _matches * 100);

        // && _matches > _StartingMatches


        if (lossrate - winrate > _mutationSensitivity)
        {
            mutateGenePositive();
        } 
        else if (winrate - lossrate > _mutationSensitivity)
        {
            mutateGeneNegative();
        }
    }

    public void MatchUpdate(bool win)
    {
        _matches++;
        if (win) 
        { 
            _wins++; 
        }
        else
        {
            _losses++;
        }
        GeneUpdate();
    }

    public void Randomise()
    {
        _geneInfo = new GeneInfo(_geneID);
        Debug.Log("Randomising:" + _geneInfo._geneID);
        _geneInfo._geneArray = new byte[4];
        for (int i = 0; i < _geneInfo._geneArray.Length; i++)
        {
            _geneInfo._geneArray[i] = (byte)UnityEngine.Random.Range(1, 10);
        }
    }

    public void mutateGenePositive()
    {
        int randomGene = UnityEngine.Random.Range(0, _geneInfo._geneArray.Length);
        checked
        {
            Debug.Log("before: " + _geneInfo._geneArray[randomGene]);
            byte increment = (byte)((float)_geneInfo._geneArray[randomGene]  * _learningRate);

            increment = (byte)(Mathf.Max((int)increment, 1));

            Debug.Log("increment is:" + increment);

            //byte increment = (byte)((lossrate/100) * _learningRate);
            _geneInfo._geneArray[randomGene] += increment;
            Debug.Log("after: " + _geneInfo._geneArray[randomGene]);

            Debug.Log("save data:" + _geneInfo._geneArray);

            _geneInfo._dataChangeFlag = true;
        }
    }

    public void mutateGeneNegative()
    {
        int randomGene = UnityEngine.Random.Range(0, _geneInfo._geneArray.Length);
        checked
        {
            Debug.Log("before: " + _geneInfo._geneArray[randomGene]);
            byte increment = (byte)((float)_geneInfo._geneArray[randomGene] * _learningRate);

            increment = (byte)(Mathf.Max((int)increment, 1));
            Debug.Log("increment is:" + increment);

            //byte increment = (byte)((winrate/100) * _learningRate);
            if (increment > _geneInfo._geneArray[randomGene])
            {
                _geneInfo._geneArray[randomGene] -= increment;
            }
            else
            {
                _geneInfo._geneArray[randomGene] = 1;
            }

            Debug.Log("after: " + _geneInfo._geneArray[randomGene]);

            Debug.Log("save data:" + _geneInfo._geneArray);

            _geneInfo._dataChangeFlag = true;
        }
    }
}
