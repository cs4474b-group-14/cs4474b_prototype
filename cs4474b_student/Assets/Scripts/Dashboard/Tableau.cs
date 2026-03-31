using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau : MonoBehaviour
{
    [SerializeField]
    GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var game in GameDataManager.Instance.proofreadGames)
        {
            // create task items for each game + add as child to content field 

        }

        i = 0;
        foreach (var game in GameDataManager.Instance.homophoneGames)
        {
            // create task items for each game + add as child to content field 
        }

        i = 0;
        foreach (var game in GameDataManager.Instance.transcriptionGames)
        {
            // create task items for each game + add as child to content field 
        }
    }

}
