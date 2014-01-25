using UnityEngine;
using System.Collections;

public class BackGroundMusic : MonoBehaviour {
	public AudioClip[] myMusic;
	// Use this for initialization
	void Start () {
		if(!audio.isPlaying)
			playRandomMusic();
	}
	
	
	private void playRandomMusic()
	{
		int rnd = Random.Range (0, myMusic.Length);
		audio.clip = myMusic[rnd];
		audio.Play();
		
	}
}