using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
	public Sound[]    sounds;
	public Slider     MasterVolumeSlider;
	public GameRegion gameRegion;

	[HideInInspector]
	public AudioSource playingSource;

	private bool allowSound;

	public void Awake()
	{
		playingSource = null;
		foreach (Sound sound in sounds)
		{
			var sr = gameObject.AddComponent<AudioSource>();
			sr.clip      = sound.audioClip;
			sr.volume    = SystemVariables.audioSettings.masterVolume;
			sr.loop      = sound.loop;
			sound.source = sr;
		}
	}

	private void Start()
	{
		allowSound = true;
		StartCoroutine(PlaySoundLoop());
	}

	private IEnumerator PlaySoundLoop()
	{
		while (allowSound)
			foreach (Sound s in sounds)
			{
				PlaySound(s);
				yield return new WaitForSeconds(s.audioClip.length);
			}
	}


	public void PlaySound(Sound s)
	{
		if (s        == null) return;
		if (s.source == playingSource) return;
		playingSource = s.source;
		s.source.Play();
	}
}
