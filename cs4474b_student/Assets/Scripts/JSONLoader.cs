using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ProofreadGameEntry
{
    public string answerText;
    public int errorCount;
}

[System.Serializable]
public class TranscriptionGameEntry
{
    public string word;
    public string audioData;
}

[System.Serializable]
public class HomophoneGameEntry
{
    public List<string> words;
}

[System.Serializable]
public class GameInputData
{
    public List<ProofreadGameEntry> proofreadGames;
    public List<TranscriptionGameEntry> transcriptionGames;
    public List<HomophoneGameEntry> homophoneGames;
    public List<string> prioritizedWords;
}

public class JSONLoader : MonoBehaviour
{
    [SerializeField] private string jsonFileName = "input.json";

    public List<HomophoneQuestion> LoadHomophoneQuestions()
    {
        string path = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (!File.Exists(path))
        {
            Debug.LogError($"input.json not found at: {path}");
            return new List<HomophoneQuestion>();
        }

        string json = File.ReadAllText(path);
        GameInputData data = JsonUtility.FromJson<GameInputData>(json);

        if (data == null || data.homophoneGames == null)
        {
            Debug.LogError("Failed to parse homophoneGames from input.json");
            return new List<HomophoneQuestion>();
        }

        List<HomophoneQuestion> questions = new List<HomophoneQuestion>();

        foreach (var entry in data.homophoneGames)
        {
            if (entry.words == null || entry.words.Count == 0)
            {
                Debug.LogWarning("Empty words list in a homophoneGames entry — skipping.");
                continue;
            }

            string correct = entry.words[0];

            if (!SentenceDictionary.sentences.ContainsKey(correct))
            {
                Debug.LogWarning($"No sentence found for: '{correct}' — skipping.");
                continue;
            }

            List<string> shuffled = entry.words.ToList();
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
            }

            HomophoneQuestion q = new HomophoneQuestion();
            q.correctAnswer = correct;
            q.allChoices = shuffled.ToArray();
            q.sentence = SentenceDictionary.sentences[correct];
            questions.Add(q);
        }

        return questions;
    }
}