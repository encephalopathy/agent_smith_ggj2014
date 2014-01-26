using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	public Object PlayerPrefab = null;
	public Player player = null;

	// Use this for initialization
	void Start () {
		NetworkSyncer.GetSyncer().sync();
	}

	// Update is called once per frame
	void Update () {
		if (player != null) {
			return;
		}

		NetworkSyncer syncer = NetworkSyncer.GetSyncer();
		if (syncer.numSynced == syncer.maxPlayers) {
			Network.Instantiate(PlayerPrefab, Vector3.up, Quaternion.identity, 0);
			syncer.numSynced = 0;
		}
	}
}
