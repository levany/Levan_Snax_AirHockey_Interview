using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{
    //// Members

    [Header("General")]
    public bool PlayBGMusic = true;

    [Header("Clips")]
	public AudioClip hitPuck;
	public AudioClip score;
	public AudioClip music;

    [Header("references")]
	public AudioSource sfxAudioSource;
	public AudioSource musicAudioSource;

    //// API

	public void PlayHitPuck()
	{
		sfxAudioSource.clip = hitPuck;
		playSfx();
	}

	public void PlayScore()
	{
		sfxAudioSource.clip = score;
		playSfx();
	}

    //// Lifecycle Events

    private void OnEnable()
    {
        if (this.PlayBGMusic)
        {
            musicAudioSource.loop = true;
            musicAudioSource.clip = music;
            musicAudioSource.Play();
        }
    }

    private void OnDisable()
    {
        musicAudioSource.Stop();
        sfxAudioSource.Stop();
    }

    //// Methods

    private void playSfx()
	{
		sfxAudioSource.Play();
	}

    private void playMusic()
	{
		musicAudioSource.loop = true;
        musicAudioSource.Play();
	}
}
