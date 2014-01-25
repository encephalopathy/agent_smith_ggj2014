using UnityEngine;
using System.Collections;

public class tongueAnimate : MonoBehaviour {



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKey(KeyCode.Space)) {
			transform.localScale = Vector3.Lerp (Vector3.one, Vector3.one * 3.0f, Time.deltaTime);
		}
	
	}
}
