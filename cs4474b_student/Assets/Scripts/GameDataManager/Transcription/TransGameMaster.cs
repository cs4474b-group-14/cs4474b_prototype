using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class TransGameMaster : MonoBehaviour
{
    public string word;
    public AudioClip audio;
    public AudioSource audioSource;
    public TMP_InputField inputField;

    public GameObject play_button;
    public GameObject results_correct;
    public GameObject results_incorrect;
    public TMP_Text results_correction;

    public GameObject submitButton;
    public GameObject mainMenuButton;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        inputField = FindObjectOfType<TMP_InputField>();

        audioSource.clip = audio;
    }
    public void Submit()
    {
        //dont allow submit without text
        if(inputField.text == "")
        {
            return;
        }

        inputField.enabled = false; //dont allow editing after submit
        inputField.textComponent.color = new Color(1f,1f,1f);

        if(inputField.text.ToLower() == word.ToLower())
        {
            inputField.image.color = new Color(0.4f,0.8f,0.4f);
            play_button.SetActive(false);
            // correct 
            results_correct.SetActive(true);
        }
        else
        {
            inputField.image.color = new Color(1f,0.4f,0.4f);
            play_button.SetActive(false);
            // incorrect
            results_incorrect.SetActive(true);
            results_correction.text = word;
        }

        submitButton.SetActive(false);
        mainMenuButton.SetActive(true);
    }

    public void mainMenu()
    {
        GameResult.proofreadComplete = true;
        SceneManager.LoadScene("Dashboard");
    }
}
