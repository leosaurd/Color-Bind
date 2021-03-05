using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

	public static SaveManager singleton;

	// These are the paths to the save files, this is in Appdata/roaming/Colorblind
	// TODO Work out how this will work with other platforms
	private readonly string saveDir = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Colorblind\";
	readonly public string savePathTemplate = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Colorblind\{0}.json";
	public string currPath;
	public string currentFileGUID;

	public SaveData saveData;

	void Awake()
	{
		// Assign singleton
		if (SaveManager.singleton == null)
			singleton = this;
		else
			Destroy(gameObject);


		// Create the save directory if it doesn't exist
		if (!File.Exists(saveDir))
			Directory.CreateDirectory(saveDir);
	}

	public void CreateNewSave(string saveName)
	{
		if (saveData != null)
			Save();

		// Save file names are GUIDs to avoid collisions
		currentFileGUID = Guid.NewGuid().ToString();
		saveData = new SaveData(saveName);
		Save();
	}

	public void Load(string fileToLoad)
	{
		// If the save slot they are changing to exists
		if (File.Exists(String.Format(savePathTemplate, fileToLoad)))
		{
			currPath = String.Format(savePathTemplate, fileToLoad);
			// Convert the file's data into a string
			string json = File.ReadAllText(currPath);
			// Parse the string into PlayerSaveData format and store it in playerData
			saveData = JsonUtility.FromJson<SaveData>(json);

			// If their save file is out of date delete it to avoid errors
			// TODO try to implement save conversion system
			if (saveData.version != new SaveData("").version)
			{
				string name = saveData.saveName;
				DeleteSaveFile(fileToLoad);
				CreateNewSave(name);
			}
		}
	}

	public void DeleteSaveFile(string fileToDel)
	{
		// If the save slot they are deleting exists
		if (File.Exists(String.Format(savePathTemplate, fileToDel)))
		{
			saveData = null;
			File.Delete(String.Format(savePathTemplate, fileToDel));
		}
	}

	public void Save()
	{
		// Create a string that contains json text made from the contents of playerData
		string json = JsonUtility.ToJson(saveData, true);
		// Create a new .json file
		StreamWriter sw = File.CreateText(currPath);
		sw.Close();
		// Write the json text to the file
		File.WriteAllText(currPath, json);
	}

	private void OnApplicationQuit()
	{
		if (saveData != null)
			Save();
	}

	// Class to format our save data, this essentially holds all out save data
	[System.Serializable]
	public class SaveData
	{
		public SaveData(string _saveName)
		{
			saveName = _saveName;
		}

		public readonly string version = "1.0";

		// Used to identify the save to the user
		public string saveName;

		public Settings settings;

		[System.Serializable]
		public struct Settings
		{
			public float sfxVolume;
			public float musicVolume;
			// TODO Implement resolution and window mode
		}

	}
}
