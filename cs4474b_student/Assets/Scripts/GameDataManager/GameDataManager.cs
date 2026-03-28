using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

[System.Serializable]
public class GameDataManager
{
    // singleton logic
    private static GameDataManager instance = new();
    public static GameDataManager Instance => instance;

    public List<ProofreadData> proofreadGames = new();
    public List<TranscriptionData> transcriptionGames = new();
    public List<HomophonesData> homophoneGames = new();

    private GameDataManager() 
    { 

    }

    // loads game from a provided path
    public bool load_game_data(string path)
    {

        try
        {
            string jsonString = File.ReadAllText(path);
            if (string.IsNullOrEmpty(jsonString)) return false;
            JsonUtility.FromJsonOverwrite(jsonString, instance);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON Parsing Error: {e.Message}");
            return false;
        }

    }

    // saves current instance of game data; returns path to which it is saved
    public string save_game_data()
    {
        string path = null;
        try
        {
            string json = JsonUtility.ToJson(instance, true);
            path = Path.Combine(Application.persistentDataPath, "new save.json");
            File.WriteAllText(path, json);
            Debug.Log($"Data saved successfully to: {path}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save data: {e.Message}");
        }

        return path;
    }

    // Deletes current singleton instance; for debugging.
    public static void purge_instance()
    {
        instance = new();
    }

}
