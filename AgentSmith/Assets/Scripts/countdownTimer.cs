using UnityEngine;
using System.Collections;

public class countdownTimer : MonoBehaviour
{
		public float t_timerStart = 2000000f; //Seconds
		private float t_reset = 0;
		public bool t_active = true;
		private float t_currentCount = 0;
		public GUIText t_spot;

		// Use this for initialization
		void Awake ()
		{
				t_reset = t_timerStart;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (t_active == true && t_currentCount >= 0) {
						t_currentCount = t_timerStart - Time.time;
						t_spot.text = timeConversions (t_currentCount);
				}
		}

		void OnGUI () {
		GUI.Box (new Rect (0,0,300,50),timeConversions (t_currentCount) );
		}
	
		private string timeConversions (float timeInput)
		{
				int t_minutes = (int)(timeInput / 60);
				int t_seconds = (int)(timeInput % 60);
				return (t_minutes.ToString () + " Minutes and " + t_seconds.ToString () + " Seconds Remaining");
		}

		void resetTimer ()
		{
				t_timerStart = t_reset;
		}


}
