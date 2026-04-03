using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau : MonoBehaviour
{
    [SerializeField] GameObject contentField;
    [SerializeField] GameObject taskItemPrefab; 

    // Start is called before the first frame update
    void Start()
    {
        test_saves();



        int i = 1;
        foreach (var game in GameDataManager.Instance.proofreadGames)
        {
            // create task items for each game + add as child to content field 
            var newItem = Instantiate(taskItemPrefab);
            newItem.GetComponent<TaskItem>().set_game_type(TaskItem.GameType.proofread, i++);
            newItem.transform.SetParent(contentField.transform, false);
        }

        foreach (var game in GameDataManager.Instance.homophoneGames)
        {
            // create task items for each game + add as child to content field 
            var newItem = Instantiate(taskItemPrefab);
            newItem.GetComponent<TaskItem>().set_game_type(TaskItem.GameType.homophones, i++);
            newItem.transform.SetParent(contentField.transform, false);
        }

        foreach (var game in GameDataManager.Instance.transcriptionGames)
        {
            var newItem = Instantiate(taskItemPrefab);
            newItem.GetComponent<TaskItem>().set_game_type(TaskItem.GameType.transcription, i++);
            newItem.transform.SetParent(contentField.transform, false);
        }
    }

    // TODO: Delete this from here; it's just for testing purposes.
    private void test_saves()
    {
        Debug.Log("Populating Game Data.");
        ProofreadData p1 = new("Test number 1", 0);
        ProofreadData p2 = new("Test number 2", 34);
        ProofreadData p3 = new("Test number 3", 7);
        HomophonesData h = new(new List<WordPair> { new WordPair("Test1", "Test2") }, 2);

        GameDataManager.Instance.proofreadGames.Add(p1);
        GameDataManager.Instance.proofreadGames.Add(p2);
        GameDataManager.Instance.proofreadGames.Add(p3);
        GameDataManager.Instance.homophoneGames.Add(h);

        
    }


}
