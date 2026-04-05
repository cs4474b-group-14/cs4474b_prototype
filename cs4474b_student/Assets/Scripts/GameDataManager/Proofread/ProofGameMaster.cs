using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public GameObject resultsScreen;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI missedText;
    public TextMeshProUGUI wrongText;

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
        if (index >= current_words.Count || index >= given_words.Count || index >= correct_words.Count)
        {
            Debug.LogWarning($"CalcResults: index {index} out of range.");
            return;
        }

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
                {   // student incorrectly identified a word as having a spelling mistake
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

    public void PrintResults()
    {
        identified_and_correct = new List<int>();
        identified_but_incorrect = new List<int>();
        misidentified = new List<int>();
        failed_to_indentify = new List<int>();

        RequestData();

        for (int i = 0; i < given_words.Count; i++)
            CalcResults(i);

        resultsScreen.SetActive(true);

        correctText.text = "Corrected:\n" + (identified_and_correct.Count == 0 ? "None" :
            string.Join("\n", identified_and_correct.Select(i =>
                $"{given_words[i]} -> {current_words[i]}")));

        missedText.text = "Missed:\n" + (failed_to_indentify.Count == 0 ? "None" :
            string.Join("\n", failed_to_indentify.Select(i =>
                $"{given_words[i]} (correct: {correct_words[i]})")));

        wrongText.text = "Wrongly changed:\n" +
                         (misidentified.Count == 0 && identified_but_incorrect.Count == 0 ? "None" :
                             string.Join("\n",
                                 misidentified.Select(i => $"{given_words[i]} -> {current_words[i]} (was already correct)")
                                     .Concat(identified_but_incorrect.Select(i =>
                                         $"{given_words[i]} -> {current_words[i]} (correct: {correct_words[i]})"))));
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
        // Setup(); // first time setup
    } 

    public void Setup()
    {
        given_words = GetWords(provided_text);
        correct_words = GetWords(correct_text);
        current_words = new List<string>(given_words); // initialize current_words

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
            word.transform.SetParent(board.transform, false); // SetParent before Setup
            ProofWord word_script = word.GetComponent<ProofWord>();
            word_script.Setup(given_words[i], given_words[i], this, i);

            // add punctuation after word if applicable
            if(lastChar == ',')
                {   // add comma after word
                    
                }
            else if (lastChar == '.') 
                {   // add period after word

                }
        }
    }

    public void tryAgain()
    {
        SceneManager.LoadScene("Proofread");
    }
    
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}