using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TaskItem : MonoBehaviour
{

    public UnityEvent onSelection;

    public enum GameType
    {
        None,
        proofread,
        transcription,
        homophones
    }

    [SerializeField] int taskIndex = 1;
    [SerializeField] public bool isComplete = false;
    [SerializeField] GameType gameType = GameType.None;
    [SerializeField] TextMeshProUGUI textfield;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] Sprite proofreadImage;
    [SerializeField] Sprite homophonesImage;
    [SerializeField] Sprite transcriptionImage;

    private string gameName;

    private void Start()
    {
        // check if this game was just completed
        if (gameType == GameType.proofread && GameResult.proofreadComplete)
            isComplete = true;
        else if (gameType == GameType.homophones && GameResult.homophonesComplete)
            isComplete = true;

        OnValidate();
    }

    private void OnValidate()
    {
        switch (gameType)
        {
            case GameType.proofread:
                image.sprite = proofreadImage;
                gameName = "Proofread";
                break;
            case GameType.homophones:
                image.sprite = homophonesImage;
                gameName = "Homophones";
                break;
            case GameType.transcription:
                image.sprite = transcriptionImage;
                gameName = "Transcription";
                break;
            default:
                image.sprite = null;
                gameName = "";
                break;
        }

        if (isComplete)
        {
            textfield.text = $"Task #{taskIndex}: {gameName}\nCOMPLETE";
            GetComponent<UnityEngine.UI.Image>().color = new Color(25 / 255f, 185 / 255f, 25 / 255f, 1f);
        }
        else 
        {
            textfield.text = $"Task #{taskIndex}: {gameName}\nINCOMPLETE";
        }
    }

    public void set_game_type(GameType gt, int index)
    {
        gameType = gt;
        taskIndex = index;
        OnValidate();
    }

    public void on_click()
    {
        switch (gameType) 
        {
            case GameType.proofread:
                Tableau.hasWonProofread = true;
                SceneManager.LoadScene("Proofread");
                break;
            case GameType.homophones:
                Tableau.hasWonHomophones = true;
                SceneManager.LoadScene("homophonesScene");
                break;
            case GameType.transcription:
                SceneManager.LoadScene("Transcription");
                break;
            default:
                Debug.Log("wtf type of game is this?");
                break;
        }
    }
}
