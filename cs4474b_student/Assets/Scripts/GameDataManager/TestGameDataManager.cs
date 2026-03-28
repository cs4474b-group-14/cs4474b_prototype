using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TestGameDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        test_saves();
    }

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
        print_data_manager();

        Debug.Log("Saving Game Data:");
        string path = GameDataManager.Instance.save_game_data();

        Debug.Log("Purging Game Data");
        GameDataManager.purge_instance();
        print_data_manager();

        Debug.Log("Game Data Post Reload:");
        GameDataManager.Instance.load_game_data(path);
        print_data_manager();
    }


    private void print_data_manager()
    {
        Debug.Log("Printing GameDataManager Contents:");
        if (GameDataManager.Instance == null)
        {
            Debug.Log("Instance is empty");
            return;
        }

        if (GameDataManager.Instance.proofreadGames != null && GameDataManager.Instance.proofreadGames.Count > 0)
        {
            string toPrint = "";
            foreach (var game in GameDataManager.Instance.proofreadGames)
            {
                toPrint += game.ToString() + '\n';
            }
            Debug.Log($"Proofreading Items: \n" +
                $"{toPrint}");
        }


        if (GameDataManager.Instance.homophoneGames != null && GameDataManager.Instance.homophoneGames.Count > 0)
        {
            string toPrint = "";
            foreach (var game in GameDataManager.Instance.homophoneGames)
            {        
                toPrint += game.ToString() + '\n';
            }
            Debug.Log($"Homophone Items: \n" +
                $"{toPrint}");
        }

        if (GameDataManager.Instance.transcriptionGames != null && GameDataManager.Instance.transcriptionGames.Count > 0)
        {

        }

    }

}
