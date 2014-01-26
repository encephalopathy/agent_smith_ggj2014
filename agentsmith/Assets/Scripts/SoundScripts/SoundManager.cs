using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	private static SoundManager instance;
	public AudioClip BackGroundSound;
	public  AudioClip[] myMusic;
	public  ParticleSystem[] myEffect;
	public enum Action {
		BACKGROUND, START,HIT,LOOSE,VICTORY,FAINT,WHINE,END
	};

	public Transform brick;
	public static SoundManager Instance {
		
		get {
			
			return instance;
			
		}
		
	}

 void Start()
	{
		audio.clip = myMusic[0];// define the clip
		audio.Play(); // start the sound

		}
	// Use this for initialization
  void Awake () {
				instance = this;
		}

	//GameObject gObject = new GameObject("TempAudio");
	//SoundManager.Instance.PlaySoundAndLight(gameObject, SoundManager.Action);

	public void PlaySoundAndLight(GameObject gObject, SoundManager.Action action)
	{
		int musicVal = 0;
		int effectVal = 0;
		switch (action) {
		case Action.BACKGROUND:
			musicVal=effectVal=0;
			break;
		case Action.START:
			musicVal=effectVal=1;
			break;
		case Action.HIT:
			musicVal=effectVal=2;
			break;
		case Action.LOOSE:
			musicVal=effectVal=3;
			break;
		case Action.FAINT:
			musicVal=effectVal=4;
			break;
		case Action.VICTORY:
			musicVal=effectVal=5;
			break;
		case Action.WHINE:
			musicVal=effectVal=6;
			break;
		case Action.END:
			musicVal=effectVal=7;
			break;
		default:
			musicVal=effectVal=0;
			break;
		}

		audio.PlayOneShot(myMusic[musicVal]); // start the sound


		//for (int y = 0; y < 5; y++) {
			//for (int x = 0; x < 5; x++) {
				//Instantiate(myEffect[effectVal], Vector3 (x, y, 0), Quaternion.identity);
			//}
		//}
		//myEffect[effectVal]
		//particle effect

		//Transform 
		Instantiate(myEffect[0], gObject.transform.position, Quaternion.identity);
		//

		//gObject.particleEmitter.emit 
		//ParticleEmitter emitter = (ParticleEmitter)gObject.AddComponent<ParticleEmitter> ();
		//emitter.emit = 
		// Calculate index
		//emitter.emit = true;
		//emitter.animation.animatePhysics = true;
		//Color col = guiTexture.color;
		//col.a = 11;
		//col.b = 22;
		//guiTexture.color = col;
		//guiTexture.color = Color.green;
		//emitter.Emit(10);
		//emitter.animation.Play();
		//Destroy(gameObject, audio.length); // destroy object after clip duration
	}

	//if(!audio.isPlaying)
	//playRandomMusic();
	
	private  void playRandomMusic()
	{
		int rnd = Random.Range (0, myMusic.Length);
		audio.clip = myMusic[rnd];
		audio.Play();
		
	}
}

