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

		}
	}

	[RPC]
	void ThrowWhip() {
		if (Input.GetKey(KeyCode.Space)) {
			animation.Play("whipAnim");
		}
	}
}
