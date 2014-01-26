using UnityEngine;
using System.Collections;

public class whipControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	void Update()
	{
		if (networkView.isMine)
		{
			if (Input.GetKey(KeyCode.Space)) {
				animation.Play("whipAnim");
			}
		}
	}
}
