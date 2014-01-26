using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		//GameObject gObject = new GameObject("TempAudio");
		SoundManager.Instance.PlaySoundAndLight(gameObject, SoundManager.Action.HIT);
		SoundManager.Instance.PlaySoundAndLight(gameObject, SoundManager.Action.LOOSE);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
