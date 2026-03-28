using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[System.Serializable]
public class WordPair 
{
    public string word1, word2;
    public WordPair(string word1, string word2) 
    { 
        this.word1 = word1;
        this.word2 = word2;
    }

    public override string ToString()
    {
        return $"({word1}, {word2})";
    }
}
