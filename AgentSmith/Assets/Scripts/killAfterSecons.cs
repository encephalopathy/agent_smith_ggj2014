using UnityEngine;
using System.Collections;

public class killAfterSecons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("CleanupParticles");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator CleanupParticles() {
		yield return new WaitForSeconds(5f);
		Destroy (this.gameObject);
	}
}
