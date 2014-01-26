using UnityEngine;
using System.Collections;

public class whipControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	/*void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
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
			
			
			myCam = gameObject.AddComponent<Camera>();
			//Camera.current = myCam;
		}
	}
	
	void Awake()
	{
		lastSynchronizationTime = Time.time;
	}*/
	
	void Update()
	{
		if (networkView.isMine)
		{
			if (Input.GetKey(KeyCode.Space)) {
				animation.Play("whipAnim");
			}
		}
	}
}
