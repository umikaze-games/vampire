using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;
	public AudioSource bgmPlayer;
	public AudioClip stage1bgm;
	public List<SFXData>sFXDatas;
	public AudioSource sfxPlayer;
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else
		{
			Destroy(this.gameObject);
		}
		//bgmPlayer = GetComponent<AudioSource>();
	}
	private void Start()
	{
		PlayBGM();
	}
	private void OnEnable()
	{
		EventHandler.PlaySFXEvent += OnPlayerSFXEvent;
	}
	private void OnDisable()
	{
		EventHandler.PlaySFXEvent -= OnPlayerSFXEvent;
	}
	public void PlayBGM()
	{ 
		bgmPlayer.clip = stage1bgm;
		bgmPlayer.Play();
	}

	public void StopBGM()
	{
		bgmPlayer.Stop();
	}

	public void OnPlayerSFXEvent(SFXName sfxName)
	{
		if (sfxPlayer.isPlaying && sfxPlayer.clip == GetClipFromName(sfxName))
		{
			return; 
		}

		for (int i = 0; i < sFXDatas.Count; i++)
		{
			if (sFXDatas[i].sfxName == sfxName)
			{
				sfxPlayer.clip = sFXDatas[i].sfxClip;

				break;
			}
			
		}
		sfxPlayer.Play();
	}
	private AudioClip GetClipFromName(SFXName sfxName)
	{
		foreach (var sfxData in sFXDatas)
		{
			if (sfxData.sfxName == sfxName)
			{
				return sfxData.sfxClip;
			}
		}
		return null; 
	}
}
