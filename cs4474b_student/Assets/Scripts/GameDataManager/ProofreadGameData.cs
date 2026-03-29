using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ProofreadData : MonoBehaviour
{
    public string answerText;
    public int errorCount;

    public ProofreadData(string answerText, int errorCount)
    {
        this.answerText = answerText;
        this.errorCount = errorCount;
    }

    public override string ToString()
    {
        return $"Text: {answerText}; " +
            $"Errors: {errorCount}";
    }

    /*
    // work in progress; may be unnecessary 
    public List<int> answers;

    // generates new question answer set.
    public void GenerateNewGaps(int gapCount)
    {
        // if the string is invalid
        if (answerText.Length <= gapCount || gapCount == 0)
        {
            return;
        }

        System.Random rand = new();

        int i = 0;
        while (i < gapCount)
        {
            int num = rand.Next(0, answers.Count);
            if (Char.IsLetter(answerText[num]) && !answers.Contains(num))
            {
                answers.Add(num);
                i++;
            } 
        }
    }

     
     */
}
