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

    private void Start()
    {
    }

    private void OnValidate()
    {
        switch (gameType)
        {
            case GameType.proofread:
                image.sprite = proofreadImage;
                break;
            case GameType.homophones:
                image.sprite = homophonesImage;
                break;
            case GameType.transcription:
                image.sprite = transcriptionImage;
                break;
            default:
                image.sprite = null;
                break;
        }

        if (isComplete)
        {
            textfield.text = $"Task #{taskIndex}: DONE";
        }
        else 
        {
            textfield.text = $"Task #{taskIndex}: TODO";
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
