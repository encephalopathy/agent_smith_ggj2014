using UnityEngine;
using System.Collections;

public class NetworkSyncer : MonoBehaviour {

	public int maxPlayers = 3;
	public int numConnected = 0;
	public int numReady = 0;
	public int numSynced = 0;

	public static NetworkSyncer GetSyncer() {
		GameObject obj = GameObject.Find("NetworkSyncer(Clone)");
		if (obj != null) {
			return obj.GetComponent<NetworkSyncer>();
		}
		return null;
	}

	public void sync() {
		networkView.RPC("Sync", RPCMode.AllBuffered);
	}

	public void resetSync() {
		networkView.RPC("ResetSync", RPCMode.AllBuffered);
	}

	public void amReady() {
		networkView.RPC("AddAmReady", RPCMode.AllBuffered);
	}

	public void finishIntro() {
		networkView.RPC("LoadGame", RPCMode.AllBuffered);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnNetworkInstantiate(NetworkMessageInfo info) {
		Debug.Log("New syncer instantiated by " + info.sender);
		networkView.RPC("AddNewClient", RPCMode.AllBuffered);
	}

	[RPC]
	void Sync() {
		++numSynced;
	}
	
	[RPC]
	void ResetSync() {
		numSynced = 0;
	}

	[RPC]
	void AddAmReady() {
		++numReady;
	}

	[RPC]
	void AddNewClient() {
		++numConnected;
	}

	[RPC]
	void LoadGame () {
		Network.SetSendingEnabled(0, false);	
		Network.isMessageQueueRunning = false;
		
		// All network views loaded from a level will get a prefix into their NetworkViewID.
		Application.LoadLevel(1);

		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled(0, true);
	}
}
