using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Events;

public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    private bool useEncryption = false;

    /*
    private bool newPuzzleLevelStarted = false;
    private bool restartInvoked = false;
    */

    private readonly string encryptionCodeWord = "caesar";
    private readonly string levelReStartExtension = ".restar";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load(string profileId, bool restartInvoked)
    {
        // Base case - if the profileId is null, return right away
        if (profileId == null)
        {
            return null;
        }

        // Use Path.Combine to account for different OS'S having different path separators
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            if (restartInvoked)
            {
                AttemptRestartLevel(fullPath);
            }

            try
            {
                // Load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Optionally decrypt the data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // Deserialize the data from JSON back into the C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }

        }
        return loadedData;

    }

    public void Save(GameData data, string profileId, bool newPuzzleLevelStarted)
    {
        // Base case - if the profileId is null, return right away
        if (profileId == null)
        {
            return;
        }
        
        // Use Path.Combine to account for different OS'S having different path separators
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        string levelStartFilePath = fullPath + levelReStartExtension;
        try
        {
            // Create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize the C# game data object into JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            // Optionally encrypt the data
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // Write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            // See if we need to have a copy for restarting a level
            if (newPuzzleLevelStarted)
            {
                File.Copy(fullPath, levelStartFilePath, true);
                Debug.Log("Making restart file!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }

        // FOR DEVELOPING PURPOSES, DISABLE WHEN BUILDING FINAL GAME
        //GUIUtility.systemCopyBuffer = fullPath;
        DataPersistenceManager.instance.SetNewLevel(false);
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // Loop over all directory names in the directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            // Defensive programming - Check if the data file exists
            // If it doesn't, then this folder isn't a profile and should be skipped
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if(!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles beacuse it does not contain data: " + profileId);
                continue;
            }

            // Load the game data for this profile and put it in the dictionary
            // REMOVE FALSE IF RESTART NOT WORKING!!!
            GameData profileData = Load(profileId, false);
            // Defensive programming - Ensure the profile data isn't null
            //Because if it is the somthing went wrong and we should let ourselves know
            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();

        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            // Skip this entry if the gamedata is null
            if (gameData == null)
            {
                continue;
            }

            // If this is the first data we've come across that exists, it's the most recent so far
            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            // Otherwise, compare to see which date is the most recent
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                // The greatest DateTime value is the most recent
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }

        }
        return mostRecentProfileId;
    }

    // This method is a simple implementation of XOR encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }

    private bool AttemptRestartLevel (string fullPath)
    {
        bool success = false;

        string restartFilePath = fullPath + levelReStartExtension;
        try
        {
            // If the file exists, attempt to roll back to it by overwriting the original file
            if (File.Exists(restartFilePath))
            {
                File.Copy(restartFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Restarted level, replaced savefile with level restart file.");
            }
            // Otherwise we don't have levelStartFile, so something hasn't worked
            else
            {
                throw new Exception("Tried to load level restart file, but it doesn't exist.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to load level restart file at: " 
                + restartFilePath + "\n" + e);
        }

        return success;
    }

}
