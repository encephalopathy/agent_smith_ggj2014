﻿using UnityEngine;
using System.Collections;

public class LoginGui : MonoBehaviour, INetworkManagerCallback {

	public enum GUIState { Login, ChooseClient, ConnectWait, Success, Failure, Instruction, Countdown }
	public GUIState currentState = GUIState.Login;

	public NetworkManager networkManager = null;
	public Object networkSyncerPrefab = null;
	public Texture2D leftBracket = null;
	public Texture2D rightBracket = null;
	public Texture2D testTexture = null;
	
	private Vector2 scrollViewVector = Vector2.zero;

	private HostData[] hostList = null;
	private string serverName = null;

	public void OnConnectToServerSuccess() {
		Debug.Log("Connected to server: " + serverName);
		currentState = GUIState.Login;
	}

	public void OnConnectToServerFail() {
		Debug.Log("Failed to connect to server");
		currentState = GUIState.Login;
	}

	public void OnHostListSuccess(HostData[] hostList) {
		this.hostList = hostList;

		if (hostList == null || hostList.Length == 0) {
			currentState = GUIState.Login;
		} else {
			currentState = GUIState.ChooseClient;
		}
	}

	public void OnStartServerSuccess(string name) {
		Debug.Log("Started server: " + name);
		this.serverName = name;
		currentState = GUIState.Login;
	}

	void OnGUI () {
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;

		switch (currentState) {
			case GUIState.ChooseClient:
				ChooseClientScreen();
				break;
			case GUIState.ConnectWait:
				WaitScreen();
				break;
			case GUIState.Login:
				LoginScreen();
				break;
			case GUIState.Success:
				LoginSuccessScreen();
				break;
			case GUIState.Failure:
				LoginFailureScreen();
				break;
			case GUIState.Instruction:
				InstructionScreen();
				break;
			case GUIState.Countdown:
				CountdownScreen();
				break;	
		}
	}

	Rect centerOn (float centerX, float centerY, float width, float height) 
	{
		return new Rect(centerX - width / 2, centerY - height / 2, width, height);
	}

	void surroundWithBrackets (Rect rect) {
		GUI.Label ( centerOn (rect.x, rect.y + 25, 20, 50), leftBracket);
		GUI.Label ( centerOn (rect.x + rect.width, rect.y + 25, 20, 50), rightBracket);
	}

	void ChooseClientScreen() {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 1.0f;

		for (int i = 0; i < hostList.Length; i++)
		{
			if (GUI.Button(centerOn (Screen.width * 0.5f, 25 + 25*i, groupWidth * 0.75f, 25), "Join " + hostList[i].gameName)) {
				currentState = GUIState.ConnectWait;
				serverName = hostList[i].gameName;
				networkManager.JoinServer(hostList[i]);
			}
		}
	}

	void WaitScreen() {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.25f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");
		
		Rect textDimensions = centerOn (groupWidth * 0.5f, groupHeight * 0.5f, groupWidth * 0.5f, 50);
		GUI.Label ( textDimensions, "Waiting to connect...");
		surroundWithBrackets(textDimensions);
		GUI.EndGroup();
	}

