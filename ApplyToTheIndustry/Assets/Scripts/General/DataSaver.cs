using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* CLASS: DataSaver
 * Used for storing player and other
 * game data into JSON files
 */
public class DataSaver : MonoBehaviour
{
    // Hash ID used for indicating what file to save to
    string hashID;

    // List of all json strings to save
    List<string> dataSaves;

    /// <summary>
    /// Set the hash ID
    /// </summary>
    private void Awake()
    {
        hashID = GenerateHash();
        dataSaves = new List<string>();
    }

    /// <summary>
    /// Saves incoming data to a JSON file
    /// </summary>
    public void SaveData(SaveData data)
    {
        // Convert data into json string and push to list
        string jsonData = JsonUtility.ToJson(data, true);
        dataSaves.Add(jsonData);

        // Get file path and push all content to json file
        string filePath = Application.dataPath + "/Data/" + hashID + ".json";

        // Get the file directory
        string dirPath = System.IO.Path.GetDirectoryName(filePath);

        // Create the file if it doesn't exist
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        try
        {
            System.IO.File.WriteAllLines(filePath, dataSaves);
        } catch(System.IO.IOException ex)
        {
            Logger.Log(ex.Message);
        }
        
    }

    /// <summary>
    /// Generates a hash ID used for identifying files
    /// </summary>
    /// <returns>string ID from the generated hash</returns>
    public string GenerateHash()
    {
        Guid test = Guid.NewGuid();
        return test.ToString();
    }
}
