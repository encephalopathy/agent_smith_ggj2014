using UnityEngine;
using System.Collections;

public class TeamInfo : MonoBehaviour {

	public Color color;
	public Mesh mesh;
	public Material profilePicMaterial;
	public string profileName;
	public string tag;

	public static TeamInfo[] GetAllTeams() {
		return GameObject.Find("GlobalState").GetComponents<TeamInfo>();
	}
	public static TeamInfo GetMyTeam() {
		return GameObject.Find("GlobalState").GetComponent<LocalData>().myTeam;
	}
	public static void SetMyTeam(TeamInfo team) {
		print("Got " + GameObject.Find("GlobalState"));
		print("Got component " + GameObject.Find("GlobalState").GetComponent<LocalData>());
		GameObject.Find("GlobalState").GetComponent<LocalData>().myTeam = team;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
