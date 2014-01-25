using UnityEngine;
using System.Collections;

using Facebook;

public class LoginGui : MonoBehaviour {

	enum GUIState { Login, Success, Failure, Instruction, Countdown }
	GUIState currentState = GUIState.Login;
	
	Texture2D leftBracket = null;
	Texture2D rightBracket = null;
	
	void OnGUI () {
		int width = Screen.width / 4;
		int height = Screen.height;
		
		switch (currentState) {
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

	void SetInit () {
		System.Console.WriteLine("set init");
	}

	void OnHideUnity(bool isGameShown) {
		System.Console.WriteLine("on hide");
	}

	void LoginScreen () {
		float width = Screen.width * 0.25f;
		float height= Screen.height;
		
		float groupWidth = Screen.width * 0.5f;
		float groupHeight = Screen.height * 0.4f;
		GUI.BeginGroup(centerOn(Screen.width * 0.5f, Screen.height * 0.75f, groupWidth, groupHeight));
		
		GUI.Box (new Rect(0, 0, groupWidth, groupHeight), "");
		
		float textWidth = groupWidth * 0.5f;
		float textHeight = 25;
		GUI.Label ( centerOn(groupWidth * 0.5f, groupHeight * 0.33f, textWidth, textHeight * 2), "# agents ready\nwaiting for # agents");
		GUI.Label (new Rect(0, 0, 100, 100), leftBracket);
		
		if (GUI.Button (centerOn(groupWidth * 0.5f, groupHeight * 0.66f, textWidth, textHeight), "Login")) {
			currentState = GUIState.Success;
			FB.Init(SetInit, OnHideUnity, null);
		}
		
		GUI.EndGroup();
	}
	
	void LoginSuccessScreen () {
		if (GUI.Button (new Rect(20,40,80,20), "SUCCESS!")) {
			currentState = GUIState.Success;
		}
	}
	
	void LoginFailureScreen () {
		
	}
	
	void InstructionScreen () {
		
	}
	
	void CountdownScreen () {
		
	}
}
