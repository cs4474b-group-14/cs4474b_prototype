using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProofWord : MonoBehaviour
{
    Button button;
    TextMeshPro button_text;
    InputField correction;
    string initial_spelling; //initial provided spelling
    string attempt_spelling; //initialized to initial_spelling, may be changed by student
    string correct_spelling; //correct provided spelling

    // Start is called before the first frame update
    void Start()
    {
        Setup("Placeholder", "Placeholder");
    }
    void Setup(string init_spelling, string corr_spelling)
    {
        attempt_spelling = initial_spelling = init_spelling;
        correct_spelling = corr_spelling;

        button_text.text = initial_spelling;
        
        
    }

    void BeginEditing()
    {
        
    }
}
