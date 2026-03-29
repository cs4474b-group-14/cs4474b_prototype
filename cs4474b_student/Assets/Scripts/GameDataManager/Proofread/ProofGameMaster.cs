using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProofGameMaster : MonoBehaviour
{
    public string answerText;
    public int errorCount;
    public string paragraph;
    public string output_lol = "";

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
        //start of testing
        paragraph  = "hello, thes is an exemple paragraph\n etc.";
        answerText = "hello, this is an example paragraph\n etc.";
        //end of testing 

        List<string> given_words = GetWords(paragraph);
        PrintWords("given words", given_words);

        List<string> correct_words = GetWords(answerText);
        PrintWords("correct words", correct_words);


    }
}
