using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProofGameMaster : MonoBehaviour
{
    // DEBUG
    public string output_lol = "";

    // PROVIDED
    public string provided_text; // given provided_text
    public string correct_text; // correct provided_text
    public List<string> given_words;
    public List<string> correct_words;
    public List<string> current_words; // for save purposes

    // MARKING
    public List<int> identified_and_correct; //identified + corrected spelling mistakes
    public List<int> identified_but_incorrect; //identified but not corrected spelling mistakes
    public List<int> misidentified; // misidentified spelling mistakes
    public List<int> failed_to_indentify; // list of incorrectly spelled words in the provided text

    //UTILITY
    public GameObject WORD_PREFAB;
    public static event Action GetData;
    public HorizontalLayoutGroup board;

    public void RequestData()
    {
        GetData.Invoke();   
    }

    public void RecieveData(int index, string current_spelling)
    {
        current_words[index] = current_spelling;
    }

    public void CalcResults(int index)
    {
        string current_spelling = current_words[index];
        string initial_spelling = given_words[index];
        string correct_spelling = correct_words[index];

        if(current_spelling != initial_spelling)
        {   // spelling changed
            if(current_spelling == correct_spelling)
            {   // spelling corrected
                identified_and_correct.Add(index);
            }
            else
            {   // spelling changed incorrectly
                if(initial_spelling == correct_spelling)
                {   // student inccorectly identified a word as having a spelling mistake
                    misidentified.Add(index);
                }
                else
                {   // student correctly identified a spelling mistake, but failed to correct it
                    identified_but_incorrect.Add(index);
                }
            }
        }
        else
        {   // spelling unchanged
            if(current_spelling != correct_spelling)
            {   // student failed to notice a spelling mistake in the provided provided_text
                failed_to_indentify.Add(index);
            }
            else
            {   // student recognized that the word was already spelt correctly
                
            }
        }
    }

    public string PrintResults()
    {
        string results = "";
        // write out results
        
        return results;
    }

    List<string> GetWords(String provided_text)
    {
        List<string> words = new List<string>(); //this will include newline characters as their own entries
        List<string> sentences = provided_text.Split('\n').ToList();
        foreach(string sentence in sentences)
        {
            List<string> contents = sentence.Split(' ').ToList();
            foreach(string word in contents)
            {
                words.Add(word);
            } 
            //words.Add("\n"); // REMOVED: assume no newlines for now
        }
        return words;
    }

    //DEBUG FUNCTION
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
        Setup(); // first time setup
    } 

    public void Setup()
    {
        //OVERRIDES INPUT REMOVE LATER
        provided_text  = "hello, thes is an exemple provided_text\n etc.";
        correct_text = "hello, this is an example provided_text\n etc.";
        //END OF REMOVE LATER

        given_words = GetWords(provided_text);
        correct_words = GetWords(correct_text);
        // DEBUG
        PrintWords("given words", given_words);
        PrintWords("correct words", correct_words);

        // Create all word objects
        for(int i = 0; i < given_words.Count; i++)
        {
            //should not be triggered, print if it is
            if (given_words[i] == "\n" || given_words[i].Length < 1)
            {   //end of line segment, ignore
                continue;
            }
            
            //check if there is punctuation
            int lastIndex = given_words[i].Length - 1;
            char lastChar = given_words[i][lastIndex];
            if((lastChar == ',') || (lastChar == '.'))
            {   //punctuation should match
                given_words[i].Remove(lastIndex);
                correct_words[i].Remove(lastIndex);
                
            }

            GameObject word = Instantiate(WORD_PREFAB);
            ProofWord word_script = word.GetComponent<ProofWord>();
            word_script.Setup(given_words[i], given_words[i]);
            word.transform.SetParent(board.transform);

            // add punctuation after word if applicable
            if(lastChar == ',')
                {   // add comma after word
                    
                }
            else if (lastChar == '.') 
                {   // add period after word

                }
        }
    }
}
