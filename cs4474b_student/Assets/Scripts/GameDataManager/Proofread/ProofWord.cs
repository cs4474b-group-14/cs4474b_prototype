using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProofWord : MonoBehaviour
{
    private ProofGameMaster GAME_MASTER;
    public int MY_INDEX; // was static int
    public ProofUndo UNDO_HISTORY;
    public Button button;
    public TMP_Text button_text;
    public TMP_InputField input_field;
    public string initial_spelling; //initial provided spelling
    public string current_spelling; //initialized to initial_spelling, may be changed by student
    public float char_size; // size of each character in the word, assumes monospacing
    public TMP_Text original_reference;

    void Awake()
    {
        ProofGameMaster.GetData += GiveData;
    }

    void OnDestroy()
    {
        ProofGameMaster.GetData -= GiveData;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setup("initial", "current");
    }

    public void Setup(string init_spelling, string curr_spelling, ProofGameMaster master, int index)
    {
        GAME_MASTER = master;
        MY_INDEX = index;
        UNDO_HISTORY = FindObjectOfType<ProofUndo>(); // idk if this will work lol

        initial_spelling = init_spelling;
        current_spelling = curr_spelling;

        //setup button and input text
        button_text.text = initial_spelling;
        input_field.text = current_spelling;

        //dynamically scale size of button to number of characters
        Dynamic_Resize();

        //hide input field
        input_field.gameObject.SetActive(false);

        //setup original text reference
        original_reference.text = initial_spelling;
        original_reference.gameObject.SetActive(false);
    }

    public void BeginEdit()
    {
        //reveal input field
        input_field.gameObject.SetActive(true);
        input_field.ActivateInputField();
        original_reference.gameObject.SetActive(true);
    }

    public void ConfirmEdit()
    {
        string prev_spelling = current_spelling;
        current_spelling = input_field.text;
        button_text.text = current_spelling;

        if(current_spelling != initial_spelling)
        {
            button_text.color = new Color(1f, 0.5f, 0f);
        }
        
        // KNOWN BUG: idk why but whenever you leave the box after removing full string it dosent revert
        print(current_spelling.Length); //DEBUG
        if(current_spelling == "")
        {
            current_spelling = initial_spelling;
            input_field.text = current_spelling;
            button_text.text = current_spelling;
            button_text.color = new Color(0f, 0f, 0f, 1f);
        }
        print(current_spelling);

        //dynamically scale size of button to number of characters
        Dynamic_Resize();

        //hide input field
        input_field.gameObject.SetActive(false);
        //hide original text
        original_reference.gameObject.SetActive(false);
        UNDO_HISTORY.RecordEdit(gameObject.GetComponent<ProofWord>(), current_spelling, prev_spelling);
    }

    public void GiveData()
    {
        GAME_MASTER.RecieveData(MY_INDEX, current_spelling);
    }

    void Dynamic_Resize()
    {
        float preferredWidth = button_text.preferredWidth + 10f; // +10 for a little padding
        float height = button.GetComponent<RectTransform>().rect.height;

        button.GetComponent<RectTransform>().sizeDelta = new Vector2(preferredWidth, height);
        input_field.GetComponent<RectTransform>().sizeDelta = new Vector2(preferredWidth, height);
    }

    public void Remote_Update(string updated)
    {
        current_spelling = updated;
        input_field.gameObject.SetActive(true);
        input_field.text = current_spelling;
        button_text.text = current_spelling;
        if(current_spelling == initial_spelling)
        {
            button_text.color = new Color(0f, 0f, 0f, 1f);
        }
        else
        {
            button_text.color = new Color(1f, 0.5f, 0f);
        }
        input_field.gameObject.SetActive(false);

        Dynamic_Resize();
    }
}