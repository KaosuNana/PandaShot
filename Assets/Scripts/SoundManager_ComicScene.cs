using UnityEngine;
using System.Collections;

public class SoundManager_ComicScene : MonoBehaviour {

	public AudioSource beepSound;
	public AudioSource swoosh1;
	public AudioSource swoosh2;
	public AudioSource swoosh3;
	public AudioSource swoosh4;
	public AudioSource clickToAnswer;
	public AudioSource comicMusic;
	
	
	static SoundManager_ComicScene instance;
	
	public static SoundManager_ComicScene Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(SoundManager_ComicScene)) as SoundManager_ComicScene;
			}
			
			return instance;
		}
	}

	public void Play_BeepSound()
	{
		if(beepSound.clip != null && SoundManager.soundOn == 1)
			beepSound.Play();
	}
	
	public void Play_Swoosh1()
	{
		if(swoosh1.clip != null && SoundManager.soundOn == 1)
		{
			swoosh1.Play();
		}
	}

	public void Play_Swoosh2()
	{
		if(swoosh2.clip != null && SoundManager.soundOn == 1)
			swoosh2.Play();
	}

	public void Play_Swoosh3()
	{
		if(swoosh3.clip != null && SoundManager.soundOn == 1)
			swoosh3.Play();
	}

	public void Play_Swoosh4()
	{
		if(swoosh4.clip != null && SoundManager.soundOn == 1)
			swoosh4.Play();
	}
	
	public void Play_ClickToAnswer()
	{
		if(clickToAnswer.clip != null && SoundManager.soundOn == 1)
			clickToAnswer.Play();
	}
	
	public void Play_ComicMusic()
	{
		if(comicMusic.clip != null && SoundManager.musicOn == 1)
			comicMusic.Play();
	}

	public void Stop_ComicMusic()
	{
		if(comicMusic.clip != null && SoundManager.musicOn == 1)
			StartCoroutine(FadeOut(comicMusic, 0.0035f));
	}

	IEnumerator FadeOut(AudioSource sound, float time)
	{
		float originalVolume = sound.volume;
		while(sound.volume != 0)
		{
			sound.volume = Mathf.MoveTowards(sound.volume, 0, time);
			yield return null;
		}
		sound.Stop();
		sound.volume = originalVolume;
	}
}
