using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ProofreadData : MonoBehaviour
{
    public string answerText;
    public int errorCount;
    public string paragraph;
    public string output_lol = "";

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

    List<string> GetWords(String paragraph)
    {
        List<string> words = new List<string>(); //this will include newline characters as their own entries
        List<string> sentences = paragraph.Split('\n').ToList();
        foreach(string sentence in sentences)
        {
            List<string> contents = sentence.Split(' ').ToList();
            foreach(string word in contents)
            {
                words.Add(word);
            } 
            words.Add("\n");
        }
        return words;
    }

    public void PrintWords(string name, List<string> words)
    {
        output_lol += name + ": \n";
        foreach(string word in words)
        {
            output_lol += word + " ";
        }
        output_lol += "\n";
    }

    public void Start()
    {
        paragraph  = "hello, thes is an exemple paragraph\n etc.";
        List<string> given_words = GetWords(paragraph);
        PrintWords("given words", given_words);

        answerText = "hello, this is an example paragraph\n etc.";
        List<string> correct_words = GetWords(answerText);
        PrintWords("correct words", correct_words);

        
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
