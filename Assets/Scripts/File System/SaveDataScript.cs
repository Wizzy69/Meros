using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveDataScript
{
	public static void SaveVideoSettings()
	{
		if (SystemVariables.videoSettings == null)
		{
			Debug.LogWarning("Failed to save video. videoSettings are NULL");
			return;
		}

		var Path = @".\GameData\Video.sk";
		if (!Directory.Exists(@".\GameData")) Directory.CreateDirectory(@".\GameData");
		FileStream filestream = File.Open(Path, FileMode.OpenOrCreate);


		var formatter = new BinaryFormatter();

		formatter.Serialize(filestream, SystemVariables.videoSettings);

		filestream.Close();
		Debug.LogWarning($"Saved video - G: {SystemVariables.videoSettings.qualityIndex}" +
		                 $" - S: {SystemVariables.videoSettings.screenSizeIndex}"         +
		                 $" - R : {SystemVariables.videoSettings.resolutionIndex}");
	}

	public static VideoSettings LoadVideoSettings()
	{
		var Path = @".\GameData\Video.sk";
		if (!Directory.Exists(@".\GameData")) Directory.CreateDirectory(@".\GameData");
		if (File.Exists(Path))
		{
			var        vs         = new VideoSettings();
			FileStream filestream = File.Open(Path, FileMode.OpenOrCreate);
			var        formatter  = new BinaryFormatter();

			vs = formatter.Deserialize(filestream) as VideoSettings;
			filestream.Close();
			return vs;
		}

		return null;
	}

	public static void SaveAudioSettings()
	{
		if (SystemVariables.audioSettings == null)
		{
			Debug.LogWarning("Failed to save audio. audioSettings are NULL");
			return;
		}

		var Path = @".\GameData\Audio.sk";
		if (!Directory.Exists(@".\GameData\")) Directory.CreateDirectory(@".\GameData");
		FileStream filestream = File.Open(Path, FileMode.OpenOrCreate);

		var formatter = new BinaryFormatter();

		formatter.Serialize(filestream, SystemVariables.audioSettings);
		filestream.Close();
		Debug.LogWarning("Saved audio MV: " + SystemVariables.audioSettings.masterVolume + "\nSaved audio SFX: " +
		                 SystemVariables.audioSettings.soundEffectVolume);
	}

	public static AudioSettings LoadAudioSettings()
	{
		var Path = @".\GameData\Audio.sk";
		if (!Directory.Exists(@".\GameData\")) Directory.CreateDirectory(@".\GameData");
		if (!File.Exists(Path)) return null;
		AudioSettings asa = null;
		using (FileStream filestream = File.Open(Path, FileMode.OpenOrCreate))
		{
			var formatter = new BinaryFormatter();

			asa = formatter.Deserialize(filestream) as AudioSettings;
		}

		return asa;
	}


	public static void SaveGame()
	{
		if (SystemVariables.playerData == null)
		{
			Debug.LogWarning("Could not save playerData");
			return;
		}

		Directory.CreateDirectory(@".\GameData\Saves");
		var Path = @".\GameData\Saves\gameSave.save";

		using (FileStream stream = File.Open(Path, FileMode.OpenOrCreate))
		{
			var formatter = new BinaryFormatter();
			formatter.Serialize(stream, SystemVariables.playerData);
		}

		Debug.LogWarning("Saved gamedata " + SystemVariables.playerData.X + " " + SystemVariables.playerData.Y + " " +
		                 SystemVariables.playerData.Z);
	}

	public static PlayerData LoadGame()
	{
		var Path = @".\GameData\Saves\gameSave.save";
		if (File.Exists(Path))
		{
			var        data       = new PlayerData();
			FileStream filestream = File.Open(Path, FileMode.OpenOrCreate);

			var formatter = new BinaryFormatter();

			data = formatter.Deserialize(filestream) as PlayerData;

			Debug.LogWarning("Loaded player position: " + data.X + " " + data.Y + " " + data.Z);

			filestream.Close();
			return data;
		}

		return null;
	}
}
