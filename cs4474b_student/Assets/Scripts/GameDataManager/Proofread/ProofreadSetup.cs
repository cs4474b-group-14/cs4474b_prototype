using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProofreadSetup : MonoBehaviour
{
    [SerializeField] private JSONLoader jsonLoader;
    [SerializeField] private ProofGameMaster gameMaster;

    private List<string> _prioritizedWords;

    private static readonly Dictionary<string, string[]> ErrorSubstitutions = new Dictionary<string, string[]>
    {
        { "a", new[] { "e", "o" } },
        { "e", new[] { "a", "i" } },
        { "i", new[] { "e", "y" } },
        { "o", new[] { "u", "a" } },
        { "u", new[] { "o", "a" } },
        { "th", new[] { "t", "d" } },
        { "ing", new[] { "in", "ign" } },
        { "tion", new[] { "sion", "shun" } },
        { "ed", new[] { "id", "d" } },
        { "ly", new[] { "ley", "li" } },
    };

    void Start()
    {
        SetupProofreadGame();
    }

    public void SetupProofreadGame()
    {
        var entries = jsonLoader.LoadProofreadEntries();
        _prioritizedWords = jsonLoader.LoadPrioritizedWords();

        if (entries == null || entries.Count == 0)
        {
            Debug.LogError("No proofread entries found.");
            return;
        }

        var entry = entries[0];
        string correct = entry.answerText;
        int errorCount = entry.errorCount;

        string corrupted = IntroduceErrors(correct, errorCount, _prioritizedWords);

        gameMaster.provided_text = corrupted;
        gameMaster.correct_text = correct;
        gameMaster.Setup();
    }

    private string IntroduceErrors(string correct, int errorCount, List<string> prioritized)
    {
        List<string> words = correct.Split(' ').ToList();
        int errorsLeft = errorCount;

        List<int> prioritizedIndices = new List<int>();
        List<int> otherIndices = new List<int>();

        for (int i = 0; i < words.Count; i++)
        {
            string clean = StripPunctuation(words[i]).ToLower();
            if (prioritized != null && prioritized.Any(p => p.ToLower() == clean))
                prioritizedIndices.Add(i);
            else
                otherIndices.Add(i);
        }

        prioritizedIndices = prioritizedIndices.OrderBy(_ => Random.value).ToList();
        otherIndices = otherIndices.OrderBy(_ => Random.value).ToList();

        List<int> orderedTargets = prioritizedIndices.Concat(otherIndices).ToList();
        HashSet<int> erroredIndices = new HashSet<int>();

        foreach (int i in orderedTargets)
        {
            if (errorsLeft <= 0) break;

            string word = words[i];
            string corrupted = CorruptWord(word);

            if (corrupted != word)
            {
                words[i] = corrupted;
                erroredIndices.Add(i);
                errorsLeft--;
            }
        }

        if (errorsLeft > 0)
            Debug.LogWarning($"Could only introduce {errorCount - errorsLeft}/{errorCount} errors — not enough corruptible words.");

        return string.Join(" ", words);
    }

    private string CorruptWord(string word)
    {
        if (word.Length < 2) return word;

        string stripped = StripPunctuation(word);
        string punctuation = word.Length > stripped.Length ? word[word.Length - 1].ToString() : "";

        foreach (var pair in ErrorSubstitutions)
        {
            int index = stripped.ToLower().IndexOf(pair.Key);
            if (index >= 0)
            {
                string replacement = pair.Value[Random.Range(0, pair.Value.Length)];
                string corrupted = stripped.Substring(0, index) + replacement + stripped.Substring(index + pair.Key.Length);
                if (corrupted.ToLower() != stripped.ToLower())
                    return corrupted + punctuation;
            }
        }

        int swapIndex = Random.Range(0, stripped.Length - 1);
        char[] chars = stripped.ToCharArray();
        (chars[swapIndex], chars[swapIndex + 1]) = (chars[swapIndex + 1], chars[swapIndex]);
        return new string(chars) + punctuation;
    }

    private string StripPunctuation(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        char last = word[word.Length - 1];
        if (last == ',' || last == '.' || last == '!' || last == '?')
            return word.Substring(0, word.Length - 1);
        return word;
    }
}