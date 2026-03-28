using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HomophonesData
{
    public List<WordPair> wordPairs = new();
    public int errorCount;

    public HomophonesData(List<WordPair> wordPairs, int errorCount) 
    { 
        this.wordPairs = wordPairs;
        this.errorCount = errorCount;
    }

    public override string ToString()
    {
        return $"Homophones: {string.Join(", ", wordPairs)}; " +
            $"Errors: {errorCount}";
    }
    
}
