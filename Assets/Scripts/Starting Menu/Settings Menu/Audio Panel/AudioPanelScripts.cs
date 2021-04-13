using UnityEngine;
using UnityEngine.UI;

public class AudioPanelScripts : MonoBehaviour
{
	public Slider       masterVolume;
	public Slider       soundeffectVolume;
	public AudioManager audioManager;

	private void Start()
	{
		masterVolume.value      = SystemVariables.audioSettings.masterVolume;
		soundeffectVolume.value = SystemVariables.audioSettings.soundEffectVolume;
	}

	public void SFXSetVolume(float newVolume)
	{
		SystemVariables.audioSettings.soundEffectVolume = newVolume;
	}

	public void SetVolume(float newVolume)
	{
		audioManager.playingSource.volume          = newVolume;
		SystemVariables.audioSettings.masterVolume = newVolume;
	}

	public void SaveAudioSettings()
	{
		SystemVariables.audioSettings.masterVolume = masterVolume.value;
		SaveDataScript.SaveAudioSettings();
	}
}
