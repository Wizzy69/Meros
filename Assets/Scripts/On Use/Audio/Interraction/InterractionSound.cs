using UnityEngine;
using UnityEngine.UI;

public class InterractionSound : MonoBehaviour
{
	public Slider  InteractionVolumeSlider;
	public Sound[] sounds;

	private bool isPlayerOn;
	private int  k;

	private void Awake()
	{
		foreach (Sound s in sounds)
		{
			var sr = new AudioSource();
			sr.clip   = s.audioClip;
			sr.loop   = s.loop;
			sr.volume = InteractionVolumeSlider.value;

			s.source = sr;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "Player")
		{
			PlaySound(sounds[0]);

			isPlayerOn = true;
		}
	}

	private void FixedUpdate()
	{
		if (isPlayerOn)
			if (Input.GetKeyDown(KeyCode.E))
				PlaySound(sounds[++k]);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.name == "Player")
			isPlayerOn = false;
	}

	private void PlaySound(Sound s)
	{
		if (s == null) return;
		s.volume = SystemVariables.audioSettings.soundEffectVolume;
		s.source.Play();
	}
}