	void LoginScreen () {		
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.4f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.75f, groupWidth, groupHeight));
		
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");
		
		float textWidth = groupWidth * 0.75f;
		float textHeight = 25;

		string statusMessage = "Choose to play as a\nserver or client first";
		if (Network.isServer) {
			statusMessage = "Playing as server " + serverName;
		} else if (Network.isClient) {
			statusMessage = "Connected to server " + serverName;
		}

		Rect textDimensions = centerOn(groupWidth * 0.5f, groupHeight * 0.33f, textWidth, textHeight * 2);
		GUI.Label (textDimensions, statusMessage);

		// Decorate the center text with some fancy brackets.
		surroundWithBrackets(textDimensions);

		NetworkSyncer syncer = NetworkSyncer.GetSyncer();
		if (Network.isServer || Network.isClient) {
			if (GUI.Button (centerOn(groupWidth * 0.5f, groupHeight * 0.66f, textWidth, textHeight), "Login")) {
				currentState = GUIState.Success;
				//FB.Init(SetInit, OnHideUnity, null);
			}
			GUI.Label (centerOn(groupWidth * 0.5f, groupHeight * 0.66f + 2*textHeight, textWidth, textHeight), "Have " + syncer.numConnected + " agents connected");
		} else {
			if (syncer == null || syncer.numConnected < syncer.maxPlayers) {
				if (GUI.Button (centerOn(groupWidth * 0.5f, groupHeight * 0.66f + textHeight, textWidth, textHeight), "Connect as Server")) {
					Network.Disconnect();
					serverName = null;
					currentState = GUIState.ConnectWait;
					networkManager.TryStartServer(this);
				}
				if (GUI.Button (centerOn(groupWidth * 0.5f, groupHeight * 0.66f + 2*textHeight, textWidth, textHeight), "Connect as Client / Switch Server")) {
					Network.Disconnect();
					serverName = null;
					currentState = GUIState.ConnectWait;
					networkManager.TryConnectToServer(this);
				}
			}
		}
		
		GUI.EndGroup();
	}
	
	void LoginSuccessScreen () {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 1f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");
		
		float textWidth = groupWidth * 0.5f;
		float textHeight = 25;
		
		Rect textDimensions = centerOn(groupWidth * 0.5f, groupHeight * 0.15f, textWidth, textHeight * 2);
		GUI.Label ( textDimensions, "WELCOME\nAgent Foo Bar Baz");
		
		// Decorate the center text with some fancy brackets.
		surroundWithBrackets(textDimensions);

		// Player texture!
		float playerHeight = groupHeight - textDimensions.y - groupHeight * 0.3f;
		GUI.Label ( centerOn (groupWidth * 0.5f, textDimensions.y + playerHeight * 0.5f, testTexture.width, testTexture.height), testTexture);

		GUI.Label ( centerOn (groupWidth * 0.5f, groupHeight * 0.7f, textWidth, textHeight * 3.0f),
		           "We have given you the CIRCLE persona.\n" +
		           "Your mission is to turn those who are not you into how you see yourself.");

		if (GUI.Button (centerOn(groupWidth * 0.5f, groupHeight * 0.85f, textWidth, textHeight), "Continue")) {
			currentState = GUIState.Instruction;
			//FB.Init(SetInit, OnHideUnity, null);
		}
		
		GUI.EndGroup();
	}
	
	void LoginFailureScreen () {
		
	}
	
	void InstructionScreen () {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 1f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");
		
		float textWidth = groupWidth * 0.5f;
		float textHeight = 25;

		GUI.Label ( centerOn (groupWidth * 0.2f, groupHeight * 0.15f, groupWidth * 0.2f, groupHeight * 0.3f), "Your enemies are those\nwho appear different");
		GUI.Label ( centerOn (groupWidth * 0.2f, groupHeight * 0.4f, groupWidth * 0.2f, groupHeight * 0.3f), "Move with WASD\nAttack with SPACE");
		GUI.Label ( centerOn (groupWidth * 0.2f, groupHeight * 0.65f, groupWidth * 0.4f, groupHeight * 0.3f), "Hit them, and assimilate\nyour foes to your side");

		GUI.Label ( centerOn (groupWidth * 0.65f, groupHeight * 0.15f, groupWidth * 0.7f, groupHeight * 0.3f), testTexture);
		GUI.Label ( centerOn (groupWidth * 0.65f, groupHeight * 0.4f, groupWidth * 0.7f, groupHeight * 0.3f), testTexture);
		GUI.Label ( centerOn (groupWidth * 0.65f, groupHeight * 0.65f, groupWidth * 0.7f, groupHeight * 0.3f), testTexture);

		GUI.Label ( centerOn (groupWidth * 0.5f, groupHeight * 0.28f, groupWidth * 1f, 20), "_____________________________________________________________________________");
		GUI.Label ( centerOn (groupWidth * 0.5f, groupHeight * 0.53f, groupWidth * 1f, 20), "_____________________________________________________________________________");
	
		if (GUI.Button (centerOn(groupWidth * 0.5f, groupHeight * 0.85f, textWidth, textHeight), "Continue")) {
			currentState = GUIState.Countdown;
			NetworkSyncer.GetSyncer().amReady();
		}
		
		GUI.EndGroup();	
	}
	
	void CountdownScreen () {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.25f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");

		string agentString = "Waiting on 1 agent...";
		NetworkSyncer syncer = NetworkSyncer.GetSyncer();
		if (syncer != null) {
			if (syncer.numReady < syncer.maxPlayers) {
				agentString = "Have " + syncer.numReady + " agents, waiting for " + syncer.maxPlayers;
			} else if (syncer.numReady >= syncer.maxPlayers) {
				if (Network.isServer) {
					syncer.finishIntro();
				}
			}
		}
		Rect textDimensions = centerOn (groupWidth * 0.5f, groupHeight * 0.5f, groupWidth * 0.5f, 50);
		GUI.Label ( textDimensions, agentString);
		surroundWithBrackets(textDimensions);
		GUI.EndGroup();
	}
}
