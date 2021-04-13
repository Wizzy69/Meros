using System;
using UnityEngine;

[Serializable]
public class Sound
{
	public float volume;
	public float speed;
	public bool  loop;

	public AudioClip   audioClip;
	public AudioSource source;
}
