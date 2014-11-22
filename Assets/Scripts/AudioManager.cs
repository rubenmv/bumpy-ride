using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	private bool _isMuted = false;

	public AudioClip music;
	public AudioClip explosion;


	// Use this for initialization
	void Start()
	{
		_isMuted = false;
	}

	public void init()
	{
		if (!_isMuted)
		{
			audio.clip = music;
			audio.Play();
			audio.loop = true;
		}
	}
	
	public bool isMuted()
	{
		return _isMuted;
	}

	public void setMuted(bool mute)
	{
		_isMuted = mute;

		if (_isMuted)
		{
			audio.volume = 0f;
		}
		else
		{
			audio.volume = 0.8f;
		}
	}

	public void playClip(AudioClip clip, float volume)
	{
		if (!_isMuted)
		{
			audio.PlayOneShot(clip, volume);
		}
	}
}
