using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	float maxDistanceOne, maxDistanceTwo, maxDistanceThree;
	Vector3 radius, posOne, posTwo;
	float speed = 20f;
	bool sideBySide,circle,swarm,standStill,flee;
	static int numOfEnemies = 1;

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;

	// Use this for initialization
	void Start () {

		sideBySide = false;
		circle = false;
		swarm = false;
		standStill = false;
		flee = false;

		while (numOfEnemies < 30){
			GameObject.Instantiate(this, transform.position + new Vector3(Random.Range(5.0f,10.0f),0,Random.Range(5.0f,10.0f)),transform.rotation);
			numOfEnemies++;
		}

		setBehavior();
	}
	
	// Update is called once per frame
	void Update () {
		if (networkView.isMine)
		{
			moveEnemy();
		}
	}

	void OnNetworkInstantiate(NetworkMessageInfo info){
		if (networkView.isMine){
			Camera.main.transform.parent = transform;
		}
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		Vector3 syncVelocity = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
			
			syncPosition = rigidbody.velocity;
			stream.Serialize(ref syncVelocity);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			stream.Serialize(ref syncVelocity);
			
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncEndPosition = syncPosition + syncVelocity * syncDelay;
			syncStartPosition = rigidbody.position;
		}
	}

	void Awake()
	{
		lastSynchronizationTime = Time.time;
	}

	void OnCollisionEnter(Collision datCollider){

	}

	void setBehavior(){
		if(Application.loadedLevelName == "CheckerPlane"){
			sideToSide ();
		}
		else if (Application.loadedLevelName == "TechArena"){
			moveInCircle();

		}
	}

	void sideToSide(){
		posOne = new Vector3(transform.position.x - 11.0f,transform.position.y + 11.0f,transform.position.z);
		posTwo = new Vector3(transform.position.x + 11.0f,transform.position.y - 11.0f,transform.position.z);
		sideBySide = true;
	}

	void moveInCircle(){

		circle = true;

		radius = new Vector3(transform.position.x - 7f,transform.position.y + 7f,transform.position.z);

	}

	void moveEnemy(){
		if(sideBySide){	
			transform.position = Vector3.Lerp(posOne, posTwo, Mathf.PingPong(speed * Time.deltaTime, 0.1f));
		}
		else if (circle){
			transform.RotateAround(radius, Vector3.up, speed * Time.deltaTime);
		}
		else if (swarm){

		}
		else if (flee){

		}
	}

	void fleeMove(){

	}

	void swarmMove(){

	}

}
