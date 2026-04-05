using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Sentence")]
    [SerializeField] private TextMeshProUGUI sentenceText;

    [Header("Drop Zone")]
    [SerializeField] private DropZone dropZone;

    [Header("Word Tray")]
    [SerializeField] private Transform wordTray;
    [SerializeField] private GameObject wordCardPrefab;

    [Header("Feedback")]
    [SerializeField] private GameObject feedbackPanel;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("End Screen")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private List<GameObject> _spawnedCards = new List<GameObject>();
    private int _score = 0;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void DisplayQuestion(HomophoneQuestion q)
    {
        sentenceText.text = q.sentence.Replace("_", "<color=#FFD700><u>_____</u></color>");

        foreach (var c in _spawnedCards) Destroy(c);
        _spawnedCards.Clear();

        float cardWidth = 300f;
        float spacing = 20f;
        float totalWidth = (cardWidth + spacing) * q.allChoices.Length - spacing;
        float startX = -totalWidth / 2f + cardWidth / 2f;

        for (int i = 0; i < q.allChoices.Length; i++)
        {
            GameObject card = Instantiate(wordCardPrefab, wordTray);
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardRect.anchorMin = new Vector2(0.5f, 0.5f);
            cardRect.anchorMax = new Vector2(0.5f, 0.5f);
            cardRect.pivot = new Vector2(0.5f, 0.5f);
            cardRect.sizeDelta = new Vector2(cardWidth, 100f);
            cardRect.anchoredPosition = new Vector2(startX + i * (cardWidth + spacing), 0f);
            card.GetComponent<DraggableWord>().Init(q.allChoices[i]);
            _spawnedCards.Add(card);
        }

        dropZone.Reset();
        feedbackPanel.SetActive(false);
        UpdateHUD();
    }

    public void ShowFeedback(bool correct, int points)
    {
        if (correct) _score += points;
        feedbackPanel.SetActive(true);
        feedbackText.text = correct
            ? (points == 10 ? "Perfect! +10" : "Correct! +5")
            : "Try again!";
        feedbackText.color = correct ? Color.green : Color.red;
        UpdateHUD();

        if (correct)
            StartCoroutine(AdvanceAfterDelay(1.5f));
    }

    IEnumerator AdvanceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackPanel.SetActive(false);
        GameManager.Instance.LoadNextQuestion();
    }

    void UpdateHUD()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {_score}";
    }

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
        if (finalScoreText != null)
            finalScoreText.text = $"You scored {_score} points!";
    }
    
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void tryAgain()
    {
        SceneManager.LoadScene("homophonesScene");
    }
}