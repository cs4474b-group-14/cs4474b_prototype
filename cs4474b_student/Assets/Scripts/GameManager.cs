using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private JSONLoader _loader;
    private List<HomophoneQuestion> _questions;
    private int _index = 0;
    private int _attempts = 0;

    public HomophoneQuestion CurrentQuestion { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        _loader = GetComponent<JSONLoader>();
    }

    void Start()
    {
        _questions = _loader.LoadHomophoneQuestions();
        Debug.Log($"Loaded {_questions.Count} questions");
        LoadNextQuestion();
    }

    public void LoadNextQuestion()
    {
        if (_index >= _questions.Count)
        {
            UIManager.Instance.ShowEndScreen();
            return;
        }
        _attempts = 0;
        CurrentQuestion = _questions[_index++];
        UIManager.Instance.DisplayQuestion(CurrentQuestion);
    }

    public bool CheckAnswer(string droppedWord)
    {
        _attempts++;
        bool correct = droppedWord.Trim().ToLower() ==
                       CurrentQuestion.correctAnswer.Trim().ToLower();

        if (correct)
        {
            int points = _attempts == 1 ? 10 : 5;
            UIManager.Instance.ShowFeedback(true, points);
        }
        else
        {
            UIManager.Instance.ShowFeedback(false, 0);
        }

        return correct;
    }
}