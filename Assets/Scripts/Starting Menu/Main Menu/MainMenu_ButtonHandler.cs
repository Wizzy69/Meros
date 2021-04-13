using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_ButtonHandler : MonoBehaviour
{
	public AudioManager audioManager;

	public Canvas settingCanvas;

	private void Awake()
	{
		loadGameSettings();
	}

	private void loadGameSettings()
	{
		if (Directory.Exists(@".\GameData"))
		{
			if (File.Exists(@".\GameData\Video.sk"))
			{
				SystemVariables.videoSettings = SaveDataScript.LoadVideoSettings();
			}
			else
			{
				SystemVariables.videoSettings = new VideoSettings{
					qualityIndex    = 2,
					resolutionIndex = Screen.resolutions.Length - 1,
					screenSizeIndex = 1
				};

				SaveDataScript.SaveVideoSettings();
			}

			Screen.SetResolution(Screen.resolutions[SystemVariables.videoSettings.resolutionIndex].width,
				Screen.resolutions[SystemVariables.videoSettings.resolutionIndex].height,
				(FullScreenMode) SystemVariables.videoSettings.screenSizeIndex);

			QualitySettings.SetQualityLevel(SystemVariables.videoSettings.qualityIndex);

			if (File.Exists(@".\GameData\Audio.sk"))
			{
				SystemVariables.audioSettings = SaveDataScript.LoadAudioSettings();

				Debug.LogWarning("Imported audio : "      + SystemVariables.audioSettings.masterVolume +
				                 "\nImported audio SFX: " + SystemVariables.audioSettings.soundEffectVolume);
			}
			else
			{
				SystemVariables.audioSettings = new AudioSettings{
					masterVolume = .5f
				};
				SaveDataScript.SaveAudioSettings();
			}
		}
		else
		{
			Directory.CreateDirectory(@".\GameData");
			loadGameSettings();
		}
	}

	public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void SettingsMenu()
	{
		settingCanvas.gameObject.SetActive(true);
		gameObject.SetActive(false);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
