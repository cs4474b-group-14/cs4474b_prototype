using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameDataManager
{
    // private lists of minigame data objects

    private static readonly GameDataManager instance = new GameDataManager();

    private GameDataManager() { }

    public static GameDataManager Instance
    {
        get { return instance; }
    }
}
