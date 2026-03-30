using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProofWord : MonoBehaviour
{
    private ProofGameMaster GAME_MASTER;
    public static int MY_INDEX;
    public Button button;
    public TMP_Text button_text;
    public TMP_InputField input_field;
    public string initial_spelling; //initial provided spelling
    public string current_spelling; //initialized to initial_spelling, may be changed by student

    // Start is called before the first frame update
    void Start()
    {
        GAME_MASTER = GetComponentInParent<ProofGameMaster>();
        ProofGameMaster.GetData += GiveData;

        //Setup("initial", "current");
    }
    public void Setup(string init_spelling, string curr_spelling)
    {
        initial_spelling = init_spelling;
        current_spelling = curr_spelling;

        //setup button and input text
        button_text.text = initial_spelling;
        input_field.text = current_spelling;

        //hide input field
        input_field.gameObject.SetActive(false);
    }

    public void BeginEdit()
    {
        //reveal input field
        input_field.gameObject.SetActive(true);
    }

    public void ConfirmEdit()
    {
        current_spelling = input_field.text;
        button_text.text = current_spelling;

        if(current_spelling != initial_spelling)
        {
            button_text.color = new Color(1f, 0.5f, 0f);
        }
        
        // KNOWN BUG: idk why but whenever you leave the box after removing full string it dosent revert
        print(current_spelling.Length); //DEBUG
        if(current_spelling.Length < 1)
        {
            current_spelling = initial_spelling;
            button_text.color = new Color(0f, 0f, 0f, 1f);
        }
        print(current_spelling);

        //hide input field
        input_field.gameObject.SetActive(false);
    }

    public void GiveData()
    {
        GAME_MASTER.RecieveData(MY_INDEX, current_spelling);
    }
}
