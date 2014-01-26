using UnityEngine;
using System.Collections;

public class simpleAI : MonoBehaviour
{

		private string lastState;
		private string nextState = null;
		private float numberStorage;
		private float randomNum;
		private string[] choices = {"rotate","wander","identify","cooldown"};
		private NavMeshAgent navigation;
		public GameObject hunted;

		public bool waiting = false;

		// Use this for initialization
		void Start ()
		{
				navigation = GetComponent<NavMeshAgent> ();
				takeAction ();
				gameObject.name = (GetComponent<MeshFilter>().mesh).ToString();
		}
	
		// Update is called once per frame
		void Update ()
		{
				takeAction ();
		}
	
		void takeAction ()
		{
				if (nextState != null) {
						nextState = lastState;
				} else {
						randomNum = Random.Range (-120, 120);
						numberStorage = 0;

						nextState = choices [Random.Range (0, choices.Length)];
				}

				switch (nextState) {
				case ("rotate"):
						Rotate ();
						lastState = "rotate";
						break;
				case ("wander"):
						Wander ();
						lastState = "wander";
						break;
				case ("identify"):
						IdentifyThreats ();
						if (hunted != null) {
								Attack ();
						}
						lastState = "identify";
						break;
				case("cooldown"):
						waiting = false; {
							StartCoroutine("Cooldown");
						}
						break;
				}
		}

		void Rotate ()
		{ //Doesn't do anything right now
				if (Mathf.Abs (numberStorage) < randomNum) {
						//transform.Rotate (new Vector3 (0,Mathf.Sign(randomNum)*5, 0));
						numberStorage = numberStorage + Mathf.Sign (randomNum) * 5;
				} else {
						nextState = null;
				}
		}

		void Wander ()
		{	
				Vector3 randomDirection = Random.insideUnitSphere * 200;
			
				randomDirection += transform.position;
				NavMeshHit hit;
				NavMesh.SamplePosition (randomDirection, out hit, 200, 1);
				Vector3 finalPosition = hit.position;
				navigation.SetDestination (finalPosition);

		nextState = null;

		}

		void IdentifyThreats ()
		{	
				GameObject[] gameobj = FindObjectsOfType (typeof(GameObject)) as GameObject[];
				foreach (GameObject testpoint in gameobj) {
						if (testpoint.tag == "Convertable") {
				if (testpoint.GetComponent<MeshFilter>().sharedMesh != GetComponent<MeshFilter>().sharedMesh && name != testpoint.name) {
										hunted = testpoint;
								} else {
										hunted = null;
								}
						}
				}
				nextState = null;
		}


		void Attack ()
		{
				navigation.SetDestination (hunted.transform.position);
		if (Vector3.Distance (transform.position, hunted.transform.position) <= 50) {
						GetComponentInChildren<Animation> ().Play ("whipAnim");
			SoundManager.Instance.PlaySoundAndLight(gameObject, SoundManager.Action.HIT);
				}
		}

		IEnumerator Cooldown () {
		//This doesn't work. T_T
			waiting = true;
			yield return new WaitForSeconds(10f);
			nextState = null;
			waiting = false;
		}


};