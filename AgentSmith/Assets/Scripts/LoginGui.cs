using UnityEngine;
using System.Collections;

public class LoginGui : MonoBehaviour, INetworkManagerCallback {

	public enum GUIState { ServerClient, Server, ChooseClient, ConnectWait, Login, Success, Failure, Instruction, Countdown, Finish }
	public GUIState currentState = GUIState.ServerClient;

	public NetworkManager networkManager = null;
	public Texture2D leftBracket = null;
	public Texture2D rightBracket = null;
	public Texture2D testTexture = null;
	
	private Vector2 scrollViewVector = Vector2.zero;

	private HostData[] hostList = null;
	private string serverName = null;
	
	public void OnConnectToServerSuccess() {
		Debug.Log("Connected to server!");
		currentState = GUIState.Success;
	}

	public void OnHostListSuccess(HostData[] hostList) {
		this.hostList = hostList;
		currentState = GUIState.ChooseClient;
	}

	public void OnStartServerSuccess(string name) {
		Debug.Log("Started server: " + name);
		this.serverName = name;
		currentState = GUIState.Server;
	}

	void OnGUI () {
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;

		switch (currentState) {
			case GUIState.ServerClient:
				ChooseServerClient();
				break;
			case GUIState.Server:
				ServerScreen();
				break;
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

	void ChooseServerClient()
	{
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.4f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");

		if (GUI.Button (centerOn(groupWidth * 0.25f, groupHeight * 0.5f, groupWidth * 0.3f, 25), "Server?")) {
			currentState = GUIState.ConnectWait;
			networkManager.TryStartServer(this);
		}

		if (GUI.Button (centerOn(groupWidth * 0.75f, groupHeight * 0.5f, groupWidth * 0.3f, 25), "Client?")) {
			currentState = GUIState.ConnectWait;
			networkManager.TryConnectToServer(this);
		}

		GUI.EndGroup();
	}

	void ServerScreen() {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.25f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");
		
		Rect textDimensions = centerOn (groupWidth * 0.5f, groupHeight * 0.5f, groupWidth * 0.5f, 50);
		GUI.Label (textDimensions, "Devoted server name: " + serverName);
		surroundWithBrackets(textDimensions);
		GUI.EndGroup();
	}

	void ChooseClientScreen() {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 1.0f;

		//scrollViewVector = GUI.BeginScrollView (centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight),
		//                                        scrollViewVector,
		//                                        centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, 50 + 25*hostList.Length));
		for (int i = 0; i < hostList.Length; i++)
		{
			if (GUI.Button(centerOn (Screen.width * 0.5f, 25 + 25*i, groupWidth * 0.75f, 25), "Join " + hostList[i].gameName)) {
				networkManager.JoinServer(hostList[i]);
			}
		}
		
		// End the ScrollView
		//GUI.EndScrollView();
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
		
		float textWidth = groupWidth * 0.5f;
		float textHeight = 25;

		Rect textDimensions = centerOn(groupWidth * 0.5f, groupHeight * 0.33f, textWidth, textHeight * 2);
		GUI.Label ( textDimensions, "# agents ready\nwaiting for # agents");

		// Decorate the center text with some fancy brackets.
		surroundWithBrackets(textDimensions);
		
		if (GUI.Button (centerOn(groupWidth * 0.5f, groupHeight * 0.66f, textWidth, textHeight), "Login")) {
			currentState = GUIState.Success;
			//FB.Init(SetInit, OnHideUnity, null);
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
		}
		
		GUI.EndGroup();	
	}
	
	void CountdownScreen () {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.25f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");

		Rect textDimensions = centerOn (groupWidth * 0.5f, groupHeight * 0.5f, groupWidth * 0.5f, 50);
		GUI.Label ( textDimensions, "Waiting on # agents...");
		surroundWithBrackets(textDimensions);
		GUI.EndGroup();

		// For now, force countdown
		Application.LoadLevel(1);
	}

	void FinishScreen () {
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.25f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.5f, groupWidth, groupHeight));
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");
		
		Rect textDimensions = centerOn (groupWidth * 0.5f, groupHeight * 0.5f, groupWidth * 0.5f, 50);
		GUI.Label ( textDimensions, "Entering in # seconds\nGood luck, Agent Foo");
		surroundWithBrackets(textDimensions);
		GUI.EndGroup();
	}
}
