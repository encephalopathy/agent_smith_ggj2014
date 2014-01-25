using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using Parse;

public class AnalyticsControllerCS : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		ParseAnalytics.TrackAppOpenedAsync ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void trackAnalytics(string name, Dictionary<string, string> data) {
		ParseAnalytics.TrackEventAsync (name, data);
	}

}
