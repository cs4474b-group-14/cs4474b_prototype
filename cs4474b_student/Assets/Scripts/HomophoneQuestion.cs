using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Homophone Game/Question")]
[System.Serializable]
public class HomophoneQuestion
{
    public string sentence;
    public string correctAnswer;
    public string[] allChoices;
}