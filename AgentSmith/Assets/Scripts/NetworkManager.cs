using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	public Object networkSyncerPrefab = null;
	private const string typeName = "Agent_GGJ2014";
	private const string gameNameBase = "AgentSmith_";
	private string gameName = "DEFAULT";
    private HostData[] hostList;

	private INetworkManagerCallback onClientConnectSuccessCallback;

	public void TryStartServer(INetworkManagerCallback callbacker)
    {
		onClientConnectSuccessCallback = callbacker;
		gameName = gameNameBase + Random.value;
        Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
		// Create a server-based object that will keep track of everyone connected to it.
		Destroy(GameObject.Find("NetworkSyncer(Clone)"));
		Network.Instantiate(networkSyncerPrefab, transform.position, transform.rotation, 0);
		onClientConnectSuccessCallback.OnStartServerSuccess(gameName);
    }

	public void TryConnectToServer(INetworkManagerCallback callbacker)
	{
		onClientConnectSuccessCallback = callbacker;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList();

			if (hostList != null && hostList.Length > 0) {
				onClientConnectSuccessCallback.OnHostListSuccess(hostList);
			}
		}
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}

    void OnConnectedToServer()
    {
		onClientConnectSuccessCallback.OnConnectToServerSuccess();
    }

	void OnFailedToConnect(NetworkConnectionError error)
	{
		onClientConnectSuccessCallback.OnConnectToServerFail();
	}
}
