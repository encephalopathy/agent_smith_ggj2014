using UnityEngine;
using System.Collections;

public interface INetworkManagerCallback {
	void OnStartServerSuccess(string serverName);
	void OnHostListSuccess(HostData[] hostList);
	void OnConnectToServerSuccess();
	void OnConnectToServerFail();
}
