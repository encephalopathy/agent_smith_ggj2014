using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	public Object PlayerPrefab = null;

	// Use this for initialization
	void Start () {
		Network.Instantiate(PlayerPrefab, Vector3.up * 5, Quaternion.identity, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
